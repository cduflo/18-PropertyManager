angular.module('app').controller('LeaseAddController', function ($scope, $state, LeaseResource) {

    $scope.lease = {};

    $scope.title = "Add Lease";

    $scope.saveLease = function () {
        LeaseResource.save($scope.lease, function (data) {
            $scope.lease = {};
        });
        alert("Save Successful!");
        $state.go('lease.grid');
    };

});