
//动态加载css,js
var dynamicLoading = {
    css: function (path) {
        if (!path || path.length === 0) {
            throw new Error('css "path" is required !');
        }
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.href = path;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        head.appendChild(link);
    },
    js: function (path) {
        if (!path || path.length === 0) {
            throw new Error('js "path" is required !');
        }
        var head = document.getElementsByTagName('head')[0];
        var script = document.createElement('script');
        script.src = path;
        script.type = 'text/javascript';
        head.appendChild(script);
    },
    meta: function (equiv, content) {
        if (!equiv || equiv.length == 0 || !content || content.length == 0) {
            throw new Error('equiv ,content is required !');
        }
        var head = document.getElementsByTagName('head')[0];
        var meta = document.createElement('meta');
        meta.httpEquiv = equiv;
        meta.content = content;
        head.appendChild(meta);
    }
}

document.writeln('<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />');
document.writeln('<meta http-equiv="Expires" content="0"/>');
document.writeln('<meta http-equiv="Cache-Control" content="no-cache"/>');
document.writeln('<meta http-equiv="Pragma" content="no-cache"/>');
document.writeln('<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />');
document.writeln('<link href="/Content/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />');
document.writeln('<link href="/Content/bootstrap-select/css/bootstrap-select.min.css" rel="stylesheet" />');
document.writeln('<link href="/Content/bootstrap-select/css/extBootstrapSelct.css" rel="stylesheet" />');
document.writeln('<link href="/Scripts/pagination_zh/lib/pagination.css" rel="stylesheet" />');
document.writeln('<link href="/Content/common.css" rel="stylesheet" />');
document.writeln('<link href="/Scripts/nprogress/nprogress.css" rel="stylesheet" />');

document.writeln('<script src="/Scripts/jquery-1.10.2.min.js"></script>');
document.writeln('<script src="/Scripts/My97DatePicker/WdatePicker.js"></script>');
document.writeln('<script src="/Scripts/pagination_zh/lib/jquery.pagination.js"></script>');
document.writeln('<script src="/Content/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>');
document.writeln('<script src="/Content/bootstrap-select/js/bootstrap-select.min.js"></script>');
document.writeln('<script src="/Content/bootstrap-select/js/SelectExtend.js"></script>');
document.writeln('<script src="/Scripts/json2.js"></script>');
document.writeln('<script src="/Scripts/vue.min.js"></script>');
document.writeln('<script src="/Scripts/cathyJs.js"></script>');
document.writeln('<script src="/Scripts/pageCommend.js"></script>');
document.writeln('<script src="/Scripts/jqueryExtend.js"></script>');
document.writeln('<script src="/Scripts/jqueryExtendAjax.js"></script>');
document.writeln('<script src="/Scripts/nprogress/nprogress.js"></script>');