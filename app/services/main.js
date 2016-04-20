angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';
    $http.defaults.headers.post["Content-Type"] =
        "application/x-www-form-urlencoded; charset=UTF-8;";
    return {
        login: function(loginModel)
        {
            $http({
                url: serviceBase + 'api/Auth/Login',
              
                method: "POST",
                data: JSON.stringify(loginModel),
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                }
            }).success(function (response) {
                return response;
            }).error(function (error) {
                return error;
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