(function () {
    'use strict';
    angular.module('TheApp').controller('KidHomeController', function ($scope) {

        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");
        $scope.Message = "Embarque dans l'aventure It'School :)";
        $scope.GoBack = function () {
            window.history.back();
        };
        $scope.demo = {
            topDirections: ['left', 'up'],
            bottomDirections: ['down', 'right'],
            isOpen: false,
            availableModes: ['md-fling', 'md-scale'],
            selectedMode: 'md-scale',
            availableDirections: ['up', 'down', 'left', 'right'],
            selectedDirection: 'right'
        };
    })

})();