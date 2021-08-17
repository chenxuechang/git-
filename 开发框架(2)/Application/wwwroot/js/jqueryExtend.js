(function ($) {
    $.getUrlParam = function (m) {
        var sValue = location.search.match(new RegExp("[\?\&]" + m + "=([^\&]*)(\&?)", "i"));
        return sValue ? sValue[1] : sValue;
    },
		$.format_number = function (n) {
		    var b = parseInt(n).toString();
		    var len = b.length;
		    //小数位
		    var pointNum = "";
		    if (n.length != b.length) {
		        pointNum = n.substr(len);
		    }
		    if (len <= 3) {
		        return b;
		    }
		    var r = len % 3;
		    var intWei = r > 0 ? b.slice(0, r) + "," + b.slice(r, len).match(/\d{3}/g).join(",") : b.slice(r, len).match(/\d{3}/g).join(",");
		    var result = intWei + pointNum;
		    return result;
		}
})(jQuery);

function getCookie(cookie_name) {
    var allcookies = document.cookie;
    var cookie_pos = allcookies.indexOf(cookie_name); //索引的长度

    // 如果找到了索引，就代表cookie存在，
    // 反之，就说明不存在。
    if (cookie_pos != -1) {
        // 把cookie_pos放在值的开始，只要给值加1即可。
        cookie_pos += cookie_name.length + 1; //这里容易出问题，所以请大家参考的时候自己好好研究一下
        var cookie_end = allcookies.indexOf(";", cookie_pos);

        if (cookie_end == -1) {
            cookie_end = allcookies.length;
        }

        var value = unescape(allcookies.substring(cookie_pos, cookie_end)); //这里就可以得到你想要的cookie的值了。。。
    }
    return value;
}
