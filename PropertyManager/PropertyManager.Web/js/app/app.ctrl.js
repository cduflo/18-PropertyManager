angular.module('app').controller('AppController', function ($scope, AuthenticationService, localStorageService) {
    var userobj = localStorageService.get('user');
    $scope.user = userobj.user;

    $scope.logout = function () {
        AuthenticationService.logout()
        location.replace('#/login');
    };
});