 var app = angular.module('BibliotekaApp', ['ngRoute'])
    .config(['$routeProvider',
        function($routeProvider) {
            $routeProvider
            .when('/Login', {
                templateUrl: 'views/LoginForm.html',
            })
			.when('/Register', {
                templateUrl: 'views/RegisterForm.html'
            })
            .otherwise({
                templateUrl: 'views/Home.html'
            });
		}
		]);