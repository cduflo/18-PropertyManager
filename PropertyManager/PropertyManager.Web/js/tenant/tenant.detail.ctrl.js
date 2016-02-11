angular.module('app').controller('TenantDetailController', function ($scope, $stateParams, $state, TenantResource) {
    //grab id from url bar
    //grab property from /api/tenantes/{tenantId}
    $scope.tenant = TenantResource.get({ tenantId: $stateParams.id });
    $scope.title = "Tenant Detail";


    $scope.saveTenant = function () {
        $scope.tenant.$update();
        alert("Save Successful!");
        $state.go('tenant.grid');
    };
});