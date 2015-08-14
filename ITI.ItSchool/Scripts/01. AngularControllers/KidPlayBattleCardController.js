// Play BattleCard
(function () {
    'use strict';
    angular.module('TheApp').controller('KidPlayBattleCardController', function ($scope, battleCardService, $localStorage) {

        var monobjet_json = sessionStorage.getItem("objet");
        var monobjet = JSON.parse(monobjet_json);
        // Affichage dans la console
        console.log(monobjet.data.FirstName + " est dans la page de jeu battleCard");

        if (battleCardService.getChoice() != "") {
            console.log('battleCardService.getChoice() different de vide ')
            $localStorage.choiceData = battleCardService.getChoice();
        }

        $scope.choice = $localStorage.choiceData;
        console.log($scope.choice);

        $scope.Time = 'Vous avez 1 minute ! ';
        $scope.Score = 0
        $scope.svgCard = "/Images/redCard.svg";

        loadBattleCardGame($scope.choice);

        $scope.$on('$locationChangeStart', function () {
            stopClock();
        });
    })

})();