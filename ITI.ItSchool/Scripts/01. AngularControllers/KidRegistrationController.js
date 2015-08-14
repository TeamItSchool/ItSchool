(function () {
    'use strict';
    angular.module('TheApp').controller("KidRegistrationController", function ($scope, RegistrationService, LoginService) {
        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");
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
                            $scope.Message = "Les terres de Schola attendent leur héros";
                            $scope.IsLogedIn = false;
                            $scope.ShowSpin = false;
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
                                    $scope.ShowSpin = true;
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
})();