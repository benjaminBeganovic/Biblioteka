angular.module('BibliotekaApp').controller("ZaduzeneknjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {

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

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');


    }]);