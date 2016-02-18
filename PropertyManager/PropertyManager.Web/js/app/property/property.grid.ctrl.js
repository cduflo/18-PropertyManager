angular.module('app').controller('PropertyGridController', function ($scope, $state, PropertyResource) {
    function activate() {
        $scope.properties = PropertyResource.query();
    }
    activate();

    $scope.deleteProperty = function (property) {
        property.$remove();
        activate();
        $state.reload();
        // $scope.tableParams.reload();
        //$state.go('.', null, { reload: true });
    };
});