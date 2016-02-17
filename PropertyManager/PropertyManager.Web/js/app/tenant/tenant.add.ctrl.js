angular.module('app').controller('TenantAddController', function ($scope, $state, TenantResource) {

    $scope.tenant = {};

    $scope.title = "Add Tenant";

    $scope.saveTenant = function () {
        TenantResource.save($scope.tenant, function (data) {
            $scope.tenant = {};
        });
        $state.go('app.tenant.grid');
    };

});