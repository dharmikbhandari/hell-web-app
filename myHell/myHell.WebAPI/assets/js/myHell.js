var myHell = angular.module('myHell', ['ngRoute','firebase']);

// configure our routes
myHell.config(function ($routeProvider) {
    $routeProvider

        // route for the home page
        .when('/', {
            templateUrl: 'home.html',
            controller: 'mainController'
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

myHell.factory("Auth", ["$firebaseAuth",
    function ($firebaseAuth) {
        return $firebaseAuth();
    }
]);

// create the controller and inject Angular's $scope
myHell.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
    
});

myHell.controller('aboutController', function ($scope) {
    $scope.message = 'Look! I am an about page. Yeah...';
});

myHell.controller('contactController', function ($scope) {
    $scope.message = 'Contact us! JK. This is just a demo.';
});

myHell.controller('loginController', function ($scope, $firebaseAuth, $window) {
    var authObj = $firebaseAuth();
    
    $scope.GetIntoHell = function (user) {
        
        if (user!=null) {
            authObj.$signInWithEmailAndPassword(user.Email, user.Password).then(function (firebaseUser) {
                $scope.message = "Authentication Success";
                $window.location.href = '/web/index.html';
            }).catch(function (error) {
                ShowNotification(error, 'danger');
            });
        }
    };

});
