angular.module('BibliotekaApp').controller("ZaduzeneknjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        BibliotekaService.dajsvazaduzenja()
            .success(function (data, status) {

                if (data == null || data == "")
                {
                    $scope.lista_zaduzenja = null;
                    $scope.polje = $sce.trustAsHtml("Nema zaduzenih knjiga!");
                }

                $scope.lista_zaduzenja = data;
            })
            .error(function (data, status) {
                $scope.lista_zaduzenja = null;
                $scope.polje = $sce.trustAsHtml("Greska! Pokusajte ponovo!");
            })

    }]);