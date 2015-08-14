(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherSelectExercicesController', function ($scope) {

        $scope.EmptySession = false;
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
            $scope.Message = 'Selectionnez un exercice à modifier';
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
    })
})();