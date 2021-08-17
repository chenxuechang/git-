function isIE() {
    if (!!window.ActiveXObject || "ActiveXObject" in window)
        return true;
    else
        return false;
}

function OpenPage(url) {
    $('#ifrMain').attr('src', url);
}


//初始化IFrame及右侧Div宽、高度、左侧Div高度
function initPageSize() {
    var windowHeight = $(window).height();
    var leftRightDivHeight = windowHeight - 80;
    $("#divLeft").height(leftRightDivHeight);
    $("#divRight").height(leftRightDivHeight);
    if (isIE()) {
        $("#divRight").width($(window).width() - 221);
    }
    else {
        $("#divRight").width($(window).width() - 222);
    }
    $("#ifrMain").height(leftRightDivHeight);
}

$(function () {
    initPageSize();
    axios.post("/api/MainApi/MainPageLoadMenu",
        {
            userId: window.localStorage.getItem("storUserId") + ""
        },
        {
            headers: {
                Authorization: window.localStorage.getItem("storToken")
            }
        }
    ).then(response => {
        var res = response.data;
        if (res.resultInfo.isSuccess == 1) {
            $(".leftMenu").html(res.menuInfo);
        }
        else {
            this.$message({
                duration: 1000,
                type: 'info',
                message: res.resultInfo.errorInfo
            });
        }
    }).catch(function (error) {
        // 请求失败处理
        console.log(error);
    });

});

/*左侧菜单栏特效Begin*/
(function ($) {
    $(window).load(function () {
        //滚动条
        $(".left").mCustomScrollbar();
    });
})(jQuery);

//显示/隐藏功能菜单  单个点开、隐藏
function showHideFuncMenuSingle(object) {
    var paraList = $(object).attr("def-list");
    var childSub = $("[def-menu='" + paraList + "']");
    var rightIcon = $("[def-righticon='" + paraList + "']");
    if (childSub.css("display") == "none") {
        childSub.css("display", "block");
        rightIcon.removeClass("glyphicon-menu-right");
        rightIcon.addClass("glyphicon-menu-down");
    }
    else {
        childSub.css("display", "none");
        rightIcon.addClass("glyphicon-menu-right");
        rightIcon.removeClass("glyphicon-menu-down");
    }
}

//显示/隐藏功能菜单  只开一个、其它隐藏
function showHideFuncMenu(object) {
    //获取所有包含def-menu的UL
    $("[def-menu]").css("display", "none");
    $("[def-righticon]").removeClass("glyphicon-menu-down");
    $("[def-righticon]").addClass("glyphicon-menu-right");

    var paraList = $(object).attr("def-list");
    var childSub = $("[def-menu='" + paraList + "']");
    var rightIcon = $("[def-righticon='" + paraList + "']");
    if (childSub.css("display") == "none") {
        childSub.css("display", "block");
        rightIcon.removeClass("glyphicon-menu-right");
        rightIcon.addClass("glyphicon-menu-down");
    }
}


function openUrl(object) {
    $("[def-list]").removeClass("menuSelected");
    $("[def-sub]").removeClass("menuSelected");

    var menuIndex = $(object).attr("def-sub");
    $("[def-list='" + menuIndex + "']").addClass("menuSelected");
    $(object).addClass("menuSelected");

    var target = $(object).attr("def-target");
    if (target == "_blank") {
        window.open($(object).attr("def-url"));
    }
    else {
        $("#ifrMain").attr("src", $(object).attr("def-url"));
    }
}
/*左侧菜单栏特效End*/

window.onresize = function () {
    initPageSize();
};



//window.onunload = function () {
//    clearAuthAndLocation();
//}




function clearAuthAndLocation() {
    window.localStorage.clear();
}

const App = {
    data() {
        var validatePass = (rule, value, callback) => {
            if (value === '') {
                callback(new Error('请输入密码'));
            } else {
                callback();
            }
        };
        var validatePass2 = (rule, value, callback) => {
            if (value === '' || value == undefined) {
                callback(new Error('请输入新密码确认'));
            } else if (value !== this.passInfo.newPass) {
                callback(new Error('两次输入密码不一致'));
            } else {
                callback();
            }
        };
        return {
            userName: window.localStorage.getItem("storUser"),
            dialogFormVisible: false,
            passInfo: { oldPass: "", newPass: "", newConfirmPass: "" },

            rules: {
                oldPass: [
                    { required: true, message: '请输入老密码', trigger: 'blur' },

                ],
                newPass: [
                    { required: true, message: '请输入新密码', trigger: 'blur' },
                    { validator: validatePass, trigger: 'blur' }
                ],
                newConfirmPass: [
                    { validator: validatePass2, trigger: 'blur' },
                    { validator: validatePass2, trigger: 'blur', required: true }
                ]
            }
        };
    },
    methods: {
        //退出系统
        backSystem: function () {
            this.$confirm('确实要退出该系统吗？', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                cancelButtonClass: 'btn-custom-cancel',
                type: 'warning'
            }).then(() => {
                clearAuthAndLocation();
                top.window.location = "/Login";
            }).catch(() => {
                this.$message({
                    duration: 1000,
                    type: 'info',
                    message: '已取消退出'
                });
            });
        },
        //修改密码
        editPasswordEvent: function (formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    axios.post("/api/MainApi/EditPassword",
                        {
                            userId: window.localStorage.getItem("storUserId") + "",
                            oldPass: this.passInfo.oldPass,
                            newPass: this.passInfo.newPass
                        },
                        {
                            headers: {
                                Authorization: window.localStorage.getItem("storToken")
                            }
                        }
                    ).then(response => {
                        var res = response.data;
                        if (res.resultInfo.isSuccess == 1) {
                            this.$alert('密码修改成功', '消息', {
                                confirmButtonText: '确定',
                                callback: action => {
                                    top.window.location = "/Login/";
                                }
                            });
                        }
                        else {
                            this.$message({
                                duration: 1000,
                                type: 'info',
                                message: res.resultInfo.errorInfo
                            });
                        }
                    }).catch(function (error) {
                        // 请求失败处理
                        console.log(error);
                    });
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        //修改密码框打开
        editPassword: function () {
            this.dialogFormVisible = true;
            this.passInfo.oldPass = "";
            this.passInfo.newPass = "";
            this.passInfo.newConfirmPass = "";
            //重置验证信息
            this.$refs['passInfo'].resetFields();
        }
    },
    created: function () {

    }
};

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");