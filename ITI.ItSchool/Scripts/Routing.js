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
    .when('/teacher/ClozeExercise', {
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
            LoginService.GetUser($scope.LoginData).then(function (d) {
                if (d.data.Nickname != null) {
                    $scope.IsLogedIn = true;
                    $scope.Message = "Bienvenue " + d.data.FirstName;
                }
                else {
                    alert('Oops tu as entré le mauvais pseudo ou le mauvais mot de passe. Réessye pour te connecter.')
                }
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
        Mail: ''
    };

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
                                LoginService.GetUser($scope.LoginData).then(function (d) {
                                    if (d.data.Nickname != null) {
                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Bienvenue " + d.data.FirstName;
                                    }
                                    else {
                                        alert('Oops tu as entré le mauvais pseudo ou le mauvais mot de passe. Réessye pour te connecter.')
                                    }
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
            LoginService.GetUser($scope.LoginData).then(function (d) {
                $scope.ButtonMessage = "Connexion en cours..";
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
})

.controller("TeacherClozeExerciseController", function ($scope, ExerciseDatas) {
    $scope.Message = 'Configuration de l\'exercice';
    $scope.Button = '15';
    $scope.IsFormValid = false;
    $scope.test = '';

    //Mettre toutes les propriétés du Game
    
    $scope.Game = {
        //A REMPLIR
        Data: '',
        Level: {
            Name: 'Tatu',
            Remarks: 'Test level Remarks'
        },
        Remarks: 'Toto'
    };

    $scope.$watch('ClozeExercise', function (newValue) {
        $scope.IsFormValid = newValue;
    });

    $scope.SaveData = function () {
        console.log( "Là : " + ExerciseDatas + "L240: " + $scope.Game.Level );
        if ($scope.IsFormValid) {
            ExerciseDatas.GetExerciseDatas($scope.Game).then(function (data) {
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
    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: ''
    };

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
    $scope.Message = 'Selectionnez un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    $scope.DictationText = {
        Text: '',
        Level: ''
    };

    //Check if Form is valid or not // here DictText is our form Name
    $scope.$watch('DictText.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveText = function () {
        if ($scope.IsFormValid) {
            $scope.Button = "Sauvegarde en cours..."
            $scope.DictationText.Text.trim();
            SaveDictationText.GetText($scope.DictationText).then(function (d) {
                $scope.Button = "Dictée sauvegardée";
            })
        }
    };    

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau facile)";
        $scope.DictationText.Level = "Easy";
    }
    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau moyen)";
        $scope.DictationText.Level = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau difficile)";
        $scope.DictationText.Level = "Hard";
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
        UserName: '',
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
    return fac;
});

.factory('ExerciseDatas', function ($http) {
    var fac = {};
    var data = "";
    fac.GetExerciseDatas = function (d) {
        return $http({
            url: '/Data/GetExerciseDatas',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
});