angular.module('BibliotekaApp').controller("MenuCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {
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

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');

    }]);