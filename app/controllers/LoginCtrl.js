
angular.module('BibliotekaApp').controller("LoginCtrl", ["$scope","BibliotekaService", '$sce', '$http', '$cookies',
    function ($scope, BibliotekaService, $sce,$http, $cookies) {
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
            .success(function (data, status) {
                $scope.error = $sce.trustAsHtml("Uspjesno ste prijavljeni");
                $(".gost").hide();
                $(".clan").show();
                console.log(data);
                switch (data) {                 
                    case 'a':
                        $(".admin").show();
                        break;
                    case 'b':
                        $(".bibl").show();
                }
            })
            .error(function (data, status) {
                $scope.error = $sce.trustAsHtml("Greska u autentifikaciji");
            });
        }
        
    };
        
}]);