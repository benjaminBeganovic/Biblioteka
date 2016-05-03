angular.module('BibliotekaApp').controller("PretragaCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate', 
function ($scope, BibliotekaService, $sce, $http, $translate) {
        var defaultTipKnjige = 0;
        var defaultJezik = 2;
        var proslaPretraga;

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
                proslaPretraga = "napredna";
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })

        };

        $scope.jednostavna = function () {
            BibliotekaService.jednostavnapretraga($scope.jednostavnaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
                proslaPretraga = "jednostavna";
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        $scope.pokodu = function () {
            BibliotekaService.pretragakod($scope.pokoduModel)
            .success(function (data, status) {
                $scope.rezultat = data;
                proslaPretraga = "pokodu";
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        $scope.osvjeziPretragu = function () {

            if (proslaPretraga == "napredna")
                $scope.napredna();
            else if (proslaPretraga == "jednostavna")
                $scope.jednostavna();
            else
                $scope.pokodu();
        };

        $scope.trans = function () {
            if (document.getElementById("cLang").innerHTML == "BS")
                $translate.use('bs');
            else
                $translate.use('en');
        };

    //dio za rezervisanje
        $scope.polje = "da_li_ste_sig_za_rezervaciju";
        var idK = "";

        $scope.rezervisi = function () {

            $scope.polje = "...";
            //treba blokirati UI i poslije deblokirati !!!
            document.getElementById("da").style.display = 'none';
            document.getElementById("ne").style.display = 'none';

            BibliotekaService.rezervisi(idK)
            .success(function (data, status) {

                var poruka;
                if (data.status == "co")
                    poruka = "uspjesna_rezervaicija";
                else if (data.status == "wa")
                    poruka = "uspjesna_rezervaicija_wa";
                else
                    poruka = data.status;

                $scope.polje = poruka;
            })
            .error(function (data, status) {

                if (status == 401)
                    $scope.polje = "trebate_biti_clan";
                else
                    $scope.polje = "greska_ponovo";
            })

            var ok = "Uredu";
            if (document.getElementById("cLang").innerHTML == "EN")
                ok = "Ok";

            document.getElementById("ne").innerHTML = ok;
            document.getElementById("ne").style.display = 'inline';

        };

        $scope.dijalog = function () {

            $scope.polje = "da_li_ste_sig_za_rezervaciju";
            document.getElementById("da").style.display = 'inline';
            //$scope.osvjeziPretragu();
        };

        $scope.postaviID = function (event) {

            idK = event.target.id;
        };

        $scope.trans();

    }]);