(function () {
    'use strict';
    angular.module('TheApp').controller('KidLobbyController', function ($scope, LoginService) {

        var monobjet_json = sessionStorage.getItem("objet");
        var monobjet = JSON.parse(monobjet_json);
        // Affichage dans la console
        console.log(monobjet.data.FirstName);
        $scope.Message = "Bonjour " + monobjet.data.Nickname;
    })
})();