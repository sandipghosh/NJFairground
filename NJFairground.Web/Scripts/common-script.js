/// <reference path="jquery-2.1.0.min.js" />
/// <reference path="jquery-ui-vsdoc.js" />

/*String utility prototype starts*/
String.prototype.startsWith = function (str) {
    return this.slice(0, str.length) == str;
};

String.prototype.endsWith = function (str) {
    return this.slice(-str.length) == str;
};

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/, '');
};

String.prototype.contains = function (it) {
    return this.indexOf(it) != -1;
};

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{\{|\}\}|\{(\d+)\}/g, function (m, n) {
        if (m == "{{") { return "{"; }
        if (m == "}}") { return "}"; }
        return args[n];
    });
};

String.prototype.isEmptyOrBlank = function () {
    return (!this || 0 === this.length) || (!this || /^\s*$/.test(this));
};

String.prototype.replaceAll = function (token, newToken, ignoreCase) {
    var str, i = -1, _token;
    if ((str = this.toString()) && typeof token === "string") {
        _token = ignoreCase === true ? token.toLowerCase() : undefined;
        while ((i = (
            _token !== undefined ?
                str.toLowerCase().indexOf(
                    _token,
                    i >= 0 ? i + newToken.length : 0
                ) : str.indexOf(
                    token,
                    i >= 0 ? i + newToken.length : 0
                )
        )) !== -1) {
            str = str.substring(0, i)
                .concat(newToken)
                .concat(str.substring(i + token.length));
        }
    }
    return str;
};

String.prototype.hasQueryString = function () {
    return (this.indexOf('?') > 0);
};

isNumber = function (s) {
    var n = s.toString().trim();
    return !isNaN(parseFloat(n)) && isFinite(n);
};

isInteger = function (s) {
    var n = s.trim();
    return n.length > 0 && !(/[^0-9]/).test(n);
};

isHTML = function (str) {
    return !!$(str)[0];
};

isFloat = function (s) {
    var n = s.trim();
    return n.length > 0 && !(/[^0-9.]/).test(n) && (/\.\d/).test(n);
}

isDate = function (dt) {
    return (null != dt) && !isNaN(dt) && ("undefined" !== typeof dt.getDate);
};

rgb2hex = function (rgb) {
    rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
    return "#" +
     ("0" + parseInt(rgb[1], 10).toString(16)).slice(-2) +
     ("0" + parseInt(rgb[2], 10).toString(16)).slice(-2) +
     ("0" + parseInt(rgb[3], 10).toString(16)).slice(-2);
};

roundNumber = function (num, dec) {
    var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
    return result;
};

//Implement function for Base64 encription
Base64Encode = function (input) {
    try {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        input = escape(input);
        var output = "";
        var chr1, chr2, chr3 = "";
        var enc1, enc2, enc3, enc4 = "";
        var i = 0;

        do {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
               keyStr.charAt(enc1) +
               keyStr.charAt(enc2) +
               keyStr.charAt(enc3) +
               keyStr.charAt(enc4);
            chr1 = chr2 = chr3 = "";
            enc1 = enc2 = enc3 = enc4 = "";
        } while (i < input.length);

        return output;

    }
    catch (ex) { }
};

//Implement function for Base64 decription
Base64Decode = function (encoded) {
    try {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        do {
            enc1 = keyStr.indexOf(encoded.charAt(i++));
            enc2 = keyStr.indexOf(encoded.charAt(i++));
            enc3 = keyStr.indexOf(encoded.charAt(i++));
            enc4 = keyStr.indexOf(encoded.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        } while (i < encoded.length);

        return output;

    }
    catch (ex) { }
};

GetLetterCaseFromPascalCase = function (str) {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2');
};




