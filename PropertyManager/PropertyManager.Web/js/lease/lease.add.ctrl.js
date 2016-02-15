angular.module('app').controller('LeaseAddController', function ($scope, $state, PropertyResource, TenantResource, LeaseResource) {

    $scope.properties = PropertyResource.query();
    $scope.tenants = TenantResource.query();
    $scope.lease = {};
    $scope.title = "Add Lease";

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
        LeaseResource.save($scope.lease, function (data) {
            $scope.lease = {};
        });
        $state.go('lease.grid');
    };

});