angular.module('app').controller('LeaseDetailController', function ($scope, $state, $stateParams, PropertyResource, TenantResource, LeaseResource) {
    //grab id from url bar
    //grab property from /api/leases/{leaseId}

    $scope.tenants = TenantResource.query();
    $scope.properties = PropertyResource.query();
    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id }, function () {
        $scope.date = $scope.lease.StartDate.toString().substring(0,10);
    });
    $scope.title = "Lease Detail";



    $(function () {
        $("#datepicker").datepicker({
            autoclose: true,
        }).data({ date: '2012-08-08' }).datepicker('update').datepicker().children('input').val('2012-08-08');
    });

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
        $state.go('lease.grid');
    };
});