angular.module('BibliotekaApp').factory("BibliotekaService", ['$http', function ($http) {

    var serviceBase = 'http://nwtbiblioteka1.azurewebsites.net/';

    return {
        login: function (loginModel) {
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
        register: function (korisnikModel) {
            return $http({
                url: serviceBase + 'api/Korisniks',
                method: "POST",
                data: JSON.stringify(korisnikModel),
                withCredentials: true
            });
        },
        clanstva: function () {
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
        loginlogs: function () {
            return $http({
                url: serviceBase + 'api/Korisniks/LoginLogs',
                method: "GET",
                withCredentials: true
            });
        },
        aktivnost: function (count) {
            return $http({
                url: serviceBase + 'api/Korisniks/Aktivnost',
                method: "GET",
                withCredentials: true,
                params: { count: count }
            });
        },

        aktivnostautori: function (count) {
            return $http({
                url: serviceBase + 'api/Autors/PopularniAutori',
                method: "GET",
                withCredentials: true,
                params: { size: count }
            });
        },

        glavniautori: function (count) {
            return $http({
                url: serviceBase + 'api/Autors/GlavniAutori',
                method: "GET",
                withCredentials: true,
                params: { size: count }
            });
        },
        glavnijezici: function (count) {
            return $http({
                url: serviceBase + 'api/Jeziks/NajpopularnijiJezici',
                method: "GET",
                withCredentials: true,
                params: { size: count }
            });
        },
        glavnitipovi: function (count) {
            return $http({
                url: serviceBase + 'api/TipKnjiges/NajpopularnijiTipovi',
                method: "GET",
                withCredentials: true,
                params: { size: count }
            });
        },
        svitipoviracuna: function () {
            return $http({
                url: serviceBase + 'api/TipRacunas',
                method: "GET",
                withCredentials: true
            });
        },
        svijezici: function () {
            return $http({
                url: serviceBase + 'api/Jeziks',
                method: "GET",
                withCredentials: true
            });
        },
        naprednapretraga: function (pretragaModel) {
            return $http({
                url: serviceBase + 'api/Pretraga/Napredna',
                method: "GET",
                params: pretragaModel,
                withCredentials: true
            });
        },
        jednostavnapretraga: function (pretragaModel) {
            return $http({
                url: serviceBase + "api/Pretraga/Jednostavna",
                method: "GET",
                params: pretragaModel,
                withCredentials: true
            });
        },
        pretragakod: function (kod) {
            return $http({
                url: serviceBase + 'api/Pretraga/Kod',
                method: "GET",
                params: kod,
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
                url: serviceBase + 'api/Korisniks/SviKorisnici',
                method: "GET",
                withCredentials: true
            });
        },
        sviizdavaci: function () {
            return $http({
                url: serviceBase + 'api/Izdavacs',
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
        promjenirolu: function (id, role) {
            return $http({
                url: serviceBase + 'api/Korisniks/ChangeRole',
                method: "POST",
                params: { id: id, role: role },
                withCredentials: true
            });
        },
        banuj: function (id) {
            return $http({
                url: serviceBase + 'api/Korisniks/BanKorisnik',
                method: "POST",
                params: { id: id },
                withCredentials: true
            });
        },
        dajsvazaduzenja: function () {
            return $http({
                url: serviceBase + 'api/Zaduzenja',
                method: "GET",
                withCredentials: true
            });
        },
        dajsvazaduzenjakorisnikabib: function (username) {
            return $http({
                url: serviceBase + 'api/Zaduzenja',
                method: "GET",
                params: { username: username },
                withCredentials: true
            });
        },
        dajsvazaduzenjakorisnika: function (cc) {
            return $http({
                url: serviceBase + 'api/Zaduzenja',
                method: "GET",
                params: { cc: cc },
                withCredentials: true
            });
        },
        razduziknjigu: function (zid) {
            return $http({
                url: serviceBase + 'api/Zaduzenja',
                method: "GET",
                params: { zid: zid },
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
        dajsverezervacijekorisnikabib: function (username) {
            return $http({
                url: serviceBase + 'api/Rezervacija',
                method: "GET",
                params: { username: username },
                withCredentials: true
            });
        },
        dajsverezervacijekorisnika: function () {
            return $http({
                url: serviceBase + 'api/Rezervacija',
                method: "GET",
                withCredentials: true
            });
        },
        dajkriticneknjige: function (kk) {
            return $http({
                url: serviceBase + 'api/Knjigas',
                method: "GET",
                params: { kk: kk },
                withCredentials: true
            });
        },
        rezervisi: function (idKnjige) {
            return $http({
                url: serviceBase + 'api/Rezervacija',
                method: "POST",
                params: { idKnjige: idKnjige },
                withCredentials: true
            });
        },
        resetujpass: function (email) {
            return $http({
                url: serviceBase + 'api/Korisniks/ResetPass',
                method: "POST",
                params: { email: email }
            });
        },
        dodajknjigu: function (knjigaModel) {
            return $http({
                url: serviceBase + 'api/Knjigas',
                method: "POST",
                withCredentials: true,
                data: knjigaModel
            });
        },
        obrisiknjigu: function (id) {
            return $http({
                url: serviceBase + 'api/Knjigas/DeleteKnjiga/' + id,
                method: "POST",
                withCredentials: true
            });
        },
        dajsveknjige: function () {
            return $http({
                url: serviceBase + 'api/Knjigas',
                method: "GET",
                withCredentials: true
            });
        },
        brojknjigapokategorijama: function () {
            return $http({
                url: serviceBase + 'api/Knjigas',
                method: "GET",
                withCredentials: true,
                params: { numofcat: "" }
            });
        },
        rezervacijeugodini: function () {
            return $http({
                url: serviceBase + 'api/Rezervacija',
                method: "GET",
                withCredentials: true,
                params: { ly: "", cy: "", comp: "" }
            });
        }

    };
}]);