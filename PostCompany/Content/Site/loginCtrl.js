var app = angular.module('postApp', []);
app.controller('loginCtrl', function ($scope, $http, $window) {
    $scope.isClient = false;
    $scope.user = {};

    $scope.login = function () {
        if ($scope.isClient == false)
            $http({
                method: 'POST',
                url: baseUrl + "Employeelogin",
                data: $scope.user, //forms user object
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                //First function handles success
                console.log("success");
                console.log(response.data);
                if (response.data == 1) {
                    $window.location.href = '/Site/Manager';
                } else {

                }
            }, function (response) {
                //Second function handles error
                console.log(":| " + response.data);
                $('#FormContainer').addClass('animated shake');
                $('#FormContainer').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () { $('#FormContainer').removeClass('animated shake'); });
            });
        else
            $http({
                method: 'POST',
                url: baseUrl + "CustomerLogin",
                data: $scope.user, //forms user object
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                //First function handles success
                
            }, function (response) {
                //Second function handles error
                console.log(":| " + response.data);
                $('#FormContainer').addClass('animated shake');
                $('#FormContainer').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () { $('#FormContainer').removeClass('animated shake'); });

            });
    }
});