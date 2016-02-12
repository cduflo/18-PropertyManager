angular.module('app').controller('PropertyAddController', function ($scope, $state,  PropertyResource) {

    $scope.property = {};

    $scope.title = "Add Property";

    $scope.saveProperty = function () {
        PropertyResource.save($scope.property, function (data) {
            $scope.property = {};
        });
        $state.go('property.grid');
    };

});