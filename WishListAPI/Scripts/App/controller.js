var App = angular.module("App", []);

App.controller("wishListController", function ($scope, $http) {
    
    // Get user with id 1

    var urlUser = '/api/User';

    /* Fungerer:
    $http.get(urlUser + "/Get/" + 1).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {
            
        });*/

    // Get all users

    $http.get(urlUser).
        success(function (users) {

        }).
        error(function (data, status) {

        });

    // LogIn
    
    /* Fungerer:
    var userName = "noe";
    var password = "noe";

    $http.get(urlUser + "/LogIn/" + userName + "/" + password).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {

        });
    */
    /*
    var loginUser = {
        userName: "noe",
        password: "noe"
    }

    loginUser = JSON.stringify(loginUser);

    $http.get(urlUser + "/LogIn/" + loginUser).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {

        });*/

    /*var userName = "noe";
    var password = "noe";

    $http.get("api/Login" + "/" + userName + "/" + password).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {

        });*/

    var urlLogin = 'api/Login'

    var loginUser = {
        userName: "brukernavn",
        password: "passord"
    };

    $http.post(urlLogin, loginUser).
        success(function (data) {
            
        }).
        error(function (data, status) {

        });

    $http.get(urlUser + "/" + 1).
        success(function (user) {
            $scope.loading = true;
        }).
        error(function (data, status) {

        });

    // Fungerer:

    var user = {
        userName: "noen",
        password: "måinneholdeåttetegn",
        email: "noen@noen.com",
        phoneNumber: "91726354"
    };

    $http.post(urlUser, user).
          success(function (data) {
              
          }).
          error(function (data, status) {
              
          });
});