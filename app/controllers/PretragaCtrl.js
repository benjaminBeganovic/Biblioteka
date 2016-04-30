angular.module('BibliotekaApp').controller("PretragaCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
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

        $scope.napredna = function () {
            BibliotekaService.naprednapretraga($scope.naprednaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })

        };

        $scope.jednostavna = function () {
            BibliotekaService.jednostavnapretraga($scope.jednostavnaModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        $scope.pokodu = function () {
            BibliotekaService.pretragakod($scope.pokoduModel)
            .success(function (data, status) {
                $scope.rezultat = data;
            })
            .error(function (data, status) {
                $scope.rezultat = null;
            })
        };

        //dio za rezervisanje
        $scope.rezervisi = function (event) {

            var id = 'r' + event.target.id.substring(1, event.target.id.length);
            document.getElementById(id).style.display = 'none';
            var id2 = 'm' + event.target.id.substring(1, event.target.id.length);
            document.getElementById(id2).style.display = 'block';
            var id3 = 'b' + event.target.id.substring(1, event.target.id.length);

            var idK = event.target.id.substring(1, event.target.id.length);

            BibliotekaService.rezervisi(idK)
            .success(function (data, status) {

                var poruka;
                if (data.status == "co")
                    poruka = "Uspjesno ste rezervisali knjigu! Po knjigu bi trebali doci u naredna dva dana."
                else if (data.status == "wa")
                    poruka = "Uspjesno ste rezervisali knjigu! U narednim danima cete dobiti obavijest, o zaduzenju iste.";
                else
                    poruka = data.status;

                document.getElementById(id2).innerHTML = poruka;
                document.getElementById(id3).style.display = 'block';
                //document.getElementById(id3).scrollIntoView();
            })
            .error(function (data, status) {

                if (status == 401)
                    document.getElementById(id2).innerHTML = "Da bi rezervisali knjigu trebate biti clan!";
                else
                    document.getElementById(id2).innerHTML = "Greska! Pokusajte ponovo!";

                document.getElementById(id3).style.display = 'block';
                //document.getElementById(id3).scrollIntoView();
            })
        };

        $scope.dijalog = function (event) {

            var id = 'r' + event.target.id.substring(1, event.target.id.length);
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
        };

    }]);