﻿
angular.module('BibliotekaApp').controller("PretragaCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
        $('#kopija').hide();
        BibliotekaService.svijezici()
        .success(function (data, status) {
            $scope.jezici = data;
        })

        BibliotekaService.svitipoviknjiga()
        .success(function (data, status) {
            $scope.tipoviknjiga = data;
        })

        $scope.napredna = function () {
            
        };

        $scope.jednostavna = function () {
            console.log($scope.jednostavnaModel);
            BibliotekaService.jednostavnapretraga($scope.jednostavnaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {

            })
        };

        $scope.pokodu = function () {

        };

    }]);