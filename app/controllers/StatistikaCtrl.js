var app = angular.module('BibliotekaApp');

app.config(['ChartJsProvider', function (ChartJsProvider) {
    // Configure all charts
    ChartJsProvider.setOptions({
        responsive: true
    });
    ChartJsProvider.setOptions('Line', {
        datasetFill: false
    });
}]);

app.controller("StatistikaCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {

        BibliotekaService.rezervacijeugodini()
            .success(function (data, status) {
                if (data != null || data != "")
                {
                    var arrl = [];
                    var arrc = [];
                    arrl.push(0); arrc.push(0);
                    var i = 0;
                    for(; i < 12 && i < data.length; i++)
                        arrl.push(data[i]);

                    for(; i < 24 && i < data.length; i++)
                        arrc.push(data[i]);

                    $scope.data1 = [arrl, arrc];
                }
                
            })
            .error(function (data, status) {
                //nema podataka
            })

        BibliotekaService.brojknjigapokategorijama()
            .success(function (data, status) {
                if (data != null || data != "")
                    $scope.data2 = data;
            })
            .error(function (data, status) {
                //nema podataka
            })


        $scope.labels1 = ["", "Januar", "Februar", "Mart", "April", "Maj", "Juni", "Juli", "August", "Septembar", "Octobar", "Novembar", "Decembar"];
        $scope.series1 = ['Rezervacije u prethodnoj godini', 'Rezervacije u ovoj godini'];
        $scope.colours1 = ['#7a7a52', '#005ce6'];
        $scope.onClick1 = function (points, evt) {
            console.log(points, evt);
        };

        $scope.labels2 = ["Književnost", "Stručna literatura", "Filozofija", "Nauka", "Zakon", "Relegija", "Pismo", "Esej", "Dnevnici i Časopisi", "Autobiografija", "Biografija"];
}]);