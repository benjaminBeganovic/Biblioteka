
angular.module('BibliotekaApp').controller("LoginCtrl", ["$scope","BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, vcRecaptchaService,$http) {
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
            .then(function (data)
            {
                $scope.error = $sce.trustAsHtml(data.status);
            });
        }
        
    };
        
}]);