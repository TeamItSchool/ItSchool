(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherHomeController', function ($scope) {

        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");
        $scope.Message = 'Page "Professeurs"';
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
            window.history.back();
        };
    })

})();