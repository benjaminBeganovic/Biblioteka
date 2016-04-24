
angular.module('BibliotekaApp').controller("MenuCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        $scope.logoutMe = function () {
                BibliotekaService.logout()
                .success(function (data, status, headers, config) {
                })
                .error(function (data) {
                });
            }

    }]);