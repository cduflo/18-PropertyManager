angular.module('app').controller('LeaseDetailController', function ($scope, $state, $stateParams, LeaseResource) {
    //grab id from url bar
    //grab property from /api/leases/{leaseId}
    $scope.lease = LeaseResource.get({ leaseId: $stateParams.id });
    $scope.title = "Lease Detail: {{lease.LeaseId}}";


    $scope.saveLease = function () {
        $scope.lease.$update();
        alert("Save Successful!");
        $state.go('lease.grid');
    };
});