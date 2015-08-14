(function () {
    'use strict';
    angular.module('TheApp').controller('KidPlayDictationController', function ($scope, GetChildExerciseDictation, CheckDictationText) {

        $scope.EmptySession = false;
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
            var monobjet_json = sessionStorage.getItem("objet");
            var monobjet = JSON.parse(monobjet_json);
            // Affichage dans la console
            console.log(monobjet.data.FirstName + " est dans la modification de la dictée");

            $scope.Message = 'Sélectionne un niveau.';
            $scope.EasySelected = false;
            $scope.MediumSelected = false;
            $scope.HardSelected = false;
            $scope.Message2 = "";
            $scope.IsFormValid = false;
            $scope.Button = "Valider";
            $scope.ShowDictationAudio = false;
            $scope.ChildTextWritted = "";

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

            $scope.ExerciseDictationData = {
                //A REMPLIR
                Text: '',
                Level: {
                    Name: 'Test'
                },
                AudioData: '',
                UsersIds: ''
            };

            $scope.ExerciseDictation = {

            };


            $scope.ExerciseDictationList = null;

            var easyDictationFound = false;
            var mediumDictationFound = false;
            var hardDictationFound = false;

            GetChildExerciseDictation.GetExerciseDictation(monobjet.data.UserId).then(function (d) {
                $scope.ExerciseDictationList = d.data;

                if ($scope.ExerciseDictationList != null) {
                    for (var i = 0; i < $scope.ExerciseDictationList.length; i++) {
                        if ($scope.ExerciseDictationList[i].LevelId == 1)
                            easyDictationFound = true;
                        if ($scope.ExerciseDictationList[i].LevelId == 2)
                            mediumDictationFound = true;
                        if ($scope.ExerciseDictationList[i].LevelId == 3)
                            hardDictationFound = true;
                    }
                    if (easyDictationFound == false)
                        document.getElementById("easy").disabled = true;
                    if (mediumDictationFound == false)
                        document.getElementById("medium").disabled = true;
                    if (hardDictationFound == false)
                        document.getElementById("hard").disabled = true;
                }
                else {
                    document.getElementById("easy").disabled = true;
                    document.getElementById("medium").disabled = true;
                    document.getElementById("hard").disabled = true;
                }
                console.log($scope.ExerciseDictationList);
            });

            $scope.Easy = function () {
                for (var i = 0; i < $scope.ExerciseDictationList.length ; i++) {
                    if ($scope.ExerciseDictationList[i].LevelId == 1) {
                        $scope.ShowDictationAudio = true;
                        $scope.ExerciseDictation = $scope.ExerciseDictationList[i];

                        var audio = document.getElementById("dictationAudio");
                        audio.src = $scope.ExerciseDictation.AudioData;
                        //audio.play();
                    }
                    console.log($scope.ExerciseDictation.AudioData);
                }
                $scope.EasySelected = true;
                $scope.Message = "Niveau Facile : Tu peux le faire !";
                //$scope.ExerciseDictationData.Level.Name = "Easy";
            }
            $scope.Medium = function () {
                for (var i = 0; i < $scope.ExerciseDictationList.length ; i++) {
                    if ($scope.ExerciseDictationList[i].LevelId == 2) {
                        $scope.ShowDictationAudio = true;
                        $scope.ExerciseDictation = $scope.ExerciseDictationList[i];

                        var audio = document.getElementById("dictationAudio2");
                        audio.src = $scope.ExerciseDictation.AudioData;
                    }
                    console.log($scope.ExerciseDictation.AudioData);
                }
                $scope.MediumSelected = true;
                $scope.Message = "Niveau Moyen : Fonce, tu peux y arriver !";
                //$scope.ExerciseDictationData.Level.Name = "Medium";
            }
            $scope.Hard = function () {
                for (var i = 0; i < $scope.ExerciseDictationList.length ; i++) {
                    if ($scope.ExerciseDictationList[i].LevelId == 3) {
                        $scope.ShowDictationAudio = true;
                        $scope.ExerciseDictation = $scope.ExerciseDictationList[i];

                        var audio = document.getElementById("dictationAudio3");
                        audio.src = $scope.ExerciseDictation.AudioData;
                    }
                    console.log($scope.ExerciseDictation.AudioData);
                }
                $scope.HardSelected = true;
                $scope.Message = "Niveau Difficle : Tu es le meilleur, donne tout ce que tu as !";
                //$scope.ExerciseDictationData.Level.Name = "Hard";
            }

            //Check if Form is valid or not // here DictText is our form Name
            $scope.$watch('DictText.$valid', function (newVal) {
                $scope.IsFormValid = newVal;
            });

            $scope.SubmitText = function () {
                if ($scope.IsFormValid) {
                    $scope.Button = "Validtion en cours..."
                    $scope.ExerciseDictation.Text = monobjet.data.Nickname + "/" + $scope.ChildTextWritted;
                    CheckDictationText.GetDictationText($scope.ExerciseDictation).then(function (d) {
                        alert(d.data);
                        $scope.Button = "Dictée envoyée";
                    })
                }
            };
        }
    })

})();