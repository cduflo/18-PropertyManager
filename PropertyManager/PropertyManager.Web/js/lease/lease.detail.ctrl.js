angular.module('app').controller('LeaseDetailController', function ($scope, $stateParams, LeaseResource) {
    //grab id from url bar
    //grab property from /api/leases/{leaseId}
    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id });


    $scope.saveLease = function () {
        $scope.lease.$update();
        alert("Save Successful!");
    };
});