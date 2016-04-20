angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';
    
    return {
        login: function(loginModel)
        {
                        return $http({
                            method: 'POST',
                            url: serviceBase + 'api/Auth/Login',
                            data: loginModel,
                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                        }).success(function () {
                        })
                                .error(function () {
                                });
                    },
        register: function(registerModel)
                    {
                        return $http.post(serviceBase + 'api/Auth/Register', registerModel)
								.success(function () {
                                })
                                .error(function(){
                                });
					}
	};
}]);