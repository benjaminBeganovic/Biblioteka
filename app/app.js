var app = angular.module('BibliotekaApp', ['ngRoute', 'ngCookies'])
    .config(['$routeProvider',
        function($routeProvider) {
            $routeProvider
            .when('/Login', {
                templateUrl: 'views/LoginForm.html'
            })
			.when('/Register', {
                templateUrl: 'views/RegisterForm.html'
			})
            .when('/Napredna', {
                templateUrl: 'views/NaprednaPretragaForm.html'
            })
            .when('/Jednostavna', {
                templateUrl: 'views/JednostavnaPretragaForm.html'
            })
            .when('/Knjige', {
                templateUrl: 'views/JednostavnaPretragaForm.html'
            })
            .when('/PoKodu', {
                templateUrl: 'views/PoKoduPretragaForm.html'
            })
            .otherwise({
                templateUrl: 'views/Home.html'
            });
		}
    ])