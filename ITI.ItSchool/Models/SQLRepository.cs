using ITI.ItSchool.Models.AvatarEntities;
using ITI.ItSchool.Models.ClassExercicesPlug;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public class SQLRepository : IRepository
    {
        /// <summary>
        /// [HELPER METHOD] Finds a user by it's id.
        /// </summary>
        /// <param name="id">The id of the user we are looking for.</param>
        /// <returns>The user we found.</returns>
        private User FindUserById(int id)
        {
            User userFound = null;
            using (var db = new UserContext())
            {
                userFound = db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault();
            }
            return userFound;
        }


        /// <summary>
        /// Checks if the fields on the client side are well filled
        /// </summary>
        /// <param name="user">The user which contains the fields (name, class, nickname...)</param>
        /// <returns>True if all the specified fields are not empty.</returns>
        private bool CheckEmptyFields(User user)
        {
            if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName) ||
                String.IsNullOrEmpty(user.Mail) || String.IsNullOrEmpty(user.Nickname) ||
                String.IsNullOrEmpty(user.Password))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if there is an existing mail in the db when the user submits it's register.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool CheckExistingMail(User user)
        {
            User usersMail = null;

            using (var db = new UserContext())
            {
                usersMail = db.Users.Where(u => u.Mail.Equals(user.Mail)).FirstOrDefault();

                if (usersMail == null) return false;

                return true;
            }
        }

        /// <summary>
        /// Creates a new class (of pupils).
        /// </summary>
        /// <param name="class">The class object to create.</param>
        /// <returns>True if it has well created.</returns>
        public bool Create(Class @class)
        {
            bool isCreated = false;
            using (var db = new SchoolContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Classes.Add(@class);
                db.SaveChanges();
                isCreated = true;
            }
            return isCreated;
        }

        /// <summary>
        /// Creates a new User add it to the database.
        /// We have to take care / the user now is creating a GradeID and a RightID
        /// </summary>
        /// <param name="user">The user to create as an object</param>
        /// <returns>True if the user was well created.</returns>
        public bool Create(User user)
        {
            User userToCreate = null;
            Avatar userAvatar = new Avatar();
            Class aClass = new Class();
            Group group = new Group();
            bool mailExists = true;
            bool emptyFields = false;

            if (user == null) throw new ArgumentNullException("The 'User' as an object type is null.", "user");

            using (var userContext = new UserContext())
            {
                userContext.Configuration.LazyLoadingEnabled = false;
                userToCreate = userContext.Users.Where(u => u.Nickname.Equals(user.Nickname)).FirstOrDefault();
                mailExists = this.CheckExistingMail(user);
                emptyFields = this.CheckEmptyFields(user);

                if (userToCreate == null)
                {
                    if (!mailExists && !emptyFields)
                    {
                        using (var avatarContext = new AvatarContext())
                        {
                            avatarContext.Configuration.LazyLoadingEnabled = false;
                            userAvatar.Name = "Avatar_" + user.Nickname;
                            Body body = avatarContext.Bodies.Where(b => b.Name.Equals("Corps")).FirstOrDefault();
                            userAvatar.Body = null;
                            userAvatar.BodyId = body.BodyId;
                            Foot feet = avatarContext.Feet.Where(f => f.Name.Equals("Pieds")).FirstOrDefault();
                            userAvatar.Feet = null;
                            userAvatar.FootId = feet.FootId;
                            Legs legs = avatarContext.Legs.Where(l => l.Name.Equals("Jambes")).FirstOrDefault();
                            userAvatar.Legs = null;
                            userAvatar.LegsId = legs.LegsId;
                            Avatar a = avatarContext.Avatars.OrderByDescending(av => av.AvatarId).FirstOrDefault();

                            if (a == null) user.AvatarId = 1;
                            else user.AvatarId = a.AvatarId + 1;

                            user.Avatar = userAvatar;
                            userAvatar.User = user;
                            user.Avatar = userAvatar;
                        }

                        using (SchoolContext sc = new SchoolContext())
                        {
                            sc.Configuration.LazyLoadingEnabled = false;
                            aClass = sc.Classes.Where(c => c.Name.Equals(user.Class.Name)).FirstOrDefault();
                            user.Class = null;
                            user.ClassId = aClass.ClassId;
                        }
                        User searchedUser = userContext.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
                        userAvatar.User = null;

                        if (searchedUser == null) userAvatar.UserId = 1;
                        else userAvatar.UserId = searchedUser.UserId + 1;

                        group = userContext.Groups.Where(gr => gr.Name.Equals(user.Group.Name)).FirstOrDefault();
                        user.Group = null;
                        user.GroupId = group.GroupId;
                        userContext.Users.Add(user);
                        userContext.SaveChanges();
                        int i = user.UserId;
                        group = userContext.Groups.Include("Users").Where(gr => gr.Name.Equals(user.Group.Name)).FirstOrDefault();
                        return true;
                    }
                    else return false;

                }
                else return false;
            }
        }

        /// <summary>
        /// Finds a user by his nickname
        /// </summary>
        /// <param name="nickname">The concerned user's nickname.</param>
        /// <returns>the User</returns>
        public User FindByNickname(string nickname)
        {
            User user = null;
            using (var uc = new UserContext())
            {
                user = uc.Users.Where(a => a.Nickname.Equals(nickname)).FirstOrDefault();
            }
            return user;
        }

        /// <summary>
        /// Find a user by his nickname
        /// </summary>
        /// <param name="nickname">String which represent the concerd user's nickname</param>
        /// <returns>JSon Data for AngularJS</returns>
        public JsonResult FindUserByNickname(string nickname)
        {
            JsonResult jsonData = null;
            using (var uc = new UserContext())
            {
                uc.Configuration.LazyLoadingEnabled = false;
                User user = uc.Users
                    .Include("Class")
                    .Include("Group")
                    .Include("Avatar")
                    .Where(a => a.Nickname.Equals(nickname)).FirstOrDefault();
                jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                if (user != null)
                {
                    user.Class.Users = null;
                    user.Group.Users = null;
                    user.Avatar.User = null;
                }
                else
                {
                    LoginData d = new LoginData();
                    d.Username = null;
                    d.Password = null;
                    jsonData = new JsonResult { Data = d, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            return jsonData;
        }

        public JsonResult SetExercise(Exercise exercise)
        {
            JsonResult jr = null;

            using (var db = new ExerciseContext())
            {
                Exercise e = db.Exercises.Add(exercise);
                db.SaveChanges();
                var jsonData = new JsonResult { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                jr = jsonData;
            }
            return jr;
        }

        /// <summary>
        /// Finds all users in our DB
        /// </summary>
        /// <returns>A list of Users</returns>
        public IList<User> FindAllUsers()
        {
            IList<User> users = new List<User>();
            using (UserContext userContext = new UserContext())
            {
                users = userContext.Users.ToList();
            }

            return users;
        }

        public IList<User> Update(User user)
        {
            IList<User> userToUpdate = new List<User>();
            using (var db = new UserContext())
            {
                var query = from u in db.Users
                            where u.UserId == user.UserId
                            select u;

                userToUpdate = db.Users.ToList();

                if (userToUpdate[0].Password.Equals(user.Password))
                {
                    userToUpdate[0].Nickname = user.Nickname;
                    userToUpdate[0].FirstName = user.FirstName;
                    userToUpdate[0].LastName = user.LastName;
                    userToUpdate[0].Mail = user.Mail;
                }
                else return null;

                return userToUpdate;
            }
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetChildrenByClassId(int id)
        {
            List<User> users = new List<User>();
            JsonResult data = null;
            using (UserContext uc = new UserContext())
            {
                uc.Configuration.LazyLoadingEnabled = false;
                users = uc.Users
                    .Include("Avatar")
                    .Include("Class")
                    .Include("Group")
                    .Where(u => u.ClassId.Equals(id)).Where(u => u.Group.Name.Equals("Élèves")).ToList();

                for (int i = 0; i < users.Count(); i++)
                {
                    users[i].Class.Users = null;
                    users[i].Group.Users = null;
                    users[i].Avatar.User = null;

                }
                data = new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }

        public List<User> GetChildrenListByClassId(int id)
        {
            List<User> children = new List<User>();
            using (UserContext uc = new UserContext())
            {
                children = uc.Users.Where(u => u.ClassId.Equals(id)).Where(u => u.Group.Name.Equals("Élèves")).ToList();
            }
            return children;
        }

        public List<int> GetChildrenListIdByClassId(int id)
        {
            List<int> childrenIDs = new List<int>();
            using (UserContext uc = new UserContext())
            {
                childrenIDs = uc.Users.Where(u => u.ClassId.Equals(id)).Where(u => u.Group.Name.Equals("Élèves")).Select(u => u.UserId).ToList();
            }
            return childrenIDs;
        }

        /// <summary>
        /// Gets all pupils' classes.
        /// </summary>
        /// <returns>All the classes in a list form in JSON Data format.</returns>
        public JsonResult GetClasses()
        {
            List<Class> classes = new List<Class>();
            JsonResult jsonData = null;
            using (var db = new SchoolContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                classes = db.Classes.ToList();
                jsonData = new JsonResult { Data = classes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return jsonData;
        }

        /// <summary>
        /// Gets all groups (Teacher, pupil,...).
        /// </summary>
        /// <returns>The groups as a JSON format.</returns>
        public JsonResult GetGroups()
        {
            List<Group> groups = new List<Group>();
            using (var db = new UserContext())
            {
                groups = db.Groups.OrderBy(g => g.Name).ToList();
                var jsonData = new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
            }
        }

        public JsonResult GetChapters()
        {
            List<Chapter> chapters = new List<Chapter>();
            using (var db = new SchoolContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                chapters = db.Chapters.ToList();
                return new JsonResult { Data = chapters, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public User FindById(int id)
        {
            User user = null;
            using (var uc = new UserContext())
            {
                user = uc.Users.Where(a => a.UserId.Equals(id)).FirstOrDefault();
            }
            return user;
        }

        public User FindByGrade(string grade)
        {
            throw new NotImplementedException();
        }

        public User FindByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User u)
        {
            throw new NotImplementedException();
        }


        public JsonResult getBattleCardChoice()
        {
            using (var db = new ExerciseBattleCardContext())
            {
                //db.ExerciseBattleCard.Where(exBattle => exBattle.Choice.Equals())
            }
            throw new NotImplementedException();
        }


        public JsonResult getUsersByClasses(int id)
        {
            List<User> users = new List<User>();
            JsonResult data = null;
            using (var uc = new UserContext())
            {
                uc.Configuration.LazyLoadingEnabled = false;

                users = uc.Users
                    //    .Include("Avatar")
                    //    .Include("Class")
                    //    .Include("Group")
                    .Where(u => u.ClassId.Equals(id)).Where(u => u.Group.Name.Equals("Élèves")).ToList();

                //for (int i = 0; i < users.Count(); ++i)
                //{
                //    users[i].Class.Users = null;
                //    users[i].Group.Users = null;
                //    users[i].Avatar.User = null;
                //}
                data = new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }


        public JsonResult GetClozeExerciseContent(string exerciseName)
        {
            ExerciseCloze ec = new ExerciseCloze();
            JsonResult data = null;
            using (var db = new ExerciseClozeContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                ec = db.ExerciseCloze.Where(e => e.Name.Equals(exerciseName)).FirstOrDefault();
                data = new JsonResult { Data = ec, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }

        /// <summary>
        /// Creates a new cloze exercise. 
        /// </summary>
        /// <param name="exCloze">The exercise to Create</param>
        /// <returns>3 probables possibilities : "created" means that we have well created the exercise, 
        /// "error existing name" means that there is already an exercise named with the same name received, "not created" 
        /// in any other case.</returns>
        public string CreateExerciseCloze(ExerciseClozeData exCloze)
        {
            List<int> usersIds = new List<int>();
            ExerciseType et = new ExerciseType();
            Exercise exercise = new Exercise();
            ExerciseCloze ec = null;
            Chapter c = new Chapter();
            Level l = new Level();
            User pupil = new User();
            string nameReceived = exCloze.Name;
            string levelReceived = exCloze.Level.Name;
            string chapterReceived = exCloze.Chapter.Name;
            string creationInfo = "not created";

            if (exCloze.UsersIds != null) usersIds = exCloze.UsersIds.ToList();

            pupil = this.FindUserById(usersIds[0]);

            // Get the Id of the Chapter which what we refer to
            using (var db = new SchoolContext())
            {
                c = db.Chapters.Where(ch => ch.Name.Equals(chapterReceived)).FirstOrDefault();
            }

            /* We create the Exercise cloze after catching all the FK we needed, and assign to the exercise an
            unique id */
            using (var db = new ExerciseClozeContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                // Before we check if the we already have an exercise cloze with an existing name
                ec = db.ExerciseCloze.Where(ex => ex.Name.Equals(nameReceived)).FirstOrDefault();

                if (ec != null) return "error existing name";

                // Get the Id of the Exercise Type and create a new Exercise
                using (var exerciseContext = new ExerciseContext())
                {
                    et = exerciseContext.ExerciseTypes.Where(ex => ex.Name.Equals("Texte à trous")).FirstOrDefault();
                    exercise.ExerciseTypeId = et.ExerciseTypeId;
                    exerciseContext.Exercises.Add(exercise);
                    exerciseContext.SaveChanges();
                }

                l = db.Level.Where(lv => lv.Name.Equals(levelReceived)).FirstOrDefault();

                // Create the cloze exercise
                ec = new ExerciseCloze();

                ec.ExerciseClozeId = exercise.ExerciseId;
                ec.Name = exCloze.Name;
                ec.Text = exCloze.Text;
                ec.Words = exCloze.HiddenWords;
                ec.ChapterId = c.ChapterId;
                ec.LevelId = l.LevelId;
                ec.Level = null;

                try
                {
                    db.ExerciseCloze.Add(ec);
                    db.SaveChanges();
                    creationInfo = "created";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /* Finally, we affect the exercise : if the level is Easy, we affect the exercise to all the class.
             Else, we affect it to some pupils only : the list which we have received. */
            if (ec.LevelId == 1)
            {
                usersIds = null;
                usersIds = this.GetChildrenListIdByClassId(pupil.ClassId);
                ExerciseAffectation(usersIds, exercise.ExerciseId);
            }
            else
            {
                ExerciseAffectation(usersIds, exercise.ExerciseId);
            }

            return creationInfo;
        }

        /// <summary>
        /// Create Or Update a Dictation
        /// It create a new Exercise only if the dictation doesn't exists in the DB
        /// It create a new ExerciseDictation if it doesn't exist in the DB
        /// It Updates a dictation if it doesn't exists in the DB
        /// </summary>
        /// <param name="dictationData"></param>
        /// <returns></returns>
        public JsonResult SaveDictation(ExerciseDictationData dictationData)
        {
            // The Message that will be in the Json
            string message = "";

            //The Exercise that will be saved in the DB if it's the first time.
            ExerciseDictation dictationExo = new ExerciseDictation();

            // The list which will contain the users' IDs sent from the dictationData Object recieved in the parameter
            List<int> usersIds = new List<int>();

            //Affecting users' ids to a list.
            //This will be used for the affectation LATER
            if (dictationData.UsersIds != null)
                usersIds = dictationData.UsersIds.ToList();

            //Here we stock the user's nickname and the text data
            string[] words = dictationData.Text.Split('/');

            //User Nickname is getted here
            string nickname = words[0];

            // Reaffecting correct data on the exercise
            dictationExo.Text = words[1];

            // Affecting the audio data to the Dictation Exercise Entity
            dictationExo.AudioData = dictationData.AudioData;


            IRepository repo = new SQLRepository();

            //Here we get our teacher user from the DataBase (IT CAN'T BE NULL !!!! IMPOSSIBLE !!)
            User user = repo.FindByNickname(nickname);

            #region SaveExercise
            using (var edc = new ExerciseDictationContext())
            {
                dictationExo.Chapter = new Chapter();
                dictationExo.Chapter.ClassId = user.ClassId;
                dictationExo.Chapter.Class = null;
                dictationExo.Chapter.Name = "Dictée";

                using (var sc = new SchoolContext())
                {
                    Chapter chapter = sc.Chapters.Where(c => c.Name.Equals("Dictée"))
                                                 .FirstOrDefault();

                    dictationExo.ChapterId = chapter.ChapterId;
                    dictationExo.LevelId = edc.Level.Where(l => l.Name.Equals(dictationData.Level.Name)).Select(l => l.LevelId).FirstOrDefault();
                    dictationExo.Level = null;
                    dictationExo.Name = "Dictée " + sc.Classes
                                            .Where(cl => cl.ClassId.Equals(dictationExo.Chapter.ClassId))
                                            .Select(cl => cl.Name)
                                            .FirstOrDefault() + edc.Level
                                            .Where(l => l.LevelId.Equals(dictationExo.LevelId))
                                            .Select(l => l.Name)
                                            .FirstOrDefault();
                    dictationExo.Chapter = null;
                }
                ExerciseDictation dictation = edc.ExerciseDictation.Where(exDictation => exDictation.Name.Equals(dictationExo.Name)).FirstOrDefault();

                if (dictation == null)
                {
                    Exercise exercise = new Exercise();
                    int exerciseId = 0;
                    using (ExerciseContext exoContext = new ExerciseContext())
                    {
                        ExerciseType exoType = exoContext.ExerciseTypes.Where(exType => exType.Name.Equals("Dictée")).FirstOrDefault();
                        exercise.ExerciseTypeId = exoType.ExerciseTypeId;
                        exoContext.Exercises.Add(exercise);

                        exoContext.SaveChanges();
                        exerciseId = exercise.ExerciseId;
                    }

                    //Then we save the Exercise Plug
                    dictationExo.ExerciseDictationId = exerciseId;
                    edc.ExerciseDictation.Add(dictationExo);
                    edc.SaveChanges();

                    //Finally we affect the exercise to
                    if (dictationExo.LevelId.Equals(1))
                    {
                        usersIds = null;
                        usersIds = repo.GetChildrenListIdByClassId(user.ClassId);
                    }
                    ExerciseAffectation(usersIds, exerciseId);

                    message = "Jeu enregistré";
                }
                else
                {
                    ExerciseDictation refExoDictation = new ExerciseDictation();
                    refExoDictation = edc.ExerciseDictation.Where(ex => ex.Name.Equals(dictationExo.Name)).FirstOrDefault();

                    if (dictationExo.LevelId.Equals(1))
                        usersIds = null;
                    usersIds = repo.GetChildrenListIdByClassId(user.ClassId);

                    ExerciseAffectation(usersIds, refExoDictation.ExerciseDictationId);
                    dictation.Text = dictationExo.Text;

                    if (dictationExo.AudioData != null)
                        dictation.AudioData = dictationExo.AudioData;

                    //3. Mark entity as modified
                    edc.Entry(dictation).State = System.Data.Entity.EntityState.Modified;
                    //4. call SaveChanges
                    edc.SaveChanges();
                    message = "Texte mis à jour.";
                }
            }
            JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return data;
            #endregion
        }

        public JsonResult SaveBattleCard(ExerciseBattleCardData exoBattleCardData)
        {
            ExerciseBattleCard battleCardExo = new ExerciseBattleCard();
            List<int> usersIds = new List<int>();
            //Affecting users' ids to a list.
            //This will be used for the affectation LATER
            if (exoBattleCardData.UsersIds != null)
                usersIds = exoBattleCardData.UsersIds.ToList();

            string message = "";

            string[] words = exoBattleCardData.ChoiceData.Split('/');

            //User Nickname is getted here
            string nickname = words[0];

            // Reaffecting correct data on the exercise
            battleCardExo.Choice = words[1];


            IRepository repo = new SQLRepository();
            User user = repo.FindByNickname(nickname);

            #region SaveExercise
            using (var exoBattleCardContext = new ExerciseBattleCardContext())
            {
                using (var uc = new UserContext())
                {
                    battleCardExo.Chapter = new Chapter();
                    battleCardExo.Chapter.ClassId = user.ClassId;
                    battleCardExo.Chapter.Class = null;
                }

                battleCardExo.Chapter.Name = "Chapitre 1";

                using (var sc = new SchoolContext())
                {
                    Chapter chapter = sc.Chapters.Where(c => c.Name.Equals(battleCardExo.Chapter.Name))
                                                 .FirstOrDefault();

                    battleCardExo.ChapterId = chapter.ChapterId;
                    battleCardExo.LevelId = exoBattleCardContext.Level.Where(l => l.Name.Equals(exoBattleCardData.Level.Name)).Select(l => l.LevelId).FirstOrDefault();
                    battleCardExo.Level = null;
                    battleCardExo.Name = "BattleCard " + sc.Classes
                                            .Where(cl => cl.ClassId.Equals(battleCardExo.Chapter.ClassId))
                                            .Select(cl => cl.Name)
                                            .FirstOrDefault() + exoBattleCardContext.Level
                                            .Where(l => l.LevelId.Equals(battleCardExo.LevelId))
                                            .Select(l => l.Name)
                                            .FirstOrDefault();
                    battleCardExo.Chapter = null;

                }
                //ExerciseBattleCard battleCard = exoBattleCardContext.ExerciseBattleCard.Where(ebattleCard => ebattleCard.Name.Equals(battleCardExo.Name) && ebattleCard.Level.Name.Equals(battleCardExo.Level.Name)).FirstOrDefault();
                ExerciseBattleCard battleCard = exoBattleCardContext.ExerciseBattleCard.Where(ebattleCard => ebattleCard.Name.Equals(battleCardExo.Name)).FirstOrDefault();
                if (battleCard == null)
                {
                    Exercise exercise = new Exercise();
                    int exerciseId = 0;
                    using (ExerciseContext exoContext = new ExerciseContext())
                    {
                        ExerciseType exoType = exoContext.ExerciseTypes.Where(exType => exType.Name.Equals("CardGame")).FirstOrDefault();
                        exercise.ExerciseTypeId = exoType.ExerciseTypeId;
                        exoContext.Exercises.Add(exercise);

                        exoContext.SaveChanges();
                        exerciseId = exercise.ExerciseId;
                    }

                    //Then we save the Exercise Plug
                    battleCardExo.ExerciseBattleCardId = exerciseId;
                    exoBattleCardContext.ExerciseBattleCard.Add(battleCardExo);
                    exoBattleCardContext.SaveChanges();

                    //Finally we affect the exercise to
                    if (battleCardExo.LevelId.Equals(1))
                    {
                        usersIds = null;
                        usersIds = repo.GetChildrenListIdByClassId(user.ClassId);
                    }
                    ExerciseAffectation(usersIds, exerciseId);
                    message = "Jeu enregistré";
                }
                // If the exercise was already in bdd, update the data
                else
                {
                    ExerciseBattleCard refExoBattleCard = new ExerciseBattleCard();

                    refExoBattleCard = exoBattleCardContext.ExerciseBattleCard.Where(ex => ex.Name.Equals(battleCardExo.Name)).FirstOrDefault();

                    ExerciseAffectation(usersIds, refExoBattleCard.ExerciseBattleCardId);
                    battleCard.Choice = battleCardExo.Choice;

                    //3. Mark entity as modified
                    exoBattleCardContext.Entry(battleCard).State = System.Data.Entity.EntityState.Modified;
                    //4. call SaveChanges
                    exoBattleCardContext.SaveChanges();
                    message = "Texte mis à jour.";
                }
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }
            #endregion
        }

        /// <summary>
        /// Affects a specific exercise to on or multiple users
        /// </summary>
        /// <param name="usersIds"></param>
        /// <param name="exerciseId"></param>
        private void ExerciseAffectation(List<int> usersIds, int exerciseId)
        {
            using (ExerciseContext exerciseContext = new ExerciseContext())
            {
                for (int i = 0; i < usersIds.Count(); i++)
                {
                    ExerciseAffectation exerciseAffectation = new ExerciseAffectation();
                    exerciseAffectation.UserId = usersIds[i];
                    int id = usersIds[i];
                    exerciseAffectation.ExerciseId = exerciseId;
                    exerciseAffectation.CreationDate = DateTime.Now;
                    exerciseAffectation.FirstViewDate = exerciseAffectation.CreationDate;
                    exerciseAffectation.EndDate = DateTime.Now;

                    //We look if the affectation already exist or no
                    ExerciseAffectation exA = exerciseContext.ExercisesAffectations.Where(e => e.UserId.Equals(id)).Where(e => e.ExerciseId.Equals(exerciseId)).FirstOrDefault();
                    if (exA == null)
                    {
                        exerciseContext.ExercisesAffectations.Add(exerciseAffectation);
                        exerciseContext.SaveChanges();
                    }
                }
            }
        }

        public List<ExerciseAffectation> GetExerciseAffectationListByUserId(int id)
        {
            List<ExerciseAffectation> affectations = new List<ExerciseAffectation>();
            using (ExerciseContext exoContext = new ExerciseContext())
            {
                affectations = exoContext.ExercisesAffectations.Where(eA => eA.UserId.Equals(id)).ToList();
            }
            return affectations;
        }

        public List<ExerciseDictation> GetExerciseDictationListById(List<int> IDs)
        {
            List<ExerciseDictation> affectedDictations = new List<ExerciseDictation>();

            using (ExerciseDictationContext exoDictationContext = new ExerciseDictationContext())
            {
                exoDictationContext.Configuration.LazyLoadingEnabled = false;
                //allDictations = exoDictationContext.ExerciseDictation.ToList();
                for (int i = 0; i < IDs.Count(); i++)
                {
                    ExerciseDictation exoDictation = new ExerciseDictation();
                    int id = IDs[i];
                    exoDictation = exoDictationContext.ExerciseDictation.Where(eD => eD.ExerciseDictationId.Equals(id)).FirstOrDefault();
                    affectedDictations.Add(exoDictation);
                }

            }
            return affectedDictations;
        }


        public List<ExerciseBattleCard> GetExerciseBattleCardListById(List<int> IDs)
        {
            List<ExerciseBattleCard> affectedBattleCard = new List<ExerciseBattleCard>();

            using (ExerciseBattleCardContext exoBattleCardContext = new ExerciseBattleCardContext())
            {
                exoBattleCardContext.Configuration.LazyLoadingEnabled = false;
                for (int i = 0; i < IDs.Count(); i++)
                {
                    ExerciseBattleCard exoBattleCard = new ExerciseBattleCard();
                    int id = IDs[i];
                    exoBattleCard = exoBattleCardContext.ExerciseBattleCard.Where(eBC => eBC.ExerciseBattleCardId.Equals(id)).FirstOrDefault();
                    if (exoBattleCard != null)
                        affectedBattleCard.Add(exoBattleCard);
                }
            }
            return affectedBattleCard;
        }
    }
}