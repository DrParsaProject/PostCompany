var app = angular.module('postApp', []);
app.controller('managerCtrl', function ($scope, $http, $window) {
    $scope.newuser = {};
    $scope.logout = function () {
        $http({
            method: 'POST',
            url: baseUrl + "Logout",
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $window.location.href = '/Site/Login';
        });
    };
    $scope.roleStr = function (x) {
        if (x == 0) {
            return "Manager";
        } else if (x == 1) {
            return "Counter";
        } else if (x == 2) {
            return "Transport";
        } else if (x == 3) {
            return "Weight";
        }
    }
    $scope.strRole = function (x) {
        if (x == "Manager") {
            return 0;
        } else if (x == "Counter") {
            return 1;
        } else if (x == "Transport") {
            return 2;
        } else if (x == "Weight") {
            return 3;
        }
    }
    $scope.submitNewEmp = function () {
        if ($scope.newuser.confirmPassword != $scope.newuser.Password)
            return;
        $scope.newuser.Role = $scope.strRole($scope.newuser.Role);
        $http({
            method: 'POST',
            url: baseUrl + "Employee",
            data: $scope.newuser,
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $('#newEmpFormContainer').addClass('animated fadeOutDown');
            $('#newEmpFormContainer').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#newEmpFormContainer').removeClass('animated fadeOutDown');
                $('#newEmpFormContainer').addClass('animated fadeInDown');
            });
            $('#newEmpFormContainer').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#newEmpFormContainer').removeClass('animated fadeInDown');
            });
            refreshEmpList();
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
        });
        $scope.newuser.Role = $scope.roleStr($scope.newuser.Role);
    };

    refreshEmpList = function () {
        $http({
            method: 'GET',
            url: baseUrl + "Employee",
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            console.log(response.data);
            $scope.emps = response.data;
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
        });
    }
    refreshEmpList();
});

