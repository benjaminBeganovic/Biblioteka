angular.module('BibliotekaApp').controller("RazduzenjeCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        
        $scope.getZaduzenja = function () {

            $scope.polje = $sce.trustAsHtml("");
            if ($scope.zaduzenjeModel == null || $scope.zaduzenjeModel.username == "")
                $scope.polje = $sce.trustAsHtml("Trebate unijeti username!");
            else {
                BibliotekaService.dajsvazaduzenjakorisnikabib($scope.zaduzenjeModel.username)
            .success(function (data, status) {
                if (data == null || data == "")
                    $scope.polje = $sce.trustAsHtml("Korisnik nema zaduzenja!");
                $scope.lista_zaduzenja = data;
            })
            .error(function (data, status) {
                $scope.lista_zaduzenja = null;
                $scope.polje = $sce.trustAsHtml("Pogresan username!");
            })
            }

        };

        $scope.razduzi = function (zaduzenje) {

            var idRez = zaduzenje.ID;
            var id = 'z' + idRez;
            var id2 = 'm' + idRez;
            var id3 = 'b' + idRez;
            document.getElementById(id2).innerHTML = "...";
            document.getElementById(id2).style.display = 'block';

            document.getElementById(id).style.display = 'none';

            BibliotekaService.razduziknjigu(zaduzenje.ID)
        .success(function (data, status) {

            poruka = "Uspjesno ste razduzili knjigu!";
            document.getElementById(id2).innerHTML = poruka;
            document.getElementById(id3).style.display = 'block';
            //document.getElementById(id3).scrollIntoView();
        })
        .error(function (data, status) {

            document.getElementById(id2).innerHTML = "Greska! Pokusajte ponovo!" + status;
            document.getElementById(id3).style.display = 'block';
            //document.getElementById(id3).scrollIntoView();
        });                
            
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
                $scope.getZaduzenja();
            };
        
        }]);