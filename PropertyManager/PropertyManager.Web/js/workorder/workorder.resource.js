angular.module('app').factory('WorkOrderResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/workorders/:workorderId', { workorderId: '@WorkOrderId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});