(function () {
    'use strict';
    angular.module('TheApp').controller('KidSelectExercicesController', function ($scope) {

        $scope.EmptySession = false;
        //$('body').css('background-image', 'url(../Images/magic_kingdomHD.png)');
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
            $scope.Message = 'A quoi veux-tu jouer ?';
            $scope.demo = {
                topDirections: ['left', 'up'],
                bottomDirections: ['down', 'right'],
                isOpen: false,
                availableModes: ['md-fling', 'md-scale'],
                selectedMode: 'md-scale',
                availableDirections: ['up', 'down', 'left', 'right'],
                selectedDirection: 'right'
            };
        }

        $scope.$on('$locationChangeStart', function () {
            $('body').css('background-image', 'url(../Images/backgroundCliffHD.png)');
        });
    })
})();