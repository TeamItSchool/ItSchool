(function () {
    'use strict';
    angular.module('TheApp').controller('HomeController', function ($scope) {
        if (sessionStorage.getItem("objet") != null)
            sessionStorage.removeItem("objet");
        $scope.Message = "Un voyage vers l'apprentissage";
    })
})();