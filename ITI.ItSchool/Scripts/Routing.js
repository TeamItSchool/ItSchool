﻿angular.module('TheApp', ['ngRoute', 'ngMaterial']) // ['ngRoute'] is required for the routing and
    //['ngMaterial'] for the material design components
.run(function ($log, $rootScope) {
    $log.debug("startApp running");
})
.config(function ($routeProvider, $locationProvider) {
    //here we will write code for implement routing 
    $routeProvider
    .when('/', { // This is for reditect to another route
        redirectTo: function () {
            return '/home';
        }
    })
    .when('/home', {
        templateUrl: '/Templates/Home.html',
        controller: 'HomeController'
    })
    .when('/teacher', {
        templateUrl: '/Templates/TeacherHomePage.html',
        controller: 'TeacherHomeController'
    })
    .when('/teacher/login', {
        templateUrl: '/Templates/TeacherLoginPage.html',
        controller: 'TeacherLoginController'
    })
    .when('/teacher/registration', {
        templateUrl: '/Templates/TeacherRegistrationPage.html',
        controller: 'TeacherRegistrationController'
    })
    .when('/teacher/lobby', {
        templateUrl: '/Templates/TeacherLobbyPage.html',
        controller: 'TeacherLobbyController'
    })
    .when('/teacher/exercices', {
        templateUrl: '/Templates/TeacherSelectExercicesPage.html',
        controller: 'TeacherSelectExercicesController'
    })
    .when('/teacher/exercices/cloze_exercise', {
        templateUrl: '/Templates/TeacherCustomizeClozeExercisePage.html',
        controller: 'TeacherClozeExerciseController'
    })
    .when('/teacher/exercices/drag_drop', {
        templateUrl: '/Templates/TeacherCustomizeDragAndDropPage.html',
        controller: 'TeacherDragAndDropController'
    })
    .when('/teacher/exercices/dictation', {
        templateUrl: '/Templates/TeacherCustomizeDictationPage.html',
        controller: 'TeacherDictationController'
    })
    .when('/teacher/exercices/battleCard', {
        templateUrl: '/Templates/TeacherDescriptionBattleCardPage.html',
        controller: 'TeacherDescriptionBattleCardController'
    })
    .when('/teacher/exercices/battleCard/customize', {
        templateUrl: '/Templates/TeacherCustomizeBattleCardPage.html'
        //controller: 'TeacherCustomizeBattleCardController'
    })
    .when('/kid', {
        templateUrl: '/Templates/KidHomePage.html',
        controller: 'KidHomeController'
    })
    .when('/kid/login', {
        templateUrl: '/Templates/KidLoginPage.html',
        controller: 'KidLoginController'
    })
    .when('/kid/registration', {
        templateUrl: '/Templates/KidRegistrationPage.html',
        controller: 'KidRegistrationController'
    })
    .when('/kid/lobby', {
        templateUrl: '/Templates/KidLobbyPage.html',
        controller: 'KidLobbyController'
    })
    .when('/kid/exercices', {
        templateUrl: '/Templates/KidSelectExercicesPage.html',
        controller: 'KidSelectExercicesController'
    })
    .when('/kid/exercices/dictation', {
        templateUrl: '/Templates/KidPlayDictationPage.html',
        controller: 'KidPlayDictationController'
    })
    .when('/kid/exercices/battleCard', {
        templateUrl: '/Templates/KidDescriptionBattleCardPage.html',
        controller: 'KidDescriptionBattleCardController'
    })
    .when('/kid/exercices/battleCard/play', {
        templateUrl: '/Templates/KidPlayBattleCardPage.html',
        controller: 'KidPlayBattleCardController'
    })
    .otherwise({   // This is when any route not matched
        templateUrl: '/Templates/Error.html',
        controller: 'ErrorController'
    })
    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})
