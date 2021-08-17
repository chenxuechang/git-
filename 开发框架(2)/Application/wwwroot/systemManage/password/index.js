function editpas() {
    var sourcePassword = $("#inputPassword0").val().trim();
    if (sourcePassword == null || sourcePassword == "") {
        $("#tdPasswordError").text("请输入原密码");
        $("#inputPassword0").focus();
        return;
    }
    var newpassword = $("#inputPassword1").val().trim();
    if (newpassword == null || newpassword == "") {
        $("#tdPasswordError").text("请输入新密码");
        $("#inputPassword1").focus();
        return;
    }
    var newpassword2 = $("#inputPassword2").val().trim();
    if (newpassword2 == null || newpassword2 == "") {
        $("#tdPasswordError").text("请输入新密码确认");
        $("#inputPassword2").focus();
        return;
    }
    if (newpassword != newpassword2) {
        $("#tdPasswordError").text("两次输入新密码不一致请重新输入");
        $("#inputPassword2").focus();
        return;
    }

    var ajaxData = { id: window.localStorage.getItem("storUserId"), oldpas: sourcePassword, newpas: newpassword, newpas2: newpassword2 };

    $.ajax({
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(ajaxData),
        url: "/api/MainApi/EditPassword",
        async: false,
        dataType: "text",
        success: function (result) {
            var res = JSON.parse(result);
            if (res == 3) {
                $("#tdPasswordError").text("原密码不正确");
                $("#inputPassword0").focus();
            }
            else {
                bootAlertCallback("消息", "密码修改成功,请重新登陆", function () {
                    clearAuthAndLocation();
                    top.window.location = "/Login/";
                });
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(JSON.parse(XMLHttpRequest.responseText).ExceptionMessage);
        }
    })
}


function clearAuthAndLocation() {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/MainApi/Logout",
        async: false,
        dataType: "json",
        success: function (result) {

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(JSON.parse(XMLHttpRequest.responseText).ExceptionMessage);
        }
    });
    window.localStorage.clear();
}