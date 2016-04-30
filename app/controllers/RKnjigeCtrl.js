angular.module('BibliotekaApp').controller("RKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        BibliotekaService.dajsverezervacijekorisnika()
            .success(function (data, status) {
                if (data == null || data == "")
                {
                    $scope.lista_rezervacija = null;
                    $scope.polje = $sce.trustAsHtml("Nemate rezervacija!");
                }
                else
                    $scope.lista_rezervacija = data;
            })
            .error(function (data, status) {
                $scope.lista_rezervacija = null;
                $scope.polje = $sce.trustAsHtml("Greska! Pokusajte ponovo!");
            })

    }]);