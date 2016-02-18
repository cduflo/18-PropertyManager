angular.module('app').controller('WorkOrderDetailController', function ($scope, $stateParams, $state, PropertyResource, TenantResource, WorkOrderResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.workorder = WorkOrderResource.get({ workorderId: $stateParams.id }, function () {
        $scope.startDate = $scope.workorder.OpenedDate.toString().substring(0, 10);
        $scope.endDate = $scope.workorder.ClosedDate.toString().substring(0, 10);
    });
    $scope.properties = PropertyResource.query();
    $scope.tenants = TenantResource.query();
    $scope.title = "Work Order Detail";
    $scope.priorityList = [1,2,3,4,5];


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

    $scope.priorityselect = function (priority) {
        $scope.workorder.Priority = priority;
    }

    $scope.saveWorkOrder = function () {
        $scope.getDates();
        $scope.setDates();
        $scope.workorder.$update();
        $state.go('app.workorder.grid');
    };
});