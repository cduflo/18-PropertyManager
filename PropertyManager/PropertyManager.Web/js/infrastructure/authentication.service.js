angular.module('app').factory('AuthenticationService', function ($http, $q, localStorageService, apiUrl) {
    var state = {
        authorized: false
    };

    function initialize() {
        //this will be called when the application loads
        var token = localStorageService.get('token');

        if (token) {
            state.authorized = true;
        };
    };

    function register(registration) {
        //this will call /api/accounts/register
        return $http.post(apiUrl + '/accounts/register', registration).then(
            function (response) {
                return response;
            });

    };

    function login(loginData) {
        //this will call /api/token
        var data = 'grant_type=password&username=' + loginData.username + '&password=' + loginData.password;

        var deferred = $q.defer();
        logout();

        $http.post(apiUrl + '/token', data, {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).success(function (response) {
            localStorageService.set('token', {
                token: response.access_token
            });
            localStorageService.set('user', {
                user: loginData.username
            });

            state.authorized = true;
            deferred.resolve(response);
        }).error(function (err, status) {
            logout();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    function logout() {
        //this will log the user out
        localStorageService.remove('token');
        localStorageService.remove('user');
        state.authorized = false;
    };

    return {
        state: state,
        initialize: initialize,
        register: register,
        login: login,
        logout: logout
    };
});