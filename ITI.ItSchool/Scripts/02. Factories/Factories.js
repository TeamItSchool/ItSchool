(function () {
    'use strict';
    angular.module('TheApp')
        //Login Factory
        .factory('LoginService', function ($http) {
            var fac = {};
            var data = "";
            fac.GetUser = function (d) {
                return $http({
                    url: '/Data/UserLogin',
                    method: 'POST',
                    data: JSON.stringify(d),
                    headers: { 'content-type': 'application/json' }
                })
            };

            return fac;
        })

        //Registration Factory
        .factory('RegistrationService', function ($http, $q) {
            //here q is an angularJS service which helps us to run asynchronous function and return result when processing is done
            var fac = {};
            fac.SaveFormData = function (data) {
                var defer = $q.defer();
                $http({
                    url: '/Data/Register',
                    method: 'POST',
                    data: JSON.stringify(data),
                    header: { 'content-type': 'application/json' }
                }).success(function (d) {
                    //Success callback
                    defer.resolve(d);
                }).error(function (e) {
                    //Failed callback
                    console.log("An error occured. Error is : " + e + "Data is : " + JSON.stringify(data));
                    defer.reject(e);
                });
                return defer.promise;
            }

            fac.GetClasses = function () {
                return $http.get('/Data/GetClasses')
            }
            return fac;
        })

        //Save Drag And Drop Exercise Factory
        .factory('SaveDragAndDropExercice', function ($http) {
            var fac = {};
            var data = "";
            fac.GetUser = function (d) {
                return $http({
                    url: '/Data/SaveDragAndDropTeacher',
                    method: 'POST',
                    data: JSON.stringify(d),
                    headers: { 'content-type': 'application/json' }
                })
            };

            return fac;
        })

        //Exercise Data Cloze Exercise Factory
        .factory('ExerciseDatas', function ($http) {
            var fac = {};
            var data = "";
            fac.CreateClozeExercise = function (d) {
                return $http({
                    url: '/Data/CreateClozeExercise',
                    method: 'POST',
                    data: JSON.stringify(d),
                    headers: { 'content-type': 'application/json' }
                }).success(function (d) {
                    defer.resolve(d);
                }).error(function (e) {
                    defert.reject(e);
                });
            };

            fac.GetClozeExercises = function () {
                return $http.get('/Data/GetClozeExercises');
            }

            fac.GetClasses = function () {
                return $http.get('/Data/GetClasses')
            }

            fac.GetChildren = function (data) {
                return $http.get('/Data/GetUsersByClasses/' + data);
            }

            fac.GetGroups = function () {
                return $http.get('/Data/GetGroups')
            }

            fac.GetChapters = function () {
                return $http.get('/Data/GetChapters')
            }

            fac.GetLevels = function () {
                return $http.get('/Data/GetLevels');
            }

            return fac;
        })

        // Save Battle Card Choice Factory
        .factory('SaveBattleCardChoice', function ($http) {
            var fac = {};
            var data = "";
            fac.GetUsers = function (data) {
                //return $http.get('/Data/GetClasses')
                return $http.get('/Data/GetUsersByClasses/' + data)
            }
            fac.GetChoice = function (d) {
                return $http({
                    url: '/Data/SaveBattleCard',
                    method: 'POST',
                    data: JSON.stringify(d),
                    headers: { 'content-type': 'application/json' }
                })
            };
            return fac;
        })

        //Get Children For Battle Card Factory
        .factory('GetChildExerciseBattleCard', function ($http) {
            var fac = {};
            fac.GetExerciseBattleCard = function (d) {
                return $http.get('/Data/GetExerciseBattleCard/' + d)
            };
            return fac;
        })

        // Save Dictation Text Factory
        .factory('SaveDictationText', function ($http) {
            var fac = {};
            var data = "";

            fac.GetChildren = function (d) {
                return $http.get('/Data/GetSpecificChilden/' + d);
            }

            fac.GetDictation = function (d) {
                return $http.get('/Data/GetDictation/' + d);
            }

            fac.GetText = function (d) {
                return $http({
                    url: '/Data/SaveDictation',
                    method: 'POST',
                    data: JSON.stringify(d),
                    headers: { 'content-type': 'application/json' }
                })
            };

            return fac;
        })

        //Get Child For Exercise Dictation Factory
        .factory('GetChildExerciseDictation', function ($http) {
            var fac = {};
            fac.GetExerciseDictation = function (d) {
                return $http.get('/Data/GetExerciseDictation/' + d)
            };
            return fac;
        })

        //Check Dictation Text Factory
        .factory('CheckDictationText', function ($http) {
            var fac = {};
            var data = "";
            fac.GetDictationText = function (d) {
                return $http({
                    url: '/Data/CheckDictationText',
                    method: 'POST',
                    data: JSON.stringify(d),
                    header: { 'content-type': 'application/json' }
                })
            };
            return fac;
        })
})();