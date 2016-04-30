angular.module('BibliotekaApp').controller("RegisterCtrl", ["$scope", "BibliotekaService", '$sce', '$http',
    function ($scope, BibliotekaService, $sce, $http) {

        var passRegex = new RegExp(/^(?=.*\d).{4,8}$/);
        var emailRegex = new RegExp(/\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/);

        $scope.regMe = function () {
            $scope.error = $sce.trustAsHtml("...");
        if ($scope.korisnikModel == null || $scope.korisnikModel.password == ""
             || $scope.korisnikModel.username == "" || $scope.korisnikModel.ime == ""
             || $scope.korisnikModel.prezime == "" || $scope.korisnikModel.telefon == ""
             || $scope.korisnikModel.adresa == "" || $scope.korisnikModel.email == ""
             || $scope.retypeModel.password == "")
            $scope.error = $sce.trustAsHtml("Morate unijeti sve podatke!");
        else if ($scope.retypeModel.password != $scope.korisnikModel.password)
        {
            $scope.error = $sce.trustAsHtml("Passwordi nisu isti!");
        }
        else if (!passRegex.test($scope.korisnikModel.password))
        {
            $scope.error = $sce.trustAsHtml("Password moze biti izmedju 4 i 8 karaktera, i mora imati barem jedan broj!");
        }
        else if (!emailRegex.test($scope.korisnikModel.email)) {
            $scope.error = $sce.trustAsHtml("Email nije validan!");
        }
        else
        {
            BibliotekaService.register($scope.korisnikModel)
            .success(function (data, status) {

                if (data == "Izaberite drugi username!")
                    $scope.error = $sce.trustAsHtml("Molimo uzmite drugi username!");
                else
                    $scope.error = $sce.trustAsHtml("Uspjesno ste registrovani! Molimo vas da se jos verifikujete putem vaseg email-a!");
            })
            .error(function (data) {
                $scope.error = $sce.trustAsHtml("Greska u registarciji");
            });
        }
        
    };
        
}]);