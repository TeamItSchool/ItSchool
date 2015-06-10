var app = angular.module('single-page-app', ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider.when('/home', { templateUrl: 'partials/partialOne', controller: 'CtrlOne' });
    $routeProvider.when('/about', { templateUrl: 'partials/partialTwo', controller: 'CtrlOne'});
});
app.controller('cfgController', function ($scope) {

    /*     
    Here you can handle controller for specific route as well.
    */
});