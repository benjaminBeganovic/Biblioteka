angular.module('BibliotekaApp').controller("KatalogCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {
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

    $scope.dodaj = function () {
        BibliotekaService.dodajknjigu($scope.naprednaModel)
        .success(function (data, status) {
            $scope.error = $sce.trustAsHtml(""+status);
        })
        .error(function (data, status) {
            $scope.error = $sce.trustAsHtml(""+status);
        })

    };
}]);