angular.module('app').controller('PropertyDetailController', function ($scope, $stateParams, $state, PropertyResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.property = PropertyResource.get({ propertyId: $stateParams.id });
    $scope.title = "Property Detail";

    $scope.saveProperty = function () {
        $scope.property.$update();
        alert("Save Successful!");
        $state.go('property.grid');
    };
});