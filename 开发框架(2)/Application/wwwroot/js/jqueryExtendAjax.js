(function($) {
	//1.得到$.ajax的对象
	var _ajax = $.ajax;
	$.ajax = function(options) {
		//2.每次调用发送ajax请求的时候定义默认的error处理方法
		var fn = {
			error: function(XMLHttpRequest, textStatus, errorThrown) {},
			success: function(data, textStatus) {},
			beforeSend: function(XHR) {},
			complete: function(XHR, TS) {}
		}
		//3.扩展原生的$.ajax方法，返回最新的参数
		var _options = $.extend({}, {
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				fn.error(XMLHttpRequest, textStatus, errorThrown);
			},
			success: function(data, textStatus) {
				fn.success(data, textStatus);
			},
			beforeSend: function(XHR) {
				XHR.setRequestHeader('Authorization', 'BasicAuth ' + window.localStorage.getItem("storticket"));
				fn.beforeSend(XHR);
			},
			complete: function(XHR, TS) {
				fn.complete(XHR, TS);
			}
		}, options);
		//4.将最新的参数传回ajax对象
		    _ajax(_options);
	};
})(jQuery);