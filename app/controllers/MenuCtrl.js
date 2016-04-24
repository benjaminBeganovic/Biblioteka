
angular.module('BibliotekaApp').controller("MenuCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {
        $(".admin").hide();
        $(".clan").hide();
        $(".bibl").hide();
        $scope.logoutMe = function () {
                BibliotekaService.logout()
                .success(function (data, status, headers, config) {
                    $(".clan").hide();
                    $(".gost").show();
                    $(".admin").hide();
                    $(".bibl").hide();
                })
                .error(function (data) {
                });
            }

    }]);