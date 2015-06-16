angular.module('TheApp', ['ngRoute', 'ngMaterial']) // ['ngRoute'] is required for the routing and
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
        templateUrl: '/Templates/TeacherCustomizeBattleCardPage.html',
        controller: 'TeacherBattleCardController'
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
    .otherwise({   // This is when any route not matched
        templateUrl: '/Templates/Error.html',
        controller: 'ErrorController'
    })
    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})
.controller('HomeController', function ($scope) {
    $scope.Message = "Bienvenue";
})
.controller('KidLogInController', function ($scope, LoginService) {
    $scope.Message = "Embarque dans l'aventure It'School !";
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
                if (d.data.Nickname != null) {
                    var monobjet_json = JSON.stringify(d);
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);

                    $scope.IsLogedIn = true;
                    $scope.Message = "Bienvenue " + d.data.FirstName;
                }
                else {
                    alert("Oops tu as entré le mauvais pseudo ou le mauvais mot de passe. Réessye pour te connecter.")
                }

                console.log(d.data.FirstName + " " + d.data.LastName);
            })
        }
    };
})
.controller("KidRegistrationController", function ($scope, RegistrationService, LoginService) {
    $scope.submitText = "Inscription";
    $scope.IsRegistered = false;
    $scope.submitted = false;
    $scope.Message = "Inscris-toi sur It'School :)";
    $scope.message = "";
    $scope.IsFormValid = false;

    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: '',
        Grade: {
            Name: 'Classe'
        },
        Group: {
            Name: 'Élèves'
        }
    };

    $scope.Grades = null;

    RegistrationService.GetGrades().then(function (d) {
        $scope.Grades = d.data;
        //alert($scope.Grades[0].Name);
    }, function (error) {
        alert('Error on grades');
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
                                    if (d.data.Nickname != null) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Bienvenue " + d.data.FirstName;
                                    }
                                    else {
                                        alert("Oops tu as entré le mauvais pseudo ou le mauvais mot de passe. Réessye pour te connecter.")
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
.controller('KidPlayDictationController', function ($scop, CheckDictationText) {
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
.controller('KidLoginController', function ($scope) {
    $scope.Message = "Entre le pseudo et le mot de passe que tu avais choisis.";
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
                if (d.data.Nickname != null) {
                    var monobjet_json = JSON.stringify(d);
                    /*d.data.Grade = {
                        Name: "Oui"
                    }
                    document.write(d.data.Grade.Name);*/
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);
                    
                    $scope.IsLogedIn = true;
                    $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                }
                else {
                    alert("Votre pseudo ou votre mot de passe sont incorrects. Veuillez réessayer s'il vous plaît")
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
        },
        Remarks: 'Test remark...'
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
        Grade: {
            Name: 'Classe'
        },
        Group: {
            Name: 'Professeurs'
        }
    };

    $scope.Grades = null;

    RegistrationService.GetGrades().then(function (d) {
        $scope.Grades = d.data;
        //alert($scope.Grades[0].Name);
    }, function (error) {
        alert('Error on grades');
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
                                    if (d.data.Nickname != null) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                                    }
                                    else {
                                        alert("Votre pseudo ou votre mot de passe sont incorrects. Veuillez réessayer s'il vous plaît")
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

    $scope.Message = 'Selectionnez un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    /*$scope.DictationText = {
        Text: '',
        Level: ''
    };*/

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
            $scope.Button = "Sauvegarde en cours..."
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

.controller('TeacherBattleCardController', function ($scope) {
    $scope.Time = 'Vous avez 1 minutes ! ';
    $scope.Score = 0
    $scope.svgCard = '/Images/redCard.svg';
})

.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})
.controller('KidLoginController', function ($scope) {
    $scope.Message = "Entre le pseudo et le mot de passe que tu avais choisis.";
})

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
            alert('Error!');
            defer.reject(e);
        });
        return defer.promise;
    }
    fac.GetGrades = function () {
        return $http.get('/Data/GetGrades')
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
        })
};

    return fac;
});