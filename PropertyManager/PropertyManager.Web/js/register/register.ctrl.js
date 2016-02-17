﻿angular.module('app').controller('RegisterController', function ($scope, $timeout, AuthenticationService) {
    $scope.registration = {};

    $scope.register = function () {
        AuthenticationService.register($scope.registration).then(
            function (response) {
                alert("User has been registered. You will be redirected to the login page in 2 seconds.");
                $timeout(function () {
                    location.replace('/#/login');
                }, 2000);
            },
            function (error) {
                alert("Failed to register");
            });
    };
});