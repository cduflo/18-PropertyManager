angular.module('app').controller('LeaseDetailController', function ($scope, $state, $stateParams, PropertyResource, TenantResource, LeaseResource) {
    //grab id from url bar
    //grab property from /api/leases/{leaseId}
    $scope.tenants = TenantResource.query();
    $scope.properties = PropertyResource.query();
    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id });
    $scope.title = "Lease Detail";

    $scope.propselect = function (prop) {
        $scope.lease.Property = prop;
        $scope.lease.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.lease.Tenant = tenant;
        $scope.lease.TenantId = tenant.TenantId;
    }

    $scope.saveLease = function () {
        $scope.lease.$update();
        alert("Save Successful!");
        $state.go('lease.grid');
    };
});