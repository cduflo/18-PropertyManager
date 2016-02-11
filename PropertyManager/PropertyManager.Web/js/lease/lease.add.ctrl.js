angular.module('app').controller('LeaseAddController', function ($scope, $state, PropertyResource, TenantResource, LeaseResource) {

    $scope.properties = PropertyResource.query();
    $scope.tenants = TenantResource.query();
    $scope.lease = {};
    $scope.title = "Add Lease";

    $scope.propselect = function (prop) {
        $scope.lease.Property = prop;
        $scope.lease.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.lease.Tenant = tenant;
        $scope.lease.TenantId = tenant.TenantId;
    }

    $scope.saveLease = function () {
        LeaseResource.save($scope.lease, function (data) {
            $scope.lease = {};
        });
        alert("Save Successful!");
        $state.go('lease.grid');
    };

});