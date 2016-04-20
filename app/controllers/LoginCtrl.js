
angular.module('BibliotekaApp').controller("LoginCtrl", ["$scope","BibliotekaService", '$sce',
    function($scope, BibliotekaService, $sce){
    $scope.loginModel = {
      username : "",
      password : ""
    };
    $scope.loginMe = function () {
        if($scope.loginModel == null || $scope.loginModel.password == "" || $scope.loginModel.username == "")
            $scope.error = $sce.trustAsHtml("Morate unijeti sve podatke !");
        else
        {
            BibliotekaService.login($scope.loginModel)
                    .success(function (data)
                    {
                      
                        if (data.status === 200) {
                            $scope.error =$sce.trustAsHtml("Succes");

                           
                        }
                        else {
                            $scope.error = $sce.trustAsHtml(data.data);
                        }
            });
        }
        
    };
        
}]);