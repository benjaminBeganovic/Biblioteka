angular.module('BibliotekaApp').controller("RKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {

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

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');

    }]);