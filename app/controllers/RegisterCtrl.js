angular.module('BibliotekaApp').controller("RegisterCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

    $scope.regMe = function () {
        if ($scope.korisnikModel == null || $scope.korisnikModel.password == ""
             || $scope.korisnikModel.username == "" || $scope.korisnikModel.ime == ""
             || $scope.korisnikModel.prezime == "" || $scope.korisnikModel.telefon == ""
             || $scope.korisnikModel.adresa == "" || $scope.korisnikModel.email == ""
             || $scope.retypeModel.password == "")
            $scope.error = $sce.trustAsHtml("Morate unijeti sve podatke !");
        else if ($scope.retypeModel.password != $scope.korisnikModel.password)
        {
            $scope.error = $sce.trustAsHtml("Passwordi nisu isti!");
        }
        else
        {
            BibliotekaService.register($scope.korisnikModel)
            .success(function (data, status) {
                $scope.error = $sce.trustAsHtml("Uspjesno ste registrovani! Molimo vas da se još verifikujete putem vašeg email-a!");
            })
            .error(function (data) {
                $scope.error = $sce.trustAsHtml("Greska u registarciji");
            });
        }
        
    };
        
}]);