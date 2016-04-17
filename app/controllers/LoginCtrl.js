
app.controller("LoginCtrl", ['$scope', 'BibliotekaService',
    function($scope, BibliotekaService){
    
    $scope.loginModel = {
      loginUsername : "",
      loginPsw : ""
    };
    
    $scope.loginMe = function(){
        
        if($scope.loginModel == null || $scope.loginModel.loginPsw == "" || $scope.loginModel.loginUsername == "")
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