angular.module('app').controller('WorkOrderGridController', function ($scope, $state,  WorkOrderResource) {
    function activate() {
        $scope.workorders = WorkOrderResource.query();
    }
    activate();


    $scope.deleteWorkOrder = function (workorder) {
        workorder.$remove();
        $state.reload();
        activate();
        $scope.tableParams.reload();

    };
});