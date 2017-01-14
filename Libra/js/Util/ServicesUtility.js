ServicesUtility = {
    isInitialized: false,

    initiliaze: function () {
        if (this.isInitialized) {
            return;
        }
        this.isInitialized = true;
    },

    runWebMethod: function (verb, url, params, fnCallBack) {
        $.ajax({
            type: verb,
            url: url,
            dataType: 'json',
            contentType: 'application/json',
            data: params,
            success: function (r) {
                fnCallBack(r.d);
            },
            error: function (error) {
                fnCallBack(error);
            }
        });
    }
}

$(document).ready(function () {
    ServicesUtility.initiliaze();
    window.services = ServicesUtility;
});