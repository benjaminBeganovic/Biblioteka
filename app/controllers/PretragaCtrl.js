
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

    }]);