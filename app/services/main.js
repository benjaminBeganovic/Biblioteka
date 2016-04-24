angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';
    //$http.defaults.headers.post["Content-Type"] =
      //  "application/x-www-form-urlencoded; charset=UTF-8;";
    return {
        login: function(loginModel)
        {
            return $http({
                url: serviceBase + '/api/Auth/Login',
                method: "POST",
                data: JSON.stringify(loginModel),
                withCredentials: true
            });
        },
        logout: function () {
            return $http({
                url: serviceBase + '/api/Auth/Logout',
                method: "POST",
                withCredentials: true
            });
        },
        register: function(registerModel)
                    {
            return $http.post(serviceBase + 'api/Auth/Register', registerModel);
        },
        clanstva: function()
        {
            return $http({
                url: serviceBase + '/api/Clanstvoes/',
                method: "GET",
                withCredentials: true
            });
        },
	};
}]);