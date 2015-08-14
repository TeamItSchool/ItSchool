(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherLoginController', function ($scope, LoginService) {

        sessionStorage.removeItem("objet");

        $scope.Message = "Entrez vos identifiants pour vous connecter.";
        $scope.IsLogedIn = false;
        $scope.ShowSpin = false;
        $scope.Submitted = false;
        $scope.IsFormValid = false;
        $scope.IsKid = false;
        $scope.ButtonMessage = "Connexion";

        $scope.demo = {
            topDirections: ['left', 'up'],
            bottomDirections: ['down', 'right'],
            isOpen: false,
            availableModes: ['md-fling', 'md-scale'],
            selectedMode: 'md-scale',
            availableDirections: ['up', 'down', 'left', 'right'],
            selectedDirection: 'right'
        };

        $scope.GoBack = function () {
            location.reload();
        };

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
                $scope.ShowSpin = true;
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
                        $scope.Message = "Bienvenue " + d.data.FirstName + " " + d.data.LastName;
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

})();