app.factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';

    var filteredBooks = [];

    return {
        login: function(loginModel)
                    {
                        return $http.post(serviceBase + 'api/Auth/Login', loginModel)
								.success(function () {
                                })
                                .error(function(){
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