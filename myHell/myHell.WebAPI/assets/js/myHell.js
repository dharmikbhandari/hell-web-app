var myHell = angular.module('myHell', ['ngRoute']);

var apiBaseURL = "https://localhost:44300/api/";

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

        // route for the login page
        .when('/login', {
        templateUrl: 'login.html',
        controller: 'loginController'
        })

        // route for the userList page
        .when('/userList', {
            templateUrl: 'UserList.html',
            controller: 'usersController'
        })

        // route for the adduser page
        .when('/addUser', {
            templateUrl: 'AddEditUser.html',
            controller: 'usersController'
        })

        // route for the adduser page
        .when('/editUser/:id', {
            templateUrl: 'AddEditUser.html',
            controller: 'usersController'
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

myHell.controller('usersController', function ($scope, $window, $http,$routeParams) {


    $scope.users = [];

    $scope.message = "This is Users page";

    $scope.AddUser = function () {
        $window.location.href = '#/addUser';
    };

    $scope.BackToUserList = function () {
        $window.location.href = '#/userList';
    };

    $scope.init = function () {
        GetAllUsers();
        
    };

    $scope.EditUser = function () {
        if ($routeParams.id > 0) {
            $http({
                method: 'GET',
                url: apiBaseURL + 'user/getuserbyid/' + $routeParams.id,
            }).then(function successCallback(response) {

                if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                    $scope.user = {};
                    $scope.user.Name = response.data.Object.Table[0].Name;
                    $scope.user.Email = response.data.Object.Table[0].Email;
                    $scope.user.Password = response.data.Object.Table[0].Password;
                    $scope.user.Active = response.data.Object.Table[0].Active;
                }
                else {
                    ShowNotification(response.data.Error, 'danger');
                }

                $scope.TableTitle = "Edit User-" + response.data.Object.Table[0].Name;

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
               var message = response.status + " : " + response.statusText;
                ShowNotification(message, 'danger');
            });
        }
        else {
            $scope.TableTitle = "Add New User";
        }
    };

    $scope.Save = function (user) {
        Save(user)
    }



    function GetAllUsers() {
        $http({
            method: 'GET',
            url: apiBaseURL + 'user/getallusers/',
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                $scope.users = response.data.Object.Table;
            }
            else {
                ShowNotification(response.data.Error, 'danger');
            }

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.

            var message = response.status + " : " + response.statusText;
            ShowNotification(message, 'danger');
        });
    };

    
    function Save(user) {
        if (user != null) {

            $http({
                method: 'POST',
                url: apiBaseURL + 'user/saveuser/',
                data: JSON.stringify(user)
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                
                if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                    ShowNotification(response.data.Message, 'success');
                }
                else {
                    ShowNotification(response.data.Error, 'danger');
                }
                
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
    }


});
