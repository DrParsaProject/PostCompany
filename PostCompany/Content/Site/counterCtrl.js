var app = angular.module('postApp', []);
app.controller('counterCtrl', function ($scope, $http, $window) {
    $scope.newuser = {};
    $scope.editProfile = {};
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
    $scope.statusStr = function (x) {
        if (x == 0) {
            return "NotPaid";
        } else if (x == 1) {
            return "Pending";
        } else if (x == 2) {
            return "Sending";
        } else if (x == 3) {
            return "Received";
        } else if (x == 4) {
            return "Canceled";
        } else if (x == 5) {
            return "Failed";
        }
    }
    $scope.strStatus = function (x) {
        if (x == "NotPaid") {
            return 0;
        } else if (x == "Pending") {
            return 1;
        } else if (x == "Sending") {
            return 2;
        } else if (x == "Received") {
            return 3;
        } else if (x == "Canceled") {
            return 4;
        } else if (x == "Failed") {
            return 5;
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
            refreshPostList();
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
        });
        $scope.newuser.Role = $scope.roleStr($scope.newuser.Role);
    };

    refreshPostList = function () {
        $http({
            method: 'GET',
            url: baseUrl + "Employee",
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            console.log(response.data);
            $scope.posts = response.data;
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
        });
    }
    refreshPostList();
});

