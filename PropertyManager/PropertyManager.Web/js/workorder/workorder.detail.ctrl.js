angular.module('app').controller('WorkOrderDetailController', function ($scope, $stateParams, WorkOrderResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.workorder = WorkOrderResource.get({ workorderId: $stateParams.id });
    $scope.title = "Work Order Detail: {{workorder.WorkOrderId}}";


    $scope.saveWorkOrder = function () {
        $scope.workorder.$update();
        alert("Save Successful!");
        $state.go('workorder.grid');
    };
});