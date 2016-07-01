var app = angular.module('postApp', []);
app.controller('transportCtrl', function ($scope, $http, $window) {
    $scope.editProfileUser = {};
    $scope.editPost = {};
    $scope.logout = function () {
        console.log("asdfasdfasdfasdfasdfasdfas");
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

    $scope.submitEditPost = function () {
        console.log("asdfasdfasdfasdfasdf");
        $scope.editPost.Status = $scope.strStatus($scope.editPost.Status);
        $http({
            method: 'PUT',
            url: baseUrl + "Box/" + postID,
            data: $scope.editPost,
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $('#editPostFC').addClass('animated fadeOutDown');
            $('#editPostFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editPostFC').removeClass('animated fadeOutDown');
                $('#editPostFC').addClass('animated fadeInDown');
            });
            $('#editPostFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editPostFC').removeClass('animated fadeInDown');
            });
            location.reload(true);
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            console.log(response.data);
            $('#editPostFC').addClass('animated shake');
            $('#editPostFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editPostFC').removeClass('animated shake');
            });
        });
        $scope.editPost.Status = $scope.statusStr($scope.editPost.Status);
    };
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
        }, function (response) {
            //Second function handles error
            console.log(":| " + response.data);
            $('#editFC').addClass('animated shake');
            $('#editFC').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                $('#editFC').removeClass('animated shake');
            });

        });
    };
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