.controller('HomeController', function ($scope) {
    $scope.Message = "Bienvenue";
})
.controller('KidLoginController', function ($scope, LoginService) {

    sessionStorage.removeItem("objet");

    $scope.Message = "Embarque dans l'aventure It'School :)";
    $scope.IsLogedIn = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.IsTeacher = false;
    $scope.ButtonMessage = "Connexion";

    $scope.LoginData = {
        Username: '',
        Password: ''
    };
    //Check if Form is valid or not // here LoginForm is our form Name
    $scope.$watch('LoginForm.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.Login = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            $scope.ButtonMessage = "Connexion en cours..";
            LoginService.GetUser($scope.LoginData).then(function (d) {
                if (d.data.Username == null && d.data.Nickname == null) {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Oops, tu as entré un mauvais pseudo ou il n'existe pas. Réessaie ou inscris-toi si tu n'es pas inscrit.");
                } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                    $scope.LoginData.Password = "";
                    alert("Oops, tu as entré un mauvais mot de passe. Réessaye.");
                }
                else if (d.data.Nickname != null && d.data.Group.Name == "Élèves" && d.data.Password == $scope.LoginData.Password) {
                    var monobjet_json = JSON.stringify(d);
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);

                    $scope.IsLogedIn = true;
                    $scope.Message = "Bienvenue " + d.data.FirstName;
                } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs") {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Attention, vous vous trouvez actuellement dans l'espace des élèves.\n Veuillez cliquez sur le bouton 'Espace Professeur'.");
                    $scope.Message = "Rejoins l'espace 'Enfant'";
                    $scope.IsTeacher = true;
                }
                else {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Aïe.. Quelque chose s'est mal passé. vois avec tes parents ou ton professeur.")
                }

                console.log(d.data);
            })
        }
    };
})
.controller("KidRegistrationController", function ($scope, RegistrationService, LoginService) {
    $scope.submitText = "Inscription";
    $scope.IsRegistered = false;
    $scope.submitted = false;
    $scope.Message = "Inscris-toi sur It'School ! :)";
    $scope.message = "";
    $scope.IsFormValid = false;

    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: '',
        Class: {
            Name: ''
        },
        Group: {
            Name: 'Élèves'
        }
    };

    $scope.Classes = null;

    RegistrationService.GetClasses().then(function (d) {
        $scope.Classes = d.data;
        //alert($scope.Grades[0].Name);
    }, function (error) {
        alert('Error on Classes 151');
    });

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Inscription") {
            $scope.submitted = true;
            $scope.message = "";

            if ($scope.IsFormValid) {
                $scope.submitText = "Patience..";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Le compte a bien été créé.') {
                        $scope.IsRegistered = true;
                        $scope.Message = "Embarque dans l'aventure It'School !";
                        $scope.IsLogedIn = false;
                        $scope.Submitted = false;
                        $scope.IsFormValid = false;
                        $scope.IsTeacher = false;

                        $scope.LoginData = {
                            Username: '',
                            Password: ''
                        };
                        //Check if Form is valid or not // here f1 is our form Name
                        $scope.$watch('LoginForm.$valid', function (newVal) {
                            $scope.IsFormValid = newVal;
                        });
                        $scope.Login = function () {
                            $scope.Submitted = true;
                            if ($scope.IsFormValid) {
                                $scope.ButtonMessage = "Connexion en cours..";
                                LoginService.GetUser($scope.LoginData).then(function (d) {
                                    if (d.data.Username == null && d.data.Nickname == null) {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as entré un mauvais pseudo ou il n'existe pas. Réessaie ou inscris-toi si tu n'es pas inscrit.");
                                    } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as entré un mauvais mot de passe. Réessaye.");
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves" && d.data.Password == $scope.LoginData.Password) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Bienvenue " + d.data.FirstName + " " + d.data.Group.Name;
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs") {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Attention, vous vous trouvez actuellement dans l'espace des élèves.\n Veuillez cliquez sur le bouton 'Espace Professeur'.");
                                        $scope.Message = "Rejoins l'espace 'Enfant'";
                                        $scope.IsTeacher = true;
                                    }
                                    else {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as du rentrer un mauvais pseudo ou un mauvais mot de passe. Réessaye de te connecter.")
                                    }

                                    console.log(d.data.FirstName + " " + d.data.LastName);
                                })
                            }
                        };
                        //Have to clear form here
                        //ClearForm();
                    }
                    $scope.submitText = "Inscription";
                });
            }
            else {
                $scope.message = 'Il faut bien tout remplir :)';
            }
        }
    }
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})
.controller('KidLobbyController', function ($scope, LoginService) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName);
    $scope.Message = "Bonjour " + monobjet.data.FirstName;

})
.controller('KidSelectExercicesController', function ($scope) {
    $scope.Message = 'A quoi veux-tu jouer ?';
})
.controller('KidPlayDictationController', function ($scope, CheckDictationText) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de la dictée");

    $scope.Message = 'Sélectionne un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Valider";

    $scope.Game = {
        //A REMPLIR
        Data: '',
        Level: {
            Name: 'Test'
        },
        ExerciseType: {
            Name: 'Dictation'
        }
    };

    //Check if Form is valid or not // here DictText is our form Name
    $scope.$watch('DictText.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveText = function () {
        if ($scope.IsFormValid) {
            $scope.Button = "Validtion en cours..."
            $scope.Game.Data.trim();
            $scope.Game.Data = monobjet.data.Nickname + "/" + $scope.Game.Data;
            SaveDictationText.GetText($scope.Game).then(function (d) {
                $scope.Button = "Dictée sauvegardée";
            })
        }
    };

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau facile)";
        $scope.Game.Level.Name = "Easy";
    }
    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau moyen)";
        $scope.Game.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau difficile)";
        $scope.Game.Level.Name = "Hard";
    }
})
.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})
.controller('TeacherHomeController', function ($scope) {
    $scope.Message = 'Page "Professeurs"';
})
.controller('TeacherLobbyController', function ($scope, LoginService) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName);
    $scope.Message = "Bonjour " + monobjet.data.FirstName + " " + monobjet.data.LastName;

})
.controller('TeacherLoginController', function ($scope, LoginService) {

    sessionStorage.removeItem("objet");

    $scope.Message = "Entrez vos identifiants pour vous connecter.";
    $scope.IsLogedIn = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.IsKid = false;
    $scope.ButtonMessage = "Connexion";

    $scope.LoginData = {
        Username: '',
        Password: ''
    };
    //Check if Form is valid or not // here LoginForm is our form Name
    $scope.$watch('LoginForm.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.Login = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            $scope.ButtonMessage = "Connexion en cours..";
            LoginService.GetUser($scope.LoginData).then(function (d) {
                if (d.data.Username == null && d.data.Nickname == null) {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Vous avez entré un mauvais pseudo ou celui-ci n'existe pas. Veuillez réessayer ou vous inscrire si ce n'est pas le déjà le cas.");
                } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                    $scope.LoginData.Password = "";
                    alert("Vous avez dû entrer un mauvais mot de passe. Veuillez réessayer.");
                } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs" && d.data.Password == $scope.LoginData.Password) {
                    var monobjet_json = JSON.stringify(d);
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);

                    $scope.IsLogedIn = true;
                    $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves") {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Oops, tu es dans l'espace des professeurs. Vas dans l'espace des élèves pour te connecter.");
                    $scope.Message = "Rejoins l'espace 'Enfant'";
                    $scope.IsKid = true;
                }
                else {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Une erreur s'est produite. Veuillez contacter le service technique.")
                }

                console.log(d.data.FirstName + " " + d.data.LastName);
            })
        }
    };
})
.controller("TeacherClozeExerciseController", function ($scope, ExerciseDatas) {
    $scope.Message = 'Configuration de l\'exercice';
    $scope.Button = 'Sauvegarder';
    $scope.IsFormValid = false;
    $scope.test = '';

    //Mettre toutes les propriétés du Game
    
    $scope.Game = {
        //A REMPLIR
        Data: '',
        Level: {
            Name: '',
            Remarks: ''
        }
    };

    $scope.$watch('ClozeExercise', function (newValue) {
        $scope.IsFormValid = newValue;
    });

    $scope.SaveData = function () {
        console.log( "Là : " + ExerciseDatas + "L240: " + $scope.Game.Level );
        if ($scope.IsFormValid) {
            ExerciseDatas.SaveClozeExercise($scope.Game).then(function (data) {
                console.log("SaveData");
            });
        }
    };
})
.controller("TeacherRegistrationController", function ($scope, RegistrationService, LoginService) {

    sessionStorage.removeItem("objet");
    $scope.submitText = "Inscription";
    $scope.submitted = false;
    $scope.IsRegistered = false;
    $scope.Message = "Inscription";
    $scope.message = "";
    $scope.IsFormValid = false;
    $scope.ButtonMessage = "Connexion";

    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: '',
        Class: {
            Name: ''
        },
        Group: {
            Name: 'Professeurs'
        }
    };

    $scope.Classes = null;

    RegistrationService.GetClasses().then(function (d) {
        $scope.Classes = d.data;
        //alert($scope.Grades[0].Name);
    }, function (error) {
        alert('Error on classes 424');
    });

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Inscription") {
            $scope.submitted = true;
            $scope.message = "";
            if ($scope.IsFormValid) {
                $scope.submitText = "Veuillez patienter s'il vous plaît";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Le compte a bien été créé.') {
                        $scope.IsRegistered = true;

                        $scope.Message = "Entrez vos identifiants pour vous connecter.";
                        $scope.IsLogedIn = false;
                        $scope.Submitted = false;
                        $scope.IsFormValid = false;

                        $scope.LoginData = {
                            Username: '',
                            Password: ''
                        };
                        //Check if Form is valid or not // here f1 is our form Name
                        $scope.$watch('LoginForm.$valid', function (newVal) {
                            $scope.IsFormValid = newVal;
                        });
                        $scope.Login = function () {
                            $scope.Submitted = true;
                            if ($scope.IsFormValid) {
                                $scope.ButtonMessage = "Connexion en cours..";
                                LoginService.GetUser($scope.LoginData).then(function (d) {
                                    if (d.data.Username == null && d.data.Nickname == null) {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Vous avez entré un mauvais pseudo ou celui-ci n'existe pas. Veuillez réessayer ou vous inscrire si ce n'est pas le déjà le cas.");
                                    } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                                        $scope.LoginData.Password = "";
                                        alert("Vous avez dû entrer un mauvais mot de passe. Veuillez réessayer.");
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs" && d.data.Password == $scope.LoginData.Password) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves") {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu es dans l'espace des professeurs. Vas dans l'espace des élèves pour te connecter.");
                                        $scope.Message = "Rejoins l'espace 'Enfant'";
                                        $scope.IsKid = true;
                                    }
                                    else {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Une erreur s'est produite. Veuillez contacter le service technique.")
                                    }

                                    console.log(d.data.FirstName + " " + d.data.LastName);
                                })
                            }
                        };
                        //Have to clear form here
                        //ClearForm();
                    }
                    $scope.submitText = "Inscription";
                });
            }
            else {
                $scope.message = 'Remplissez bien tous les champs.';
            }
        }
    }
    //Clear form (reset)
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})
.controller('TeacherSelectExercicesController', function ($scope) {
    $scope.Message = 'Selectionnez un exercice à modifier';
})
.controller('TeacherDictationController', function ($scope, SaveDictationText) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de la dictée");
    console.log("Sa classe est : " + monobjet.data.Class.Name);

    $scope.Message = 'Selectionnez un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    $scope.ExerciseDictation = {
        //A REMPLIR
        Text: '',
        Level: {
            Name: 'Test'
        },
        ExerciseType: {
            Name: 'Dictée'
        }
    };

    //Check if Form is valid or not // here DictText is our form Name
    $scope.$watch('DictText.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveText = function () {
        if ($scope.IsFormValid) {
            $scope.Button = "Sauvegarde en cours..."
            $scope.ExerciseDictation.Text.trim();
            $scope.ExerciseDictation.Text = monobjet.data.Nickname + "/" + $scope.ExerciseDictation.Text;
            var res = $scope.ExerciseDictation.Text.split("/");
            SaveDictationText.GetText($scope.ExerciseDictation).then(function (d) {
                $scope.ExerciseDictation.Text = res[1];
                console.log(d.data);
                if (d.data == "Jeu enregistré")
                    $scope.Button = "Dictée sauvegardée";
                else {
                    alert(d.data);
                    $scope.Button = "Sauvegarder"
                }
            })
        }
    };    

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau facile)";
        $scope.ExerciseDictation.Level.Name = "Easy";
    }
    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau moyen)";
        $scope.ExerciseDictation.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau difficile)";
        $scope.ExerciseDictation.Level.Name = "Hard";
    }
})
.factory('SaveDictationText', function ($http) {
    var fac = {};
    var data = "";
    fac.GetText = function (d) {
        return $http({
            url: '/Data/SaveDictation',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})
.controller('TeacherDragAndDropController', function ($scope, SaveDragAndDropExercice) {
    $scope.Message = "Modifiez à vos souhaits l'exercice du 'Glissé-Déposé'";
    $scope.card1 = "1";
    $scope.card2 = "2";
    $scope.card3 = "3";
    $scope.card4 = "4";
    $scope.card5 = "5";
    $scope.card6 = "6";
    $scope.card7 = "7";
    $scope.card8 = "8";
    $scope.card9 = "9";
    $scope.card10 = "10";

    $scope.IsSaved = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;

    $scope.CardsData = {
        Card1: '', Card2: '',
        Card3: '', Card4: '',
        Card5: '', Card6: '',
        Card7: '', Card8: '',
        Card9: '', Card10: ''
    };
    //Check if Form is valid or not // here SaveTeacherDragAndDrop is our form Name
    $scope.$watch('SaveDragAndDropTeacher.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });

    $scope.Save = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            LoginService.GetUser($scope.CardsData).then(function (d) {
                if (d.data.Card1 != null) {
                    $scope.IsSaved = true;
                    $scope.Message = "Yup";
                }
                else {
                    alert('Woops')
                }
            })
        }
    };
})
.factory('SaveDragAndDropExercice', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUser = function (d) {
        return $http({
            url: '/Data/SaveDragAndDropTeacher',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})

// Description customize BattleCard
.controller('TeacherDescriptionBattleCardController', function ($scope) {

})

// Customize BattleCard
.controller('TeacherCustomizeBattleCardController', function ($scope, SaveBattleCardChoice) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de card game");
    console.log("ClassId : " + monobjet.data.ClassId)

    $scope.Message = 'Choix du niveau';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    $scope.ExerciseBattleCard = {
        //A REMPLIR
        Choice: '',
        Level: {
            Name: 'Test'
        },
        ExerciseType: {
            Name: 'CardGame'
        },
        Users :''
    };

    $scope.Children = null;
    $scope.selected = [];

    SaveBattleCardChoice.GetUsers(monobjet.data.ClassId).then(function (d) {
        //console.log("test");
        //console.log(d);
        //console.log("Taille de la data : " + d.data.length);
        //console.log(d.data);
        //console.log(d.data[0]);
        //console.log(d.data[1]);
        //console.log(d.data[0].FirstName);
        //console.log(d.data[0].LastName);
        //console.log(d.data[0].Nickname);
        //console.log(d.data[0].ClassId);

        $scope.Children = d.data;
        $scope.ExerciseBattleCard.Users = $scope.Children;
        console.log($scope.ExerciseBattleCard.Users);
    });

    $scope.toggle = function (child, list) {
        var idx = list.indexOf(child);
        if (idx > -1)
            list.splice(idx, 1);
        else
            list.push(child);
    };

    $scope.exists = function (child, list) {
        return list.indexOf(child) > -1;
    };

    //Check if Form is valid or not // here FormChoice is our form Name
    $scope.$watch('FormChoice.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveChoice = function () {
        if ($scope.IsFormValid && $scope.ExerciseBattleCard.Choice != "") {
            $scope.Button = "Sauvegarde en cours..."
            $scope.ExerciseBattleCard.Choice.trim();
            $scope.ExerciseBattleCard.Choice = monobjet.data.Nickname + "/" + $scope.ExerciseBattleCard.Choice;
            var res = $scope.ExerciseBattleCard.Choice.split("/");

            if ($scope.ExerciseBattleCard.Level.Name != "Easy") {
                console.log($scope.ExerciseBattleCard.Level.Name);
                $scope.ExerciseBattleCard.Users = $scope.selected;
            }
                

            SaveBattleCardChoice.GetChoice($scope.ExerciseBattleCard).then(function (d) {
                $scope.ExerciseBattleCard.Choice = res[1];
                console.log(d.data);
                if (d.data == "Jeu enregistré")
                    $scope.Button = "Battle Card sauvegardé";
                else {
                    alert(d.data);
                    $scope.Button = "Sauvegarder"
                }
            })
        }
    };
    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Niveau facile";
        $scope.ExerciseBattleCard.Level.Name = "Easy";
    }

    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Niveau moyen";
        $scope.ExerciseBattleCard.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Niveau difficile";
        $scope.ExerciseBattleCard.Level.Name = "Hard";
    }
})

.factory('SaveBattleCardChoice', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUsers = function (data) {
        //return $http.get('/Data/GetClasses')
        return $http.get('/Data/GetUsersByClasses/' + data)
    }
    fac.GetChoice = function (d) {
        return $http({
            url: '/Data/SaveBattleCard',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };
    return fac;
})

// Kid description BattleCard
.controller('KidDescriptionBattleCardController', function ($scope) {

})

// Play BattleCard
.controller('KidPlayBattleCardController', function ($scope) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la page de jeu card game");


    $scope.Time = 'Vous avez 1 minutes ! ';
    $scope.Score = 0
    $scope.svgCard = "/Images/redCard.svg";
})

.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})
//.controller('KidLoginController', function ($scope) {
//    $scope.Message = "Entre le pseudo et le mot de passe que tu avais choisis.";
//})

