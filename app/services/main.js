angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';
    //$http.defaults.headers.post["Content-Type"] =
      //  "application/x-www-form-urlencoded; charset=UTF-8;";
    return {
        login: function(loginModel)
        {
            return $http.post(serviceBase + '/api/Auth/Login',
            JSON.stringify(loginModel));
        },
        register: function(registerModel)
                    {
            return $http.post(serviceBase + 'api/Auth/Register', registerModel);
		}
	};
}]);