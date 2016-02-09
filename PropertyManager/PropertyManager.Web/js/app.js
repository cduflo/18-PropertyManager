angular.module('app', []);

angular.module('app').value('apiUrl', 'http://localhost:50227/api');

angular.module('app').config(function ($stateProvider, $urlRouteProvider) {
    $stateProvider.state('/dashboard', {
        url: '/dashboard', templateUrl: '/templates/dashboard/index.html', controller: 'DashboardController'
    });

});