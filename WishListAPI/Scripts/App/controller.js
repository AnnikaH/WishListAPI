var App = angular.module("App", []);

App.controller("faqController", function ($scope, $http, $location, $anchorScroll) {

    var urlUser = '/api/User';

    $http.get(urlUser + "/" + 1).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {
            
        });
});