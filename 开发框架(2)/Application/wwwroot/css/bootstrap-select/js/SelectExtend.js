$.fn.extend({
    //无默认值时默认选择第一项
    customSelect: function (params) {
        /*在包裹容器中设置  必需设置Begin*/
        //用于生成select ID
        var selectId = $(this).attr("selectId");
        //select value值字段
        var value = $(this).attr("value");
        //select text值字段
        var text = $(this).attr("text");
        //包裹select的容器   一般都为div
        var container = $(this).attr("Id");
        /*在包裹容器中设置  必需设置End*/

        /*必需设置Begin*/
        //将要填充的数据  数据可为json也可为string
        var datalist = params.data;
        if (typeof params.data == "string") {
            datalist = JSON.parse(datalist);
        }
        /*必需设置End*/

        /*可选参数Begin*/
        //宽度
        var width = 150;
        if (params.width) {
            width = params.width;
        }
        //下拉列表显示行数
        var size = 10;
        if (params.size) {
            size = params.size;
        }
        //是否可搜索
        var liveSearch = false;
        if (params.liveSearch) {
            liveSearch = params.liveSearch;
        }
        //默认值 
        var defaultValue = "";
        if (params.defaultValue) {
            defaultValue = params.defaultValue;
        }

        //可多选
        var multiple = false;
        if (params.multiple) {
            multiple = params.multiple;
        }

        //下拉菜单是否允许向上
        var dropupAuto = true;
        if (params.dropupAuto==false) {
            dropupAuto = params.dropupAuto;
        }
        /*可选参数End*/

        var flowObjectHtml = "<select id='" + selectId + "' class='selectpicker'";
        if (multiple) {
            flowObjectHtml += " multiple>";
        }
        else {
            flowObjectHtml += ">";
        }
        $.each(datalist, function (i, item) {
            if (i == 0) {
                if (defaultValue == "") {
                    defaultValue = item[value];
                }
            }
            flowObjectHtml += "<option value='" + item[value] + "'>";
            flowObjectHtml += item[text] + "</option>";
        });
        flowObjectHtml += "</select>";
        $("#" + container).empty();
        $("#" + container).append(flowObjectHtml);

        $("#" + selectId).selectpicker({
            liveSearch: liveSearch,
            width: width,
            size: size,
            dropupAuto:dropupAuto
        });
        $("#" + selectId).selectpicker('val', defaultValue);
        $('#' + selectId).selectpicker('render');
    },
    //默认没有选中项
    secondSelect: function (params) {
        /*在包裹容器中设置  必需设置Begin*/
        //用于生成select ID
        var selectId = $(this).attr("selectId");
        //select value值字段
        var value = $(this).attr("value");
        //select text值字段
        var text = $(this).attr("text");
        //包裹select的容器   一般都为div
        var container = $(this).attr("Id");
        /*在包裹容器中设置  必需设置End*/

        /*必需设置Begin*/
        //将要填充的数据  数据可为json也可为string
        var datalist = params.data;
        if (typeof params.data == "string") {
            datalist = JSON.parse(datalist);
        }
        /*必需设置End*/

        /*可选参数Begin*/
        //宽度
        var width = 150;
        if (params.width) {
            width = params.width;
        }
        //下拉列表显示行数
        var size = 10;
        if (params.size) {
            size = params.size;
        }
        //是否可搜索
        var liveSearch = false;
        if (params.liveSearch) {
            liveSearch = params.liveSearch;
        }
        //默认值 
        var defaultValue = "";
        if (params.defaultValue) {
            defaultValue = params.defaultValue;
        }

        //可多选
        var multiple = false;
        if (params.multiple) {
            multiple = params.multiple;
        }

        //下拉菜单是否允许向上
        var dropupAuto = true;
        if (params.dropupAuto == false) {
            dropupAuto = params.dropupAuto;
        }
        /*可选参数End*/

        var flowObjectHtml = "<select id='" + selectId + "' class='selectpicker'";
        if (multiple) {
            flowObjectHtml += " multiple>";
        }
        else {
            flowObjectHtml += ">";
        }
        $.each(datalist, function (i, item) {
            flowObjectHtml += "<option value='" + item[value] + "'>";
            flowObjectHtml += item[text] + "</option>";
        });
        flowObjectHtml += "</select>";
        $("#" + container).empty();
        $("#" + container).append(flowObjectHtml);

        $("#" + selectId).selectpicker({
            liveSearch: liveSearch,
            width: width,
            size: size,
            title: "请选择.....",
            dropupAuto:dropupAuto
        });
        if (defaultValue != "") {
            $("#" + selectId).selectpicker('val', defaultValue);
            $('#' + selectId).selectpicker('render');
        }
    }

});