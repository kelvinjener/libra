var Libra = {};
Libra.UI = {};

(function (window, document, Libra, $) {
    Libra.queryString = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
        return results == null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    Libra.setCache = function (key, value) {
        window.localStorage[key] = value;
    }

    Libra.getQueryStringParam = function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    Libra.getCache = function (key) {
        return window.localStorage[key];
    }

    Libra.setSessionValue = function (key, value) {
        window.sessionStorage[key] = value;
    }

    Libra.getSessionValue = function (key) {
        return window.sessionStorage[key];
    }

    Libra.tryCatch = function (fnCallback) {
        try {
            fnCallback();
        } catch (e) {
            Libra.UI.hideLoading();
            Libra.UI.showError(e);
        }
    }

    Libra.convertToDouble = function (value) {
        if (value != '' && value != undefined) {
            return parseFloat(value.replace('R$', '').replace('.', '').replace(',', '.'));
        } else {
            return 0;
        }
    }

    Libra.toStringByType = function (type, v) {
        switch (type.toLowerCase()) {
            case 'bool':
                return v ? 'Sim' : 'Não';
            case 'bytes':
                return Libra.formatSizeUnits(v);
            case 'datetime':
                return moment(v).format('DD/MM/YYYY HH:mm');
            case 'date':
                return moment(v).format('DD/MM/YYYY');
            case 'currency':
                return Libra.formatMoney(v);
            case 'double':
                return Libra.formatMoney(v).replace('R$', '').trim();
            default:
                return v;
        }
    }

    Libra.formatSizeUnits = function (bytes) {
        if ((bytes >> 30) & 0x3FF)
            bytes = (bytes >>> 30) + '.' + (bytes & (3 * 0x3FF)) + ' GB';
        else if ((bytes >> 20) & 0x3FF)
            bytes = (bytes >>> 20) + '.' + (bytes & (2 * 0x3FF)) + ' MB';
        else if ((bytes >> 10) & 0x3FF)
            bytes = (bytes >>> 10) + '.' + (bytes & (0x3FF)) + ' KB';
        else if ((bytes >> 1) & 0x3FF)
            bytes = (bytes >>> 1) + ' Bytes';
        else
            bytes = bytes + ' Byte';
        return bytes;
    }

    Libra.formatMoney = function (number, places) {
        var n = number,
        decPlaces = places == undefined ? 2 : places,
        decSeparator = ",",
        thouSeparator = ".",
        sign = n < 0 ? "-" : "",
        i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
        return 'R$' + (sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : ""));
    }

    Libra.formatDouble = function (number, places) {
        return Libra.formatMoney(number, places).replace('R$', '');
    }
})(window, document, Libra, jQuery);


Date.isLeapYear = function (year) {
    return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
};

Date.getDaysInMonth = function (year, month) {
    return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
};

Date.prototype.isLeapYear = function () {
    var y = this.getFullYear();
    return (((y % 4 === 0) && (y % 100 !== 0)) || (y % 400 === 0));
};

Date.prototype.getDaysInMonth = function () {
    return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
};

Date.prototype.addMonths = function (value) {
    var n = this.getDate();
    this.setDate(1);
    this.setMonth(this.getMonth() + value);
    this.setDate(Math.min(n, this.getDaysInMonth()));
    return this;
};

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

String.prototype.capitalize = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
}

String.prototype.toCamelCase = function () {
    var words = this.split('-');
    var newString = new Array();

    newString.push(words[0].toLowerCase());

    for (var i = 1; i < words.length; i++) {
        newString.push(words[i].capitalize());
    }

    return newString.join('');
}

String.prototype.toPascalCase = function () {
    return this.toCamelCase().capitalize();
}

String.prototype.startsWith = function (searchString, position) {
    position = position || 0;
    return this.indexOf(searchString, position) === position;
}

Array.prototype.sum = function (property) {
    var r = 0;

    $.each(this, function (i, v) {
        r += v[property];
    });

    return r;
}

Array.prototype.getFirst = function (property) {
    var tmp = new Array();
    var r = null;

    $.each(this, function (i, v) {
        if (v[property] != null) {
            r = v[property]
            return;
        }
    });

    return r;
}