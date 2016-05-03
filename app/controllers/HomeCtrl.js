angular.module('BibliotekaApp').controller("HomeCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {


        

        if (document.getElementById("cLang").innerHTML == "BS")
            $translate.use('bs');
        else
            $translate.use('en');
    }]);