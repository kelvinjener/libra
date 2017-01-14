ServicesUtility = {
    isInitialized: false,

    initiliaze: function () {
        if (this.isInitialized) {
            return;
        }
        this.isInitialized = true;
    },

    runWebMethod: function (verb, url, params, fnCallBack) {
        Libra.UI.showLoading();

        $.ajax({
            type: verb,
            url: url,
            dataType: 'json',
            contentType: 'application/json',
            data: params,
            success: function (r) {
                Libra.UI.hideLoading();
                fnCallBack(r.d);
            },
            error: function (error) {
                Libra.UI.hideLoading();
                fnCallBack(error);
            }
        });
    }
}

$(document).ready(function () {
    ServicesUtility.initiliaze();
    window.services = ServicesUtility;
});