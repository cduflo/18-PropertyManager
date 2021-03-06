﻿angular.module('app').controller('TenantGridController', function ($scope, $state, $stateParams, TenantResource) {
    function activate() {
        $scope.tenants = TenantResource.query();
    }
    activate();

    $scope.deleteTenant = function (tenant) {
        tenant.$remove();
        activate();
        $state.reload();
        // $scope.tableParams.reload();
        //$state.go('.', null, { reload: true });
    };
});