var myHell = angular.module('myHell', ['ngRoute']);

var apiBaseURL = "http://localhost:56420/api/";

// configure our routes
myHell.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'home.html',
            controller: 'mainController'
        })

        // route for the home page
        .when('/dashboard', {
            templateUrl: 'dashboard.html',
            controller: 'dashboardController'
        })

        // route for the about page
        .when('/about', {
            templateUrl: 'about.html',
            controller: 'aboutController'
        })

        // route for the contact page
        .when('/contact', {
            templateUrl: 'contact.html',
            controller: 'contactController'
        })

        // route for the contact page
        .when('/login', {
        templateUrl: 'login.html',
        controller: 'loginController'
        })

        .otherwise({ redirectTo: '/login' });
});



function ShowNotification(message,type) {

    //type : danger,warning,success
    $.notify({
        icon: "notifications",
        message: message

    }, {
            type: type,
            timer: 4000,
            placement: {
                from: 'top',
                align: 'center'
            }
        });
}


// create the controller and inject Angular's $scope
myHell.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
    
});

myHell.controller('dashboardController', function ($scope) {
    // create a message to display in our view
    $scope.TestWrite = function () {
        
    };

});

myHell.controller('aboutController', function ($scope) {
    $scope.message = 'Look! I am an about page. Yeah...';
});

myHell.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});

myHell.controller('loginController', function ($scope, $window, $http) {

    
    $scope.GetIntoHell = function (user) {

        if (user != null) {

            $http({
                method: 'POST',
                url: apiBaseURL + 'account/login/',
                data: JSON.stringify(user)
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $window.location.href = '/web/index.html';
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.

                var message = response.status + " : " + response.statusText;
                ShowNotification(message, 'danger');
            });
        }
        else {
            ShowNotification("Please enter Email/Password", 'danger');
        }
    };

});
