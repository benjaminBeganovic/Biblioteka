
app.controller("LoginCtrl", ['$scope', 'BibliotekaService',
    function($scope, BibliotekaService){
    
    $scope.loginModel = {
      username : "",
      password : ""
    };
    $scope.error = "dsad";
    $scope.loginMe = function(){
        
        if($scope.loginModel == null || $scope.loginModel.password == "" || $scope.loginModel.username == "")
            alert("Morate unijeti sve podatke !");
        else
        {
            BibliotekaService.login($scope.loginModel)
                    .then(function (data)
            {
                
            });
        }
    };
        
}]);