angular.module('BibliotekaApp').controller("RegisterCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
function ($scope, BibliotekaService, $sce, $http, $translate) {


    var passRegex = new RegExp(/^(?=.*\d).{4,8}$/);
    var emailRegex = new RegExp(/\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/);

    $scope.gRecaptchaResponse = '';
    $scope.$watch('gRecaptchaResponse', function () {
        $scope.expired = false;
    });
    $scope.expiredCallback = function expiredCallback() {
        $scope.expired = true;

    };

    $scope.regMe = function () {
        $scope.error = $sce.trustAsHtml("...");
        if ($scope.korisnikModel == null || $scope.korisnikModel.password == ""
             || $scope.korisnikModel.username == "" || $scope.korisnikModel.ime == ""
             || $scope.korisnikModel.prezime == "" || $scope.korisnikModel.telefon == ""
             || $scope.korisnikModel.adresa == "" || $scope.korisnikModel.email == ""
             || $scope.retypeModel.password == "")
            $scope.error = $sce.trustAsHtml("sve_podatke_unijeti");
        else if ($scope.retypeModel.password != $scope.korisnikModel.password) {
            $scope.error = $sce.trustAsHtml("sifre_nisu_iste");
        }
        else if (!passRegex.test($scope.korisnikModel.password)) {
            $scope.error = $sce.trustAsHtml("sifra_treba_da_bude");
        }
        else if (!emailRegex.test($scope.korisnikModel.email)) {
            $scope.error = $sce.trustAsHtml("email_nije_validan");
        }
        else if ($scope.gRecaptchaResponse == '' || $scope.gRecaptchaResponse == null) {
            $scope.error = $sce.trustAsHtml("captcha_validacija");
        }
        else if ($scope.expired == true) {
            $scope.error = $sce.trustAsHtml("captcha_validacija");
        }
        else {
            BibliotekaService.register($scope.korisnikModel)
            .success(function (data, status) {

                if (data == "Izaberite drugi username!")
                    $scope.error = $sce.trustAsHtml("drugi_username");
                else
                    $scope.error = $sce.trustAsHtml("uspjesna_registracija");
            })
            .error(function (data) {
                $scope.error = $sce.trustAsHtml("greska_ponovo");
            });
        }

    };


    if (document.getElementById("cLang").innerHTML == "BS")
        $translate.use('bs');
    else
        $translate.use('en');
}]);















