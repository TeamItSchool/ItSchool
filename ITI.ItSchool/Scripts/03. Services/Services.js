(function () {
    'use strict';
    angular.module('TheApp')

        //Battle Card Service
        .service('battleCardService', function () {
            var choice = "";

            var addChoice = function (dataChoice) {
                choice = dataChoice;
            };

            var getChoice = function () {
                return choice;
            };

            return {
                addChoice: addChoice,
                getChoice: getChoice
            };
        })
})();