// Customize BattleCard
(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherCustomizeBattleCardController', function ($scope, SaveBattleCardChoice) {

        $scope.EmptySession = false;
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
            var monobjet_json = sessionStorage.getItem("objet");
            var monobjet = JSON.parse(monobjet_json);
            // Affichage dans la console
            console.log(monobjet.data.FirstName + " est dans la modification de card game");
            console.log("ClassId : " + monobjet.data.ClassId);

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

            $scope.Message = 'Choix du niveau';
            $scope.EasySelected = false;
            $scope.MediumSelected = false;
            $scope.HardSelected = false;
            $scope.IsFormValid = false;
            $scope.Button = "Sauvegarder";

            $scope.ExerciseBattleCardData = {
                //A REMPLIR      
                Level: {
                    Name: 'Test'
                },
                ChoiceData: '',
                UsersIds: ''
            };

            $scope.Children = null;
            $scope.selected = [];

            SaveBattleCardChoice.GetUsers(monobjet.data.ClassId).then(function (d) {
                //console.log("test");
                //console.log(d);
                //console.log("Taille de la data : " + d.data.length);
                //console.log(d.data);
                //console.log(d.data[0]);
                //console.log(d.data[1]);
                //console.log(d.data[0].FirstName);
                //console.log(d.data[0].LastName);
                //console.log(d.data[0].Nickname);
                //console.log(d.data[0].ClassId);

                $scope.Children = d.data;
                $scope.ExerciseBattleCardData.UsersIds = $scope.Children;
                console.log($scope.ExerciseBattleCardData.UsersIds);
            });

            $scope.toggle = function (child, list) {
                var idx = list.indexOf(child.UserId);
                if (idx > -1)
                    list.splice(idx, 1);
                else
                    list.push(child.UserId);
            };

            $scope.exists = function (child, list) {
                return list.indexOf(child.UserId) > -1;
            };

            //Check if Form is valid or not // here FormChoice is our form Name
            $scope.$watch('FormChoice.$valid', function (newVal) {
                $scope.IsFormValid = newVal;
            });
            $scope.IsFormValid
            $scope.SaveChoice = function () {
                if ($scope.IsFormValid && $scope.ExerciseBattleCardData.ChoiceData != "") {
                    $scope.Button = "Sauvegarde en cours..."
                    $scope.ExerciseBattleCardData.ChoiceData.trim();
                    $scope.ExerciseBattleCardData.ChoiceData = monobjet.data.Nickname + "/" + $scope.ExerciseBattleCardData.ChoiceData;
                    var res = $scope.ExerciseBattleCardData.ChoiceData.split("/");


                    $scope.ExerciseBattleCardData.UsersIds = $scope.selected;


                    SaveBattleCardChoice.GetChoice($scope.ExerciseBattleCardData).then(function (d) {
                        $scope.ExerciseBattleCardData.ChoiceData = res[1];
                        console.log(d.data);
                        if (d.data == "Jeu enregistré")
                            $scope.Button = "Battle Card sauvegardé";
                        else {
                            alert(d.data);
                            $scope.Button = "Sauvegarder"
                        }
                    })
                }
            };
            $scope.Easy = function () {
                $scope.EasySelected = true;
                $scope.Message = "Niveau facile";
                $scope.ExerciseBattleCardData.Level.Name = "Easy";
            }

            $scope.Medium = function () {
                $scope.MediumSelected = true;
                $scope.Message = "Niveau moyen";
                $scope.ExerciseBattleCardData.Level.Name = "Medium";
            }
            $scope.Hard = function () {
                $scope.HardSelected = true;
                $scope.Message = "Niveau difficile";
                $scope.ExerciseBattleCardData.Level.Name = "Hard";
            }
        }
    })
})();