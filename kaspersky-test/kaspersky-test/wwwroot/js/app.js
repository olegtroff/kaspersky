(function () {
    var app = angular.module('kasperskyApp', ['ngRoute',
        'ui.bootstrap',
        'angularFileUpload',
        'ja.isbn']).run(function ($http) { });
    app.config(function ($routeProvider) {
        $routeProvider
            .when('/',
                {
                    templateUrl: 'js/views/book.html',
                    controller: 'bookController'
                })
            .when('/authors',
                {
                    templateUrl: 'js/views/author.html',
                    controller: 'authorController'
                })
            .when('/publishings',
                {
                    templateUrl: 'js/views/publishinghouse.html',
                    controller: 'publishingHouseController'

                }).otherwise({ redirectTo: '/' });
    });
})();