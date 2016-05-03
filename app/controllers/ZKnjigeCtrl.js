angular.module('BibliotekaApp').controller("ZKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {
        var cc = 1;
        BibliotekaService.dajsvazaduzenjakorisnika(cc)
            .success(function (data, status) {
                if (data == null || data == "")
                {
                    $scope.lista_zaduzenja = null;
                    $scope.polje = $sce.trustAsHtml("nemate_zaduzenja");
                }
                else
                    $scope.lista_zaduzenja = data;
            })
            .error(function (data, status) {
                $scope.lista_zaduzenja = null;
                $scope.polje = $sce.trustAsHtml("greska_ponovo");
            })

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');

    }]);