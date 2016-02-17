angular.module('app').controller('WorkOrderAddController', function ($scope, $state, PropertyResource, TenantResource, WorkOrderResource) {

    $scope.properties = PropertyResource.query();
    $scope.tenants = TenantResource.query();
    $scope.workorder = {};
    $scope.title = "Add Work Order";

    $(".datepicker").datepicker({
        format: "yyyy-mm-dd",
        autoclose: true,
    });

    $scope.getDates = function () {
        $scope.startDate = $('#startdate').datepicker('getDate');
        $scope.endDate = $('#enddate').datepicker('getDate');
    };

    $scope.setDates = function () {
        $scope.workorder.OpenedDate = $scope.startDate;
        $scope.workorder.ClosedDate = $scope.endDate;
    };

    $scope.propselect = function (prop) {
        $scope.workorder.Property = prop;
        $scope.workorder.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.workorder.Tenant = tenant;
        $scope.workorder.TenantId = tenant.TenantId;
    }

    $scope.saveWorkOrder = function () {
        $scope.getDates();
        $scope.setDates();
        WorkOrderResource.save($scope.workorder, function (data) {
            $scope.workorder = {};
        });
        $state.go('app.workorder.grid');
    };

});