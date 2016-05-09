angular.module('BibliotekaApp').controller("ResetPassCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {

    $scope.getPass = function () {
        console.log("pass");
        BibliotekaService.resetujpass($scope.email)
            .success(function (data, status) {
                $scope.error = $sce.trustAsHtml("Uspješno je resetovana šifra. Provjerite mail!");
            })
            .error(function (data, status) {
                
                $scope.error = $sce.trustAsHtml("Ne postoji taj email!");
            })
    };
       
}]);