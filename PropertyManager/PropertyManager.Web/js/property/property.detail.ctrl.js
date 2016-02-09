angular.module('app').controller('PropertyDetailController', function ($scope, $stateParams, PropertyResource) {
    //grab id from url bar
    //grab property from /api/properies/{propertyId}
    $scope.property = PropertyResource.get({ propertyId: $stateParams.id });
    
});