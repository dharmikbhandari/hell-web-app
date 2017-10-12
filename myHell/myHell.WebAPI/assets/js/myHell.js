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

        // route for the edituser page
        .when('/editUser/:id', {
            templateUrl: 'AddEditUser.html',
            controller: 'usersController'
        })





        // route for the categoryList page
        .when('/categoryList', {
            templateUrl: 'CategoryList.html',
            controller: 'categoryController'
        })

        // route for the addCategory page
        .when('/addCategory', {
            templateUrl: 'AddEditCategory.html',
            controller: 'categoryController'
        })

        // route for the editCategory page
        .when('/editCategory/:id', {
            templateUrl: 'AddEditCategory.html',
            controller: 'categoryController'
        })




        // route for the transactionList page
        .when('/transactionList', {
            templateUrl: 'transactionList.html',
            controller: 'transactionController'
        })

        // route for the addTransaction page
        .when('/addTransaction', {
            templateUrl: 'AddEditTransaction.html',
            controller: 'transactionController'
        })

        // route for the editTransaction page
        .when('/editTransaction/:id', {
            templateUrl: 'AddEditTransaction.html',
            controller: 'transactionController'
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
                    $scope.user.Id = response.data.Object.Table[0].Id;
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


myHell.controller('categoryController', function ($scope, $window, $http, $routeParams) {


    $scope.categories = [];

    $scope.message = "This is category page";

    $scope.AddCategory = function () {
        $window.location.href = '#/addCategory';
    };

    $scope.BackToCategoryList = function () {
        $window.location.href = '#/categoryList';
    };

    $scope.init = function () {
        GetAllCategories();

    };

    $scope.EditCategory = function () {
        if ($routeParams.id > 0) {
            $http({
                method: 'GET',
                url: apiBaseURL + 'category/getcategorybyid/' + $routeParams.id,
            }).then(function successCallback(response) {

                if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                    $scope.category = {};
                    $scope.category.Id = response.data.Object.Table[0].Id;
                    $scope.category.Category_Name = response.data.Object.Table[0].Category_Name;
                    $scope.category.Category_Type = response.data.Object.Table[0].Category_Type;
                    $scope.category.Active = response.data.Object.Table[0].Active;
                }
                else {
                    ShowNotification(response.data.Error, 'danger');
                }

                $scope.TableTitle = "Edit Category-" + response.data.Object.Table[0].Category_Name;

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                var message = response.status + " : " + response.statusText;
                ShowNotification(message, 'danger');
            });
        }
        else {
            $scope.TableTitle = "Add New Category";
        }
    };

    $scope.Save = function (category) {
        Save(category);
    }



    function GetAllCategories() {
        $http({
            method: 'GET',
            url: apiBaseURL + 'category/getallcategories/',
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                $scope.categories = response.data.Object.Table;
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


    function Save(category) {
        if (category != null) {

            $http({
                method: 'POST',
                url: apiBaseURL + 'category/savecategory/',
                data: JSON.stringify(category)
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
            ShowNotification("Please enter Category_Name/Category_Type", 'danger');
        }
    }


});

myHell.controller('transactionController', function ($scope, $window, $http, $routeParams) {
    $scope.transactions = {};
    $scope.transaction = {};
    $scope.Categories = {};
    $scope.message = "This is transactions page";

    $scope.AddTransaction = function () {
        $window.location.href = '#/addTransaction';
    };

    $scope.BackToTransactionList = function () {
        $window.location.href = '#/transactionList';
    };

    $scope.init = function () {
        GetAllTransactions();
        
    };

    $scope.EditTransaction = function () {
        GetAllCategories();
        if ($routeParams.id > 0) {
            $http({
                method: 'GET',
                url: apiBaseURL + 'transaction/gettransactionbyid/' + $routeParams.id,
            }).then(function successCallback(response) {

                if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                   
                    $scope.transaction.Id = response.data.Object.Table[0].Id;
                    $scope.transaction.Amount = response.data.Object.Table[0].Amount;
                    $scope.transaction.CategoryId = response.data.Object.Table[0].CategoryId;
                    $scope.transaction.UserId = response.data.Object.Table[0].UserId;
                    $scope.transaction.Active = response.data.Object.Table[0].Active;
                    //$scope.transaction.Category = $scope.Categories[0];

                    angular.forEach($scope.Categories, function (category, index) {
                        if (category.Id == response.data.Object.Table[0].CategoryId) {
                            $scope.transaction.Category = category;
                        }
                    });
                }
                else {
                    ShowNotification(response.data.Error, 'danger');
                }

                $scope.TableTitle = "Edit Transaction-" + response.data.Object.Table[0].Id;

            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                var message = response.status + " : " + response.statusText;
                ShowNotification(message, 'danger');
            });
        }
        else {
            $scope.TableTitle = "Add New Transaction";
        }
    };

    $scope.Save = function (transaction) {
        Save(transaction)
    }

    $scope.SetCategoryId = function (category) {
        $scope.transaction.CategoryId = category.Id;
    }

    function GetAllTransactions() {
        $http({
            method: 'GET',
            url: apiBaseURL + 'transaction/getalltransactions/',
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                $scope.transactions = response.data.Object.Table;
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

    function GetAllCategories() {
        $http({
            method: 'GET',
            url: apiBaseURL + 'category/getallcategories/',
        }).then(function successCallback(response) {
            // this callback will be called asynchronously
            // when the response is available
            if (response.data.Error == '' || response.data.Error == null || response.data.Error == undefined) {
                $scope.Categories = response.data.Object.Table;
                
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

    function Save(transaction) {
        console.log(transaction);
        if (transaction != null) {

            $http({
                method: 'POST',
                url: apiBaseURL + 'transaction/savetransaction/',
                data: JSON.stringify(transaction)
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
            ShowNotification("Please enter Amount/Category", 'danger');
        }
    }


});

