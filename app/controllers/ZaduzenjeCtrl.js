angular.module('BibliotekaApp').controller("ZaduzenjeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        
        $scope.getRezervacije = function () {

            if ($scope.zaduzenjeModel == null || $scope.zaduzenjeModel.username == "")
                $scope.polje = $sce.trustAsHtml("Trebate unijeti username!");
            else {
                BibliotekaService.dajsverezervacijekorisnika($scope.zaduzenjeModel.username)
            .success(function (data, status) {
                $scope.lista_rezervacija = data;
            })
            .error(function (data, status) {
                $scope.lista_rezervacija = null;
                $scope.polje = $sce.trustAsHtml("Korisnik nema rezervacija!");
            })
            }

        };

        $scope.zaduzi = function () {

            if ($scope.zaduzenjeModel == null || $scope.zaduzenjeModel.username == "")
                $scope.polje = $sce.trustAsHtml("Trebate unijeti username!");
            else {
                BibliotekaService.zaduzi($scope.zaduzenjeModel.username)
            .success(function (data, status) {
                $scope.lista_rezervacija = data;
            })
            .error(function (data, status) {
                $scope.lista_rezervacija = null;
                $scope.polje = $sce.trustAsHtml("Korisnik nema rezervacija!");
            })
            }

        };

    }]);