var code; //在全局定义验证码   
function createCode() {
    code = "";
    var codeLength = 4;//验证码的长度  
    var checkCode = document.getElementById("code");
    var random = new Array(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);//随机数  
    for (var i = 0; i < codeLength; i++) {//循环操作  
        var index = Math.floor(Math.random() * 10);//取得随机数的索引（0~35）  
        code += random[index];//根据索引取得随机数加到code上  
    }
    checkCode.value = code;//把code值赋给验证码  
}

$(function () {
    createCode();
});

function userClear() {
    $("#inputUserName").val("");
    $("#inputPassWord").val("");
    $("#inputValidateCode").val("");
}

//用户登录
function userLogin() {
    $("#tdErrInfo").text("");
    if ($("#inputUserName").val() + "" == "") {
        $("#tdErrInfo").text("用户名不允许为空");
        createCode();
        return;
    }
    if ($("#inputPassWord").val() + "" == "") {
        $("#tdErrInfo").text("密码不允许为空");
        createCode();
        return;
    }

    if ($("#inputValidateCode").val() != code) {
        $("#tdErrInfo").text("验证码错误");
        createCode();
        return;
    }

    var encrypt = new JSEncrypt();
    encrypt.setPublicKey(publicKey);
    var strUserName = encrypt.encrypt($("#inputUserName").val());
    strUserName = encodeURI(strUserName).replace(/\+/g, '%2B');

    var strUserPass = encrypt.encrypt($("#inputPassWord").val());
    strUserPass = encodeURI(strUserPass).replace(/\+/g, '%2B');

    var strUserValidateCode = encrypt.encrypt($("#inputValidateCode").val());
    strUserValidateCode = encodeURI(strUserValidateCode).replace(/\+/g, '%2B');

    axios.post("/api/LoginApi/UserLoginEncrypt",
        {
            userName: strUserName,
            userPass: strUserPass,
            validateCode: strUserValidateCode
        }
    ).then(response => {
        var res = response.data;
        if (res.resultInfo.isSuccess == 1) {
            window.localStorage.setItem("storUserId", res.userId);
            window.localStorage.setItem("storAccount", res.userAccount);
            window.localStorage.setItem("storUser", res.userName);
            window.localStorage.setItem("storRoleId", res.userRoleIds);
            window.localStorage.setItem("storToken", res.userToken);
            window.location.href = "/Main";
        }
        else {
            $("#tdErrInfo").text(res.resultInfo.errorInfo);
             createCode();
        }
    }).catch(function (error) {
         // 请求失败处理
        console.log(error);
    });
}