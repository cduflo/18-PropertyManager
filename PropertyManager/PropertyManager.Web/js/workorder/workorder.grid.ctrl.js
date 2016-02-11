﻿angular.module('app').controller('WorkOrderGridController', function ($scope, $state,  WorkOrderResource) {
    function activate() {
        $scope.workorders = WorkOrderResource.query();
        $scope.workorders.forEach(function (wo) {
            wo.OpenedDate = Date(OpenedDate);
        });
    }
    activate();

    $scope.deleteWorkOrder = function (workorder) {
        workorder.$remove();
        activate();
        $state.reload();
        alert("Delete Successful!");
    };
});