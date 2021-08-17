/**********************************************************表单验证**********************************************************/
function ShowErrorMsg(sender) {
    if ($(sender).attr('msg')) {
        var eid = "#errorMsg" + sender.id;
        if ($(eid).length == 0) {
            var error = $("<span id='errorMsg" + sender.id + "' style='color:red' def-tag='error'>" + $(sender).attr('msg') + "</span>");
            $(sender).after(error);
        }
    }
}
function RemoveErrorMsg(sender) {
    if ($(sender).attr('msg')) {
        var eid = "#errorMsg" + sender.id;
        $(eid).remove();
    }
}
function valid(reg, sender) {
    //只适用于Select
    if (reg == "!-1") {
        if ($(sender).val() != "-1" && $(sender).val() != "" && $(sender).val() != null) {

            RemoveErrorMsg(sender);
            return true;
        }
        else {
            ShowErrorMsg(sender);
            return false;
        }
    }
    if (reg != undefined) {
        if (reg.exec($(sender).val())) {
            RemoveErrorMsg(sender);
            return true;
        }
        else {
            ShowErrorMsg(sender);
            return false;
        }
    }
}
//电话
var phone = /^[+]{0,1}(\d){1,4}[ ]?([-]?((\d)|[ ]){1,12})+$/;
//必填
var required = /^(.)+/;
//下拉必填
var selreq = "!-1";
//数字(必填)
var reqfloat = /^[-]?\d+(\.\d+)?$/;
//数字(可为空)
var num = /^[-]?\d*\.{0,1}\d*$/
function ValidateControls(pareControl) {
    var result = true;
    $(pareControl).find('[validate]').each(function () {
        ex = $(this).attr('validate');
        if (ex.indexOf('phone') >= 0) { if (!valid(phone, this)) { result = false; } } //电话验证
        if (ex.indexOf('required') >= 0) { if (!valid(required, this)) { result = false; } } //必填验证
        if (ex.indexOf('selreq') >= 0) { if (!valid(selreq, this)) { result = false; } } //下拉列表必选验证
        if (ex.indexOf('reqfloat') >= 0) { if (!valid(reqfloat, this)) { result = false; } } //小数必填验证
        if (ex.indexOf('num') >= 0) { if (!valid(num, this)) { result = false; } } //数字验证
        if (ex.indexOf('length:') >= 0) {//长度验证
            var t = ex.substr(ex.indexOf('length:') + 7).split(' ')[0];
            var min = t.split(',')[0];
            var max = t.split(',').length > 1 ? t.split(',')[1] : t.split(',')[0];
            exp = new RegExp("^(.|\s|\S|[\n]){" + min + "," + max + "}$");
            if (!valid(exp, this)) { result = false; }
        }
    });
    return result;
}
