angular.module('app').controller('WorkOrderDetailController', function ($scope, $stateParams, $state, PropertyResource, TenantResource, WorkOrderResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.workorder = WorkOrderResource.get({ workorderId: $stateParams.id });
    $scope.properties = PropertyResource.query();//potentially delete with PropertyResource injected above
    $scope.tenants = TenantResource.query();
    $scope.title = "Work Order Detail";

    $scope.propselect = function (prop) {
        $scope.workorder.Property = prop;
        $scope.workorder.PropertyId = prop.PropertyId;
    }

    $scope.tenantselect = function (tenant) {
        $scope.workorder.Tenant = tenant;
        $scope.workorder.TenantId = tenant.TenantId;
    }

    $scope.saveWorkOrder = function () {
        $scope.workorder.$update();
        alert("Save Successful!");
        $state.go('workorder.grid');
    };
});