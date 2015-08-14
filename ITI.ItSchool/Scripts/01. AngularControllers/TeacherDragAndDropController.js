(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherDragAndDropController', function ($scope, SaveDragAndDropExercice) {

        $scope.Message = "Modifiez à vos souhaits l'exercice du 'Glissé-Déposé'";
        $scope.card1 = "1";
        $scope.card2 = "2";
        $scope.card3 = "3";
        $scope.card4 = "4";
        $scope.card5 = "5";
        $scope.card6 = "6";
        $scope.card7 = "7";
        $scope.card8 = "8";
        $scope.card9 = "9";
        $scope.card10 = "10";

        $scope.IsSaved = false;
        $scope.Submitted = false;
        $scope.IsFormValid = false;

        $scope.CardsData = {
            Card1: '', Card2: '',
            Card3: '', Card4: '',
            Card5: '', Card6: '',
            Card7: '', Card8: '',
            Card9: '', Card10: ''
        };
        //Check if Form is valid or not // here SaveTeacherDragAndDrop is our form Name
        $scope.$watch('SaveDragAndDropTeacher.$valid', function (newVal) {
            $scope.IsFormValid = newVal;
        });

        $scope.Save = function () {
            $scope.Submitted = true;
            if ($scope.IsFormValid) {
                LoginService.GetUser($scope.CardsData).then(function (d) {
                    if (d.data.Card1 != null) {
                        $scope.IsSaved = true;
                        $scope.Message = "Yup";
                    }
                    else {
                        alert('Woops')
                    }
                })
            }
        };
    })
})();