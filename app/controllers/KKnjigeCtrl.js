angular.module('BibliotekaApp').controller("KKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
        var kk = "";
        BibliotekaService.dajkriticneknjige(kk)
            .success(function (data, status) {
                if (data == null || data == "")
                {
                    $scope.lista_knjiga = null;
                    $scope.polje = $sce.trustAsHtml("Nema kriticnih knjiga");
                }
                else
                    $scope.lista_knjiga = data;
            })
            .error(function (data, status) {
                $scope.lista_knjiga = null;
                $scope.polje = $sce.trustAsHtml("Greska! Pokusajte ponovo!");
            })

    }]);