angular.module('BibliotekaApp').controller("LoginCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$cookies', '$window', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $cookies, $window, $translate) {
        $scope.loginModel = {
            username: "",
            password: ""
        };

        $scope.loginMe = function () {
            if ($scope.loginModel == null || $scope.loginModel.password == "" || $scope.loginModel.username == "")
                $scope.error = $sce.trustAsHtml("Morate unijeti sve podatke !");
            else {
                BibliotekaService.login($scope.loginModel)
                .success(function (data, status) {
                    $scope.error = $sce.trustAsHtml("Uspjesno ste prijavljeni");
                    $(".gost").hide();
                    console.log(data);
                    switch (data) {
                        case 'a':
                            $(".admin").show();
                            break;
                        case 'b':
                            $(".bibl").show();
                            break;
                        case 'c':
                            $(".clan").show();
                    }
                    $window.location.href = '/index.html#/Pocetna';
                })
                .error(function (data, status) {
                    $scope.error = $sce.trustAsHtml("Greska u autentifikaciji");
                });
            }

        };

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');

    }]);