.controller('ErrorController', function ($scope) {
    $scope.Message = "404 Not Found!";
})

    // INSCRIPTION
.controller('Registration', function ($scope, RegistrationService) {
    //Default Variable
    $scope.submitText = "Save";
    $scope.submitted = false;
    $scope.message = "";
    $scope.IsFormValid = false;
    $scope.User = {
        UserName: 'Steve',
        Password: '', 
        FullName: '',
        EmailID: '',
        Gender: ''
    };

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Save") {
            $scope.submitted = true;
            $scope.message = "";

            if ($scope.IsFormValid) {
                $scope.submitText = "Please wait...";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Success') {
                        //Have to clear form here
                        ClearForm();
                    }
                    $scope.submitText = "Save";
                });
            }
            else {
                $scope.message = 'Please fill required fields value';
            }
        }
    }

    //Clear form (reset)
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})

//Factories
    //Login Factory
.factory('LoginService', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUser = function (d) {
        return $http({
            url: '/Data/UserLogin',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})
.factory('CheckDictationText', function ($http) {
    var fac = {};
    var data = "";
    fac.GetDictationText = function (d) {
        return $http({
            url: '/Data/CheckDictationText',
            method: 'POST',
            data: JSON.stringify(d),
            header: { 'content-type': 'application/json' }
        })
    };
    return fac;
})
    //Registration Factory
.factory('RegistrationService', function ($http, $q) {
    //here q is an angularJS service which helps us to run asynchronous function and return result when processing is done
    var fac = {};
    fac.SaveFormData = function (data) {
        var defer = $q.defer();
        $http({
            url: '/Data/Register',
            method: 'POST',
            data: JSON.stringify(data),
            header: { 'content-type': 'application/json' }
        }).success(function (d) {
            //Success callback
            defer.resolve(d);
        }).error(function (e) {
            //Failed callback
            console.log( "An error occured. Error is : " + e + "Data is : " + JSON.stringify(data) );
            defer.reject(e);
        });
        return defer.promise;
    }
    fac.GetClasses = function () {
        return $http.get('/Data/GetClasses')
    }
    fac.GetGroups = function () {
        return $http.get('/Data/GetGroups')
    }
    return fac;
})

.factory('ExerciseDatas', function ($http) {
    var fac = {};
    var data = "";
    fac.SaveClozeExercise = function (d) {
        return $http({
            url: '/Data/SaveClozeExercise',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            defer.resolve(d);
        }).error(function (e) {
            defert.reject(e);
        });
};

    return fac;
});