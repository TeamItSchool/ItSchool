(function () {
    'use strict';
    angular.module('TheApp').controller("TeacherClozeExerciseController", function ($scope, ExerciseDatas) {

        var sessions_elements_json = sessionStorage.getItem("objet");
        var sessions_elements = JSON.parse(sessions_elements_json);

        $scope.Message = 'Que voulez-vous faire ?';
        $scope.NewExerciseSelected = false;
        $scope.SetExistingExerciseSelected = false;

        $scope.new_exercise = function () {
            $scope.NewExerciseSelected = true;
        }

        $scope.set_exercise = function () {
            $scope.SetExistingExerciseSelected = true;

            ExerciseDatas.GetClozeExercises().then(function (d) {
                console.log('Data GetClozeExercises(): ' + d.data);
                $scope.ClozeExercises = d.data;
            });
        }

        $scope.Message = 'Sélectionnez un niveau.';
        $scope.EasySelected = false;
        $scope.MediumSelected = false;
        $scope.HardSelected = false;

        $scope.Easy = function () {
            $scope.EasySelected = true;
            $scope.Message = "Insérez le texte (Niveau 'facile')";
            $scope.ExerciseClozeData.Level.Name = "Easy";
        }

        $scope.Medium = function () {
            $scope.MediumSelected = true;
            $scope.Message = "Insérez le texte (Niveau 'moyen')";
            $scope.ExerciseClozeData.Level.Name = "Medium";
        }

        $scope.Hard = function () {
            $scope.HardSelected = true;
            $scope.Message = "Insérez le texte (Niveau 'difficile')";
            $scope.ExerciseClozeData.Level.Name = "Hard";
        }

        $scope.Message = 'Configuration de l\'exercice';
        $scope.Button = 'Sauvegarder';
        $scope.IsFormValid = false;
        $scope.ExerciseClozeData = {
            Name: '',
            Text: '',
            HiddenWords: '',
            Level: {
                Name: ''
            },
            Chapter: {
                Name: ''
            },
            UsersIds: []
        };

        $scope.Children = null;
        $scope.Levels = null;

        ExerciseDatas.GetChildren(sessions_elements.data.Class.ClassId).then(function (d) {
            console.log('Data GetChildren: ' + d.data);
            $scope.Children = d.data;

            for (var i = 0; i < $scope.Children.length; i++) {
                $scope.ExerciseClozeData.UsersIds.push($scope.Children[i].UserId);
            }
            console.log('UsersIds in scope : ' + $scope.ExerciseClozeData.UsersIds);
        });

        ExerciseDatas.GetLevels().then(function (d) {
            console.log('Data GetLevel: ' + d.data);
            $scope.Levels = d.data;
        });

        //ExerciseDatas.GetClozeExercise().then(function (d) {
        //    $scope.DatabaseText = d.data.Text;
        //    $scope.Exercise.Words = d.data.Words;
        //    $scope.Exercise.Text = d.data.Text;
        //    $scope.Selections = [];

        //var textLowerCase = d.data.Text.toLowerCase();
        //var wordsText = textLowerCase.split(/[\s,.:;]+/);
        //var wordsWithCount = [];
        //var uniqueWords = [];
        //var arrayJsonFormat = [];

        //var savedIndex = 0;

        //// Sorting as we have a unique words array
        //for(var i = 0; i < wordsText.length; ++i ) {
        //    if ($.inArray(wordsText[i], uniqueWords) == -1) {
        //        var word = {
        //            'value': wordsText[i],
        //            'count': 1
        //        };
        //        arrayJsonFormat.push( word );
        //        uniqueWords.push(wordsText[i]);
        //    } else {
        //        for (var j = 0; j < arrayJsonFormat.length; j++) {
        //            if( arrayJsonFormat[j].value == wordsText[i] ) {
        //                arrayJsonFormat[j].count += 1;
        //                break;
        //            }
        //        }
        //    }
        //}
        //$scope.myData = arrayJsonFormat;
        //}, function (error) {
        //    alert("An error occured");
        //});

        //$scope.gridOptions = {
        //    data: 'myData',
        //    selectedItems: $scope.Selections,
        //    multiSelect: true
        //};

        ExerciseDatas.GetChapters().then(function (d) {
            $scope.Chapters = d.data;
        }, function (error) {
            alert('An error occured. See console for more details.')
            console.log('Error L435 is ' + error);
        });

        $scope.$watch('ClozeExercise', function (newValue) {
            $scope.IsFormValid = newValue;
        });

        $scope.$watch('setClozeEx', function (value) {
            $scope.IsFormValid = value;
        });

        $scope.update_data = function () {
            // TODO
        };

        $scope.SaveData = function () {
            console.log('in SaveData(), l896');
            ExerciseDatas.CreateClozeExercise($scope.ExerciseClozeData).then(function (creationInfo) {
                var state = creationInfo.data;
                if (state == 'created') {
                    $scope.Button = 'Exercice sauvegardé !'
                    console.log('State: exercise saved');
                } else if (state == 'error existing name') {
                    alert('Attention : la création de l\'exercice n\'a pas pu aboutir. Il existe déjà un exercice du même nom.');
                } else if (state == 'error validation form') {
                    alert('Erreur lors de la soumission de l\'exercice : le formulaire n\'est pas valide.');
                } else {
                    alert('L\'exercice n\'a pas pu être sauvegardé. Vérifiez vos entrées ou recommencez.');
                }
            });
        };
    })
})();