angular.module('BibliotekaApp').controller("KKnjigeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate', 
    function ($scope, BibliotekaService, $sce, $http, $translate) {
        var kk = "";
        BibliotekaService.dajkriticneknjige(kk)
            .success(function (data, status) {
                if (data == null || data == "")
                {
                    $scope.lista_knjiga = null;
                    $scope.polje = $sce.trustAsHtml("nema_kriticnih");
                }
                else
                    $scope.lista_knjiga = data;
            })
            .error(function (data, status) {
                $scope.lista_knjiga = null;
                $scope.polje = $sce.trustAsHtml("greska_ponovo");
            })

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');

    }]);