angular.module('app').controller('WorkOrderAddController', function ($scope, $state, WorkOrderResource) {

    $scope.workorder = {};

    $scope.title = "Add Work Order";

    $scope.saveWorkOrder = function () {
        WorkOrderResource.save($scope.workorder, function (data) {
            $scope.workorder = {};
        });
        alert("Save Successful!");
        $state.go('workorder.grid');
    };

});