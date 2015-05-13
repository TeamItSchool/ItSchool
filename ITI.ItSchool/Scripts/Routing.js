angular.module('TheApp', ['ngRoute', 'ngMaterial']) //extending from previously created angularjs module in part1
// here ['ngRoute'] is not required, I have just added to make you understand in a single place
.run(function ($log) {
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
        templateUrl: '/Templates/Home.html',
        controller: 'HomeController'
    })
    .when('/teacher', {
        templateUrl: '/Templates/TeacherHomePage.html',
        controller: 'TeacherHomeController'
    })
    .when('/kid', {
        templateUrl: '/Templates/KidHomePage.html',
        controller: 'KidHomeController'
    })
    .when('/kid/login', {
        templateUrl: '/Templates/KidLoginPage.html',
        controller: 'KidLoginController'
    })
    .when('/kid/registration', {
        templateUrl: '/Templates/KidRegistrationPage.html',
        controller: 'KidRegistrationController'
    })
    .otherwise({   // This is when any route not matched
        templateUrl: '/Templates/Error.html',
        controller: 'ErrorController'
    })

    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})
.controller('HomeController', function ($scope) {
    $scope.Message = "Bienvenue";
})
.controller('KidLogInController', function ($scope) {
    $scope.Message = "Embarque dans l'aventure It'School !";
})
.controller('TeacherHomeController', function ($scope) {
    $scope.Message = 'Page "Professeurs"';
})
.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})
.controller('KidLoginController', function ($scope) {
    $scope.Message = "Entre le pseudo et le mot de passe que tu avais choisis.";
})
.controller('ErrorController', function ($scope) {
    $scope.Message = "404 Not Found!";
});
