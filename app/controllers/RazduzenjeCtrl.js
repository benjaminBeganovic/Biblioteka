angular.module('BibliotekaApp').controller("RazduzenjeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate', 
    function ($scope, BibliotekaService, $sce, $http, $translate) {

        
        $scope.getZaduzenja = function () {

            $scope.polje = $sce.trustAsHtml("");
            if ($scope.zaduzenjeModel == null || $scope.zaduzenjeModel.username == "")
                $scope.polje = $sce.trustAsHtml("treba_usename");
            else {
                BibliotekaService.dajsvazaduzenjakorisnikabib($scope.zaduzenjeModel.username)
            .success(function (data, status) {
                if (data == null || data == "")
                    $scope.polje = $sce.trustAsHtml("korisnik_nema_zaduzenja");
                $scope.lista_zaduzenja = data;
            })
            .error(function (data, status) {
                $scope.lista_zaduzenja = null;
                $scope.polje = $sce.trustAsHtml("pogr_username");
            })
            }

        };

        $scope.razduzi = function (zaduzenje) {

            var idRez = zaduzenje.ID;
            var id = 'z' + idRez;
            var id2 = 'm' + idRez;
            var id3 = 'b' + idRez;
            //document.getElementById(id2).innerHTML = "...";
            document.getElementById(id2).style.display = 'inline';

            document.getElementById(id).style.display = 'none';

            BibliotekaService.razduziknjigu(zaduzenje.ID)
        .success(function (data, status) {

            $scope.message = $sce.trustAsHtml("uspjesno_ste_razduzili");
            document.getElementById(id3).style.display = 'inline';
            //document.getElementById(id3).scrollIntoView();
        })
        .error(function (data, status) {

            $scope.message = $sce.trustAsHtml("greska_ponovo");
            document.getElementById(id3).style.display = 'inline';
            //document.getElementById(id3).scrollIntoView();
        });                
            
        }

            $scope.dijalog = function (event) {
                var id = 'z' + event.target.id.substring(1, event.target.id.length);
                if (event.target.id.substring(0, 1) == "d") {
                    document.getElementById(id).style.display = 'inline';
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
                $scope.getZaduzenja();
            };

            if (document.getElementById("cLang").innerHTML == "BS")
                $translate.use('bs');
            else
                $translate.use('en');
        
        }]);