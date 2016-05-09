angular.module('BibliotekaApp').controller("ResetPassCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {

    $scope.getPass = function () {
        console.log("pass");
    };
       
}]);