angular.module('app').controller('LeaseGridController', function ($scope, $state,  LeaseResource) {
    function activate() {
        $scope.leases = LeaseResource.query();
    }
    activate();


    $scope.deleteLease = function (lease) {
        lease.$remove();
        activate();
        $state.reload();
        alert("Delete Successful!");
    };
});