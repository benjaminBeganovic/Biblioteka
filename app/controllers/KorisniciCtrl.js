angular.module('BibliotekaApp').controller("KorisniciCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {
    BibliotekaService.dajsvekorisnike()
    .success(function (data, status) {
        $scope.korisnici = data;
    })

    var idK = "";
    $scope.trans = function () {
        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');
    };

    function dijalog() {
        document.getElementById("da").style.display = 'inline';
        document.getElementById("ne").innerHTML = "ne";
        $scope.trans();
    }

    $scope.dijalog = dijalog;

    $scope.postaviID = function (event) {
        idK = event.target.id;
    };

    $scope.banuj = function () {
        BibliotekaService.banuj(parseInt(idK))
        .success(function (data, status) {
            BibliotekaService.dajsvekorisnike()
            .success(function (data, status) {
                $scope.korisnici = data;
            })
        })
        .error(function (data, status) {
        })
        dijalog();
    };

    $scope.trans();
}]);