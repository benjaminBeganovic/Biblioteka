angular.module('BibliotekaApp').controller("ZKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
        var cc = 1;
        BibliotekaService.dajsvazaduzenjakorisnika(cc)
            .success(function (data, status) {
                if (data == null || data == "")
                {
                    $scope.lista_zaduzenja = null;
                    $scope.polje = $sce.trustAsHtml("Nemate zaduzenja!");
                }
                else
                    $scope.lista_zaduzenja = data;
            })
            .error(function (data, status) {
                $scope.lista_zaduzenja = null;
                $scope.polje = $sce.trustAsHtml("Greska! Pokusajte ponovo!");
            })
    }]);