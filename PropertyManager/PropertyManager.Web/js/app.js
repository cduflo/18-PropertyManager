angular.module('app', ['ngResource','ui.router']);

angular.module('app').value('apiUrl', 'http://localhost:50227/api');

angular.module('app').config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider.state('dashboard', { url: '/dashboard', templateUrl: '/templates/dashboard/dashboard.html', controller: 'DashboardController'})
                  .state('property', { url: '/property', abstract: true,  template: '<ui-view/>'})
                     .state('property.grid', {url: '/grid', templateUrl: '/templates/property/property.grid.html', controller: 'PropertyGridController'})
                     .state('property.detail', {url: '/detail/:id', templateUrl: '/templates/property/property.detail.html', controller: 'PropertyDetailController'});

});