angular.module('app').controller('LeaseDetailController', function ($scope, $state, $stateParams, PropertyResource, TenantResource, LeaseResource) {
    //grab id from url bar
    //grab property from /api/leases/{leaseId}

    $scope.tenants = TenantResource.query();
    $scope.properties = PropertyResource.query();
    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id }, function () {
        $scope.startDate = $scope.lease.StartDate.toString().substring(0, 10);
        $scope.endDate = $scope.lease.EndDate.toString().substring(0, 10);
    });
    $scope.title = "Lease Detail";

    $(".datepicker").datepicker({
        format: "yyyy-mm-dd",
        autoclose: true,
    });

    $scope.getDates = function () {
        $scope.startDate = $('#startdate').datepicker('getDate');
        $scope.endDate = $('#enddate').datepicker('getDate');
    };

    $scope.setDates = function () {
        $scope.lease.StartDate = $scope.startDate;
        $scope.lease.EndDate = $scope.endDate;
    };

    $scope.propselect = function (prop) {
        $scope.lease.Property = prop;
        $scope.lease.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.lease.Tenant = tenant;
        $scope.lease.TenantId = tenant.TenantId;
    }

    $scope.saveLease = function () {
        $scope.getDates();
        $scope.setDates();
        $scope.lease.$update();
        $state.go('app.lease.grid');
    };
});