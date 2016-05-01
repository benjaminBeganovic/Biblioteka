angular.module('BibliotekaApp').controller("ZaduzenjeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {

        
        $scope.getRezervacije = function () {

            $scope.polje = $sce.trustAsHtml("");
            if ($scope.zaduzenjeModel == null || $scope.zaduzenjeModel.username == "")
                $scope.polje = $sce.trustAsHtml("Trebate unijeti username!");
            else {
                BibliotekaService.dajsverezervacijekorisnikabib($scope.zaduzenjeModel.username)
            .success(function (data, status) {
                if (data == null || data == "")
                    $scope.polje = $sce.trustAsHtml("Korisnik nema rezervacija!");
                $scope.lista_rezervacija = data;
            })
            .error(function (data, status) {
                $scope.lista_rezervacija = null;
                $scope.polje = $sce.trustAsHtml("Pogresan username!");
            })
            }

        };

        $scope.zaduzi = function (rezervacija) {

            var idRez = rezervacija.ID;
            var id = 'z' + idRez;
            var id2 = 'm' + idRez;
            var id3 = 'b' + idRez;
            var idRok = 'r' + idRez;
            document.getElementById(id2).innerHTML = "...";
            document.getElementById(id2).style.display = 'block';

            $scope.zaduzenjeModel.KorisnikID = rezervacija.KorisnikID;
            $scope.zaduzenjeModel.KnjigaID = rezervacija.KnjigaID;

            var rokValue = document.getElementById(idRok).value;
            if (rokValue < 5 || rokValue > 30) {
                document.getElementById(id3).style.display = 'block';
                document.getElementById(id2).innerHTML = "Rok treba biti izmedju 5 i 30 dana!";
            }
            else {
                document.getElementById(id).style.display = 'none';
                $scope.zaduzenjeModel.rok = new Date(new Date().getTime() + (rokValue * 24 * 60 * 60 * 1000));

                //zaduzi
                BibliotekaService.zaduzi($scope.zaduzenjeModel)
            .success(function (data, status) {
                var poruka;
                if (data.status == "nv")
                    poruka = "Uspjesno ste zaduzili knjigu!";
                else
                    poruka = data.status;

                document.getElementById(id2).innerHTML = poruka;
                document.getElementById(id3).style.display = 'block';
                //document.getElementById(id3).scrollIntoView();
            })
            .error(function (data, status) {

                document.getElementById(id2).innerHTML = "Greska! Pokusajte ponovo!";
                document.getElementById(id3).style.display = 'block';
                //document.getElementById(id3).scrollIntoView();
            });
            }
        }

            $scope.dijalog = function (event) {
                var id = 'z' + event.target.id.substring(1, event.target.id.length);
                if (event.target.id.substring(0, 1) == "d") {
                    document.getElementById(id).style.display = 'block';
                    //document.getElementById(id).scrollIntoView();
                }
                else {
                    document.getElementById(id).style.display = 'none';
                }
            };

            $scope.hideMessage = function (event) {

                var id = 'b' + event.target.id.substring(1, event.target.id.length);
                var id2 = 'm' + event.target.id.substring(1, event.target.id.length);
                document.getElementById(id).style.display = 'none';
                document.getElementById(id2).style.display = 'none';
                $scope.getRezervacije();
            };

            if (document.getElementById("cLang").innerHTML == "BS")
                $translate.use('bs');
            else
                $translate.use('en');
        
        }]);