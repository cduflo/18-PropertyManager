angular.module('app').controller('WorkOrderDetailController', function ($scope, $stateParams, WorkOrderResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.workorder = WorkOrderResource.get({ workorderId: $stateParams.id });


    $scope.saveWorkOrder = function () {
        $scope.workorder.$update();
        alert("Save Successful!");
    };
});