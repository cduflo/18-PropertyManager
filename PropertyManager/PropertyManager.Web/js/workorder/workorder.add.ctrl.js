angular.module('app').controller('WorkOrderAddController', function ($scope, $state, PropertyResource, TenantResource, WorkOrderResource) {

    $scope.properties = PropertyResource.query();
    $scope.tenants = TenantResource.query();
    $scope.workorder = {};
    $scope.title = "Add Work Order";

    $scope.propselect = function (prop) {
        $scope.workorder.Property = prop;
        $scope.workorder.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.workorder.Tenant = tenant;
        $scope.workorder.TenantId = tenant.TenantId;
    }

    $scope.saveWorkOrder = function () {
        WorkOrderResource.save($scope.workorder, function (data) {
            $scope.workorder = {};
        });
        alert("Save Successful!");
        $state.go('workorder.grid');
    };

});