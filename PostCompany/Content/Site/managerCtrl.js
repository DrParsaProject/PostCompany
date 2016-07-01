var app = angular.module('postApp', []);
app.controller('managerCtrl', function ($scope, $http, $window) {
    $scope.newuser = {};
    $scope.editEmp = {};
    $scope.editProfileUser = {};
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
            location.reload(true)
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            $('#newEmpFormContainer').addClass('animated shake');
            $('#newEmpFormContainer').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#newEmpFormContainer').removeClass('animated shake');
            });
        });
        $scope.newuser.Role = $scope.roleStr($scope.newuser.Role);
    };
    //refreshEmpList = function () {
    //    $http({
    //        method: 'GET',
    //        url: baseUrl + "Employee",
    //        data: {},
    //        headers: { 'Content-Type': 'application/json' }
    //    }).then(function (response) {
    //        console.log(response.data);
    //        $scope.emps = response.data;
    //    }, function (response) {
    //        //Second function handles error
    //        console.log(":| " + response.data);
    //    });
    //}
    //refreshEmpList();
    $scope.EditProfile = function () {
        if ($scope.editProfileUser.ConfirmNewPassword != $scope.editProfileUser.NewPassword)
            return;
        $http({
            method: 'PUT',
            url: baseUrl + "Employee/" + myId,
            data: $scope.editProfileUser,
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $('#editFC').addClass('animated fadeOutDown');
            $('#editFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFC').removeClass('animated fadeOutDown');
                $('#editFC').addClass('animated fadeInDown');
            });
            $('#editFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFC').removeClass('animated fadeInDown');
            });
            location.reload(true);
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            $('#editFC').addClass('animated shake');
            $('#editFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFC').removeClass('animated shake');
            });

        });
    };
    $scope.EditEmp = function () {
        $http({
            method: 'PUT',
            url: baseUrl + "Employee/" + $scope.editEmp.id,
            data: $scope.editEmp,
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $('#editFormC').addClass('animated fadeOutDown');
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated fadeOutDown');
                $('#editFormC').addClass('animated fadeInDown');
            });
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated fadeInDown');
            });
            location.reload(true);
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            $('#editFormC').addClass('animated shake');
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated shake');
            });
        });
    };
    $scope.DeleteEmp = function () {
        $http({
            method: 'DELETE',
            url: baseUrl + "Employee/" + $scope.editEmp.id,
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $('#editFormC').addClass('animated fadeOutDown');
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated fadeOutDown');
                $('#editFormC').addClass('animated fadeInDown');
            });
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated fadeInDown');
            });
            location.reload(true);
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            $('#editFormC').addClass('animated shake');
            $('#editFormC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFormC').removeClass('animated shake');
            });
        });
    };
    
    Editpost = function (item) {
        console.log(item);
        atts = item.getElementsByTagName("td");
        //$scope.editEmp.Name = atts[2].innerText;
        $scope.editEmp.id = atts[0].innerText;
        //$scope.editEmp.Role = atts[3].innerText;
    }
    $scope.GetProfile = function () {
        if ($scope.editProfileUser.ConfirmNewPassword != $scope.editProfileUser.NewPassword)
            return;
        $http({
            method: 'GET',
            url: baseUrl + "Employee/" + myId,
            data: {},
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.editProfileUser = response.data;
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
        });
    };
    $scope.GetProfile();
});

