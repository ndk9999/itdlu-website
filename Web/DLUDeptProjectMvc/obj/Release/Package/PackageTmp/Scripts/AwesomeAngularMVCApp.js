var app = angular.module('AwesomeAngularMVCApp', ['ngRoute']);
app.controller('LandingPageController', LandingPageController);

 

var configFunction = function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'demo',
            templateUrl: 'index'
        })
        .when('/routeOne', {
            templateUrl: 'routesDemo/one'
        })
        .when('/routeTwo', {
            templateUrl: 'routesDemo/two'
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        });
}
configFunction.$inject = ['$routeProvider'];

app.config(configFunction);