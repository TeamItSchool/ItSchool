(function () {
    'use strict';
    angular.module('TheApp', ['ngRoute', 'ngMaterial', 'ngGrid', 'ngStorage']) // ['ngRoute'] is required for the routing and
        //['ngMaterial'] for the material design components
    .run(function ($log, $rootScope) {
        $log.debug("startApp running");
    })
    .config(function ($routeProvider, $locationProvider) {
        //here we will write code for implement routing 
        $routeProvider
        .when('/', { // This is for reditect to another route
            redirectTo: function () {
                return '/home';
            }
        })
        .when('/home', {
            templateUrl: '/Templates/Home.html'/*,
        controller: 'HomeController'*/
        })
        .when('/test_microphone', {
            templateUrl: '/Templates/voiceRecorder.html'/*,
        controller:'MicrophoneController'*/
        })
        .when('/teacher', {
            templateUrl: '/Templates/TeacherHomePage.html'/*,
        controller: 'TeacherHomeController'*/
        })
        .when('/teacher/login', {
            templateUrl: '/Templates/TeacherLoginPage.html'/*,
        controller: 'TeacherLoginController'*/
        })
        .when('/teacher/registration', {
            templateUrl: '/Templates/TeacherRegistrationPage.html'/*,
        controller: 'TeacherRegistrationController'*/
        })
        .when('/teacher/lobby', {
            templateUrl: '/Templates/TeacherLobbyPage.html'/*,
        controller: 'TeacherLobbyController'*/
        })
        .when('/teacher/exercices', {
            templateUrl: '/Templates/TeacherSelectExercicesPage.html'/*,
        controller: 'TeacherSelectExercicesController'*/
        })
        .when('/teacher/exercices/matter', {
            templateUrl: '/Templates/TeacherSelectMatterPage.html'/*,
        controller: 'TeacherSelectMatterController'*/
        })
        .when('/teacher/exercices/cloze_exercise', {
            templateUrl: '/Templates/TeacherCustomizeClozeExercisePage.html'
        })
        .when('/teacher/exercices/matter/drag_drop_maths', {
            templateUrl: '/Templates/TeacherCustomizeDragAndDropMathsPage.html'/*,
        controller: 'TeacherDragAndDropMathsController'*/
        })
        .when('/teacher/exercices/matter/drag_drop_conjugaiton', {
            templateUrl: '/Templates/TeacherCustomizeDragAndDropConjugaitonPage.html'/*,
        controller: 'TeacherDragAndDropConjugaitonController'*/
        })
        .when('/teacher/exercices/matter/drag_drop_english', {
            templateUrl: '/Templates/TeacherCustomizeDragAndDropEnglishPage.html'/*,
        controller: 'TeacherDragAndDropEnglishController'*/
        })
        .when('/teacher/exercices/dictation', {
            templateUrl: '/Templates/TeacherCustomizeDictationPage.html'/*,
        controller: 'TeacherDictationController'*/
        })
        .when('/teacher/exercices/battleCard', {
            templateUrl: '/Templates/TeacherDescriptionBattleCardPage.html'/*,
        controller: 'TeacherDescriptionBattleCardController'*/
        })
        .when('/teacher/exercices/battleCard/customize', {
            templateUrl: '/Templates/TeacherCustomizeBattleCardPage.html'
            //controller: 'TeacherCustomizeBattleCardController'
        })
        .when('/kid', {
            templateUrl: '/Templates/KidHomePage.html'/*,
        controller: 'KidHomeController'*/
        })
        .when('/kid/login', {
            templateUrl: '/Templates/KidLoginPage.html'/*,
        controller: 'KidLoginController'*/
        })
        .when('/kid/registration', {
            templateUrl: '/Templates/KidRegistrationPage.html'/*,
        controller: 'KidRegistrationController'*/
        })
        .when('/kid/lobby', {
            templateUrl: '/Templates/KidLobbyPage.html'/*,
        controller: 'KidLobbyController'*/
        })
        .when('/kid/exercices', {
            templateUrl: '/Templates/KidSelectExercicesPage.html'/*,
        controller: 'KidSelectExercicesController'*/
        })
        .when('/kid/exercices/dictation', {
            templateUrl: '/Templates/KidPlayDictationPage.html'/*,
        controller: 'KidPlayDictationController'*/
        })
        .when('/kid/exercices/battleCard', {
            templateUrl: '/Templates/KidDescriptionBattleCardPage.html'/*,
        controller: 'KidDescriptionBattleCardController'*/
        })
        .when('/kid/exercices/battleCard/play', {
            templateUrl: '/Templates/KidPlayBattleCardPage.html'/*,
        controller: 'KidPlayBattleCardController'*/
        })
        .otherwise({   // This is when any route not matched
            templateUrl: '/Templates/Error.html',
            controller: 'ErrorController'
        })
        $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
    })

     // INSCRIPTION
    .controller('Registration', function ($scope, RegistrationService) {
        //Default Variable
        $scope.submitText = "Save";
        $scope.submitted = false;
        $scope.message = "";
        $scope.IsFormValid = false;
        $scope.User = {
            UserName: 'Steve',
            Password: '',
            FullName: '',
            EmailID: '',
            Gender: ''
        };

        //Check form validation // here RegisterForm is our form name
        $scope.$watch("RegisterForm.$valid", function (newValue) {
            $scope.IsFormValid = newValue;
        })

        //Save Data
        $scope.SaveData = function (data) {
            if ($scope.submitText == "Save") {
                $scope.submitted = true;
                $scope.message = "";

                if ($scope.IsFormValid) {
                    $scope.submitText = "Please wait...";
                    $scope.User = data;
                    RegistrationService.SaveFormData($scope.User).then(function (d) {
                        alert(d);
                        if (d == 'Success') {
                            //Have to clear form here
                            ClearForm();
                        }
                        $scope.submitText = "Save";
                    });
                }
                else {
                    $scope.message = 'Please fill required fields value';
                }
            }
        }

        //Clear form (reset)
        function ClearForm() {
            $scope.User = {};
            $scope.RegisterForm.$setPristine(); // here registerForm is our form name
            $scope.submitted = false;
        }
    })
})();