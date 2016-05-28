var app = angular.module('BibliotekaApp');

app.config(['ChartJsProvider', function (ChartJsProvider) {
    // Configure all charts
    ChartJsProvider.setOptions({
        //colours: ['#FF5252', '#FF8A80'],
        responsive: true
    });
    // Configure all line charts
    ChartJsProvider.setOptions('Line', {
        datasetFill: false
    });
}]).controller("StatistikaCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate', '$timeout',
    function ($scope, BibliotekaService, $sce, $http, $translate, $timeout) {


        //$scope.labels = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        $scope.labels1 = ["Januar", "Februar", "Mart", "April", "Maj", "Juni", "Juli", "August", "Septembar", "Octobar", "Novembar", "Decembar"];
        $scope.series1 = ['Rezervacije tokom prethodne godine', 'Rezervacije u ovoj godini'];
        $scope.data1 = [
          [10, 20, 30, 40, 50, 60, 70, 80, 80, 80, 110, 120],
          [20, 30, 40, 40, 45]
        ];
        $scope.colours1 = ['#7a7a52', '#005ce6'];
        $scope.onClick1 = function (points, evt) {
            console.log(points, evt);
        };

        $scope.labels2 = ["Književnost", "Stručna literatura", "Filozofija", "Nauka", "Zakon", "Relegija", "Pismo", "Esej", "Dnevnici i Časopisi", "Autobiografija", "Biografija"];
        $scope.data2 = [100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100];













    }]);