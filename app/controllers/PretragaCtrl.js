angular.module('BibliotekaApp').controller("PretragaCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
        var defaultTipKnjige = 0;
        var defaultJezik = 2;
        
        BibliotekaService.svijezici()
        .success(function (data, status) {
            $scope.jezici = data;
        })

        BibliotekaService.svitipoviknjiga()
        .success(function (data, status) {
            $scope.tipoviknjiga = data;
        })

        $scope.napredna = function () {
            BibliotekaService.naprednapretraga($scope.naprednaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })

        };

        $scope.jednostavna = function () {
            BibliotekaService.jednostavnapretraga($scope.jednostavnaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        $scope.pokodu = function () {
            BibliotekaService.pretragakod($scope.pokoduModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        $scope.rezervisi = function (idK) {

            $scope.rezervacijaModel.KnjigaID = 5;
            $scope.rezervacijaModel.KorisnikID = 6;

            BibliotekaService.rezervisi(rezervacijaModel)
            .success(function (data, status) {

                if (data.status == "co")
                    alert("Uspjesna rezervacija!");
            })
            .error(function (data, status) {
                alert("niste rezervisali");
            })
        };

    }]);