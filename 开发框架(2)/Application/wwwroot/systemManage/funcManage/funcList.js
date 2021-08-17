$(function () {
    $("#title").text(decodeURI($.getUrlParam("title")));

    tableList.getMenuList(pid);
});

var tableList = new Vue({
    el: '#divVue',
    data: { listMenuInfo: "" },
    methods: {
        getMenuList: function (pid) {
            var vm = this;//此句很重要
            $.ajax({
                type: "POST",
                //contentType: "application/json",
                //data:JSON.stringify({ pid: pid }),
                data: { menuId: pid },
                contentType: "application/json",
                url: "/api/SystemManage/FuncManageApi/ListMenuInfo",
                async: false,
                dataType: "json",
                success: function (result) {
                    vm.listMenuInfo = result;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(JSON.parse(XMLHttpRequest.responseText).ExceptionMessage);
                }
            });
        },
        addEdit: function (id) {
            window.parent.frames.addEdit(id, $.getUrlParam("pid"));
        },
        menuIcon: function (className) {
            return '<span class="' + className + '"></span>';
        },
        menuType: function (typeId) {
            if (typeId + "" == "1") {
                return "目录";
            }
            else {
                return "功能";
            }
        },
        del: function (id) {
            var pid = $.getUrlParam("pid");
            var cc = id + "$" + pid;
            window.parent.frames.bootConfirm("警告", "确实要删除该条记录吗？", cc, delFunc);
        }
    }
});

function delFunc(cc) {
    var ccArr = cc.split("$");
    var id = ccArr[0];
    var pid = ccArr[1];
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/SystemManage/FuncManageApi/MenuDelete",
        data: JSON.stringify({ id: id }),
        dataType: "text",
        success: function (result) {
            if (result == "1") {
                window.frames["ifrMenu"].tableList.getMenuList(pid);
                LoadTree();
                bootAlert("提示", "删除成功");
            }
            else {
                bootAlert("提示", result);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(JSON.parse(XMLHttpRequest.responseText).ExceptionMessage);
        }
    });
}

