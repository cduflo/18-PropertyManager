angular.module('app').controller('TenantAddController', function ($scope, $state, TenantResource) {

    $scope.tenant = {};

    $scope.title = "Add Tenant";

    $scope.saveTenant = function () {
        TenantResource.save($scope.tenant, function (data) {
            $scope.tenant = {};
        });
        alert("Save Successful!");
        $state.go('tenant.grid');
    };

});