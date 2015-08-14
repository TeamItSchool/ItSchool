(function () {
    'use strict';
    angular.module('TheApp').controller("TeacherRegistrationController", function ($scope, RegistrationService, LoginService) {

        console.log(sessionStorage.getItem("objet"));
        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");
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
                            $scope.ShowSpin = false;
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
                                    $scope.ShowSpin = true;
                                    $scope.ButtonMessage = "Connexion en cours...";
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
})();