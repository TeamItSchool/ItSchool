(function () {
    'use strict';
    angular.module('TheApp').controller('KidLoginController', function ($scope, LoginService) {

        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");

        $scope.Message = "Les terres de Schola attendent leur héros";
        $scope.IsLogedIn = false;
        $scope.ShowSpin = false;
        $scope.Submitted = false;
        $scope.IsFormValid = false;
        $scope.IsTeacher = false;
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
                        alert("Attention, vous vous trouvez actuellement dans l'espace des élèves.\nVeuillez cliquez sur le bouton 'Espace Professeur'.");
                        $scope.Message = "Nous vous invitons à rejoindre l'espace 'Professeur'";
                        $scope.IsTeacher = true;
                    }
                    else {
                        $scope.LoginData.Username = "";
                        $scope.LoginData.Password = "";
                        alert("Aïe.. Quelque chose s'est mal passée. Vois avec tes parents ou ton professeur.")
                    }

                    console.log(d.data);
                })
            }
        };
    })
})();