angular.module('BibliotekaApp').controller("KatalogCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {
    BibliotekaService.svijezici()
    .success(function (data, status) {
        $scope.jezici = data;
    })

    BibliotekaService.svitipoviknjiga()
    .success(function (data, status) {
        $scope.tipoviknjiga = data;
    })

    BibliotekaService.sviizdavaci()
    .success(function (data, status) {
        $scope.izdavaci = data;
    })

    $scope.dodaj = function () {
        $scope.naprednaModel.TipKnjigeID = parseInt($scope.tip);
        $scope.naprednaModel.IzdavacID = parseInt($scope.izdavac);
        $scope.naprednaModel.JezikID = parseInt($scope.jezik);
        BibliotekaService.dodajknjigu($scope.naprednaModel)
        .success(function (data, status) {
            $scope.error = $sce.trustAsHtml(""+status);
        })
        .error(function (data, status) {
            $scope.error = $sce.trustAsHtml(""+status);
        })

    };
}]);