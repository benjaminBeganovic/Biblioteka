angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function($http) {
    
    var serviceBase = 'http://nwtbiblioteka.azurewebsites.net/';

    return {
        login: function(loginModel)
        {
            return $http({
                url: serviceBase + 'api/Auth/Login',
                method: "POST",
                data: JSON.stringify(loginModel),
                withCredentials: true
            });
        },
        logout: function () {
            return $http({
                url: serviceBase + 'api/Auth/Logout',
                method: "POST",
                withCredentials: true
            });
        },
        register: function(korisnikModel)
        {
            return $http({
                url: serviceBase + 'api/Korisniks',
                method: "POST",
                data: JSON.stringify(korisnikModel),
                withCredentials: true
            });
        },
        clanstva: function()
        {
            return $http({
                url: serviceBase + 'api/Clanstvoes',
                method: "GET",
                withCredentials: true
            });
        },
        svitipoviknjiga: function () {
            return $http({
                url: serviceBase + 'api/TipKnjiges',
                method: "GET",
                withCredentials: true
            });
        },
        naprednapretraga: function (pretragaModel) {
            return $http({
                url: serviceBase + 'api/Pretraga/Napredna',
                method: "GET",
                data: JSON.stringify(pretragaModel),
                withCredentials: true
            });
        },
        jednostavnapretraga: function (pretragaModel) {
            return $http({
                //url: serviceBase + 'api/Pretraga/Jednostavna',
                url: "https://nwtbiblioteka.azurewebsites.net/api/Pretraga/Jednostavna",
                method: "GET",
                params: pretragaModel,
                withCredentials: true
            });
        },
        pretragakod: function (kod) {
            return $http({
                url: serviceBase + 'api/Pretraga/Kod',
                method: "GET",
                data: JSON.stringify(kod),
                withCredentials: true
            });
        },
        dajkorisnika: function (id) {
            return $http({
                url: serviceBase + 'api/Korisniks/' + id,
                method: "GET",
                withCredentials: true
            });
        },
        dajsvekorisnike: function () {
            return $http({
                url: serviceBase + 'api/Korisniks',
                method: "GET",
                withCredentials: true
            });
        },
        obrisikorisnika: function (id) {
            return $http({
                url: serviceBase + 'api/Korisniks/' + id,
                method: "DELETE",
                withCredentials: true
            });
        },
        izmijenikorisnika: function (korisnikModel) {
            return $http({
                url: serviceBase + 'api/Korisniks/' + korisnikModel.id,
                method: "PUT",
                data: JSON.stringify(korisnikModel),
                withCredentials: true
            });
        },
        zaduzi: function (zaduzenjeModel) {
            return $http({
                url: serviceBase + 'api/Zaduzenja',
                method: "POST",
                data: JSON.stringify(zaduzenjeModel),
                withCredentials: true
            });
        },
        izmijenizaduzenje: function (zaduzenjeModel) {
            return $http({
                url: serviceBase + 'api/Zaduzenja/' + zaduzenjeModel.id,
                method: "PUT",
                data: JSON.stringify(zaduzenjeModel),
                withCredentials: true
            });
        },
        napraviclanstvo: function (clanstvoModel) {
            return $http({
                url: serviceBase + 'api/Clanstvoes',
                method: "POST",
                data: JSON.stringify(clanstvoModel),
                withCredentials: true
            });
        },
        rezervisi: function (rezervacijaModel) {
            return $http({
                url: serviceBase + 'api/Rezervacija',
                method: "POST",
                data: JSON.stringify(rezervacijaModel),
                withCredentials: true
            });
        }
	};
}]);