// Kid description BattleCard
(function () {
    'use strict';
    angular.module('TheApp').controller('KidDescriptionBattleCardController', function ($scope, GetChildExerciseBattleCard, battleCardService, $localStorage) {

        $scope.EmptySession = false;
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
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
            var monobjet_json = sessionStorage.getItem("objet");
            var monobjet = JSON.parse(monobjet_json);
            // Affichage dans la console
            console.log(monobjet.data.FirstName + " est dans la page de description battleCard");

            console.log($localStorage.choiceData);

            $scope.GoBack = function () {
                window.history.back();
            };

            $scope.Message = 'Sélectionne un niveau';
            $scope.EasySelected = false;
            $scope.MediumSelected = false;
            $scope.HardSelected = false;

            var easyBattleCardFound = false;
            var mediumBattleCardFound = false;
            var hardBattleCardFound = false;

            $scope.ExerciseBattleCardData = {
                //A REMPLIR      
                Level: {
                    Name: 'Test'
                },
                ChoiceData: '',
                UsersIds: ''
            };

            $scope.ExerciseBattleCard = {
                //A REMPLIR
            };

            $scope.ExerciseBattleCardList = null;


            GetChildExerciseBattleCard.GetExerciseBattleCard(monobjet.data.UserId).then(function (d) {
                $scope.ExerciseBattleCardList = d.data;
                console.log($scope.ExerciseBattleCardList);

                for (i = 0; i < $scope.ExerciseBattleCardList.length ; i++) {
                    if ($scope.ExerciseBattleCardList[i].LevelId == 1)
                        easyBattleCardFound = true;
                    if ($scope.ExerciseBattleCardList[i].LevelId == 2)
                        mediumBattleCardFound = true;
                    if ($scope.ExerciseBattleCardList[i].LevelId == 3)
                        hardBattleCardFound = true;
                }

                if (!easyBattleCardFound)
                    document.getElementById("easy").disabled = true;
                if (!mediumBattleCardFound)
                    document.getElementById("medium").disabled = true;
                if (!hardBattleCardFound)
                    document.getElementById("hard").disabled = true;

            });

            $scope.Easy = function () {
                for (i = 0; i < $scope.ExerciseBattleCardList.length ; i++) {
                    if ($scope.ExerciseBattleCardList[i].LevelId == 1)
                        battleCardService.addChoice($scope.ExerciseBattleCardList[i].Choice);
                }
                $scope.EasySelected = true;
                $scope.Message = "Niveau Facile : Tu peux le faire !";
                $scope.ExerciseBattleCardData.Level.Name = "Easy";
            }

            $scope.Medium = function () {
                for (i = 0; i < $scope.ExerciseBattleCardList.length ; i++) {
                    if ($scope.ExerciseBattleCardList[i].LevelId == 2)
                        battleCardService.addChoice($scope.ExerciseBattleCardList[i].Choice);
                }
                $scope.MediumSelected = true;
                $scope.Message = "Niveau Moyen : Fonce, tu peux y arriver !";
                $scope.ExerciseBattleCardData.Level.Name = "Medium";
            }
            $scope.Hard = function () {
                for (i = 0; i < $scope.ExerciseBattleCardList.length ; i++) {
                    if ($scope.ExerciseBattleCardList[i].LevelId == 3)
                        battleCardService.addChoice($scope.ExerciseBattleCardList[i].Choice);
                }
                $scope.HardSelected = true;
                $scope.Message = "Niveau Difficle : Tu es le meilleur, donne tout ce que tu as !";
                $scope.ExerciseBattleCardData.Level.Name = "Hard";
            }
        }
    })

})();