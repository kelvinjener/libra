(function (window, document, Libra, $) {
    Libra.UI = {
        isInitialized: false,

        initiliaze: function () {
            if (this.isInitialized) {
                return;
            }

            this.isInitialized = true;

            this.configureUIDefaultComponents();
        },

        bindAutocomplete: function () {
            var currentObject = this;

            $('.dynamic-autocomplete').each(function () {
                currentObject.bindComponentAutocomplete($(this));
            });
        },

        bindComponentAutocomplete: function (o) {
            o.keypress(function () {
                o.attr('autocomplete-is-valid', false);
            });

            o.typeahead({
                source: function (query, process) {
                    return $.getJSON(
                    o.attr('autocomplete-url') + '?search=' + encodeURIComponent(query),
                    function (data) {
                        return process(data);
                    });
                },
                updater: function (item) {
                    o.attr('access-key-control-enabled', true);
                    o.attr('autocomplete-is-valid', true);
                    o.attr('autocomplete-valid-value', item);
                    return item;
                }
            });
        },

        disableButton: function (buttonId, waitText) {
            $(buttonId).attr("disabled", "disabled");
            $(buttonId).attr('original-val', $(buttonId).val());
            $(buttonId).val(waitText == undefined ? 'Aguarde...' : waitText);
        },

        enableButton: function (buttonId, waitText) {
            $(buttonId).removeAttr("disabled");
            $(buttonId).val($(buttonId).attr('original-val'));
        },

        showAlert: function (message) {
            bootbox.alert(message);
        },

        getDisplayErrors: function (errors, headerText) {
            var header = headerText == undefined ? 'Os seguintes errors foram encontrados:' : headerText;
            var message = '<span class="label label-danger">Erro!</span> ' + header;

            if (errors.length == 1) {
                return this.getDisplayError(errors[0]);
            } else {
                message += '<br /><br /><p><ul style="padding-left:15px;">';

                for (var i = 0; i < errors.length; i++) {
                    message += '<li>' + errors[i] + '</li>';
                }

                message += '</ul></p>';

                return message;
            }
        },

        showErrorsWithDetails: function (message, details) {
            if (details != null && details != '') {
                message += '<br /><br />Detalhes: <i>' + details + '</i>';
            }

            this.showError(message);
        },

        showErrors: function (errors, headerText) {
            var header = headerText == undefined ? 'Os seguintes erros foram encontrados:' : headerText;
            var message = '<span class="label label-danger">Erro!</span> ' + header;

            if (errors.length == 1) {
                this.showError(errors[0]);
            } else {
                message += '<br /><br /><p><ul style="padding-left:15px;">';

                for (var i = 0; i < errors.length; i++) {
                    message += '<li>' + errors[i] + '</li>';
                }

                message += '</ul></p>';

                bootbox.alert(message);
            }
        },

        showWarnings: function (errors, headerText) {
            var header = headerText == undefined ? 'As seguintes inconsistências foram encontradas:' : headerText;
            var message = '<span class="label label-warning">Aviso!</span> ' + header;

            if (errors.length == 1) {
                this.showWarning(errors[0]);
            } else {
                message += '<br /><br /><p><ul style="padding-left:15px;">';

                for (var i = 0; i < errors.length; i++) {
                    message += '<li>' + errors[i] + '</li>';
                }

                message += '</ul></p>';

                bootbox.alert(message);
            }
        },

        showSuccessess: function (successess, headerText, fnCallback) {
            var header = headerText == undefined ? '' : headerText;
            var message = '<span class="label label-success">Perfeito!</span>' + header;

            if (successess.length == 1) {
                this.showSuccess(successess[0], fnCallback);
            } else {
                message += '<br /><br /><p><ul style="padding-left:15px;">';

                for (var i = 0; i < successess.length; i++) {
                    message += '<li>' + successess[i] + '</li>';
                }

                message += '</ul></p>';

                bootbox.alert(message, fnCallback);
            }
        },

        getDisplayError: function (message) {
            return '<span class="label label-danger">Erro!</span> ' + message;
        },

        showError: function (message, fnCallback) {
            bootbox.alert('<span class="label label-danger">Erro!</span> ' + message + '', fnCallback);
        },

        showInfo: function (message) {
            bootbox.alert('<span class="col-md-12 notification blue-bg" style="padding: 5px"><strong>Ok!</strong></span>  ' + message);
        },

        showWarning: function (message, fnCallback) {
            if (fnCallback != null) {
                bootbox.alert('<span class="label label-warning">Aviso!</span> ' + message, fnCallback);
            } else {
                bootbox.alert('<span class="label label-warning">Aviso!</span> ' + message);
            }
        },

        showSuccess: function (message, fnCallback) {
            if (fnCallback != null) {
                bootbox.alert('<span class="label label-success">Perfeito!</span> ' + message, fnCallback);
            } else {
                bootbox.alert('<span class="label label-success">Perfeito!</span> ' + message);
            }
        },

        showQuestion: function (message, fnCallback) {
            bootbox.confirm('<span class="label label-warning">Atenção</span>  ' + message, fnCallback);
        },

        onErrorCallback: function (r) {
            Libra.UI.hideLoading();
            Libra.UI.showError(r.get_message());
        },

        onSuccessCallback: function (r, fnOnSucess, hideLoading) {
            if (fnOnSucess == null || hideLoading == undefined || (hideLoading != undefined && hideLoading == true)) {
                Libra.UI.hideLoading();
            }

            if (r.IsError) {
                if (r.Errors != undefined && r.Errors.length > 0) {
                    Libra.UI.showErrors(r.Errors);
                } else {
                    Libra.UI.showError(r.Message);
                }
            } else {
                if (fnOnSucess != null) {
                    fnOnSucess();
                } else {
                    Libra.UI.showSuccess(r.Message);
                }
            }
        },

        showLoading: function () {
            $('#OverlayLoading').show();
        },

        hideLoading: function () {
            $('#OverlayLoading').hide();
        },

        notifySuccess: function (message) {
            $.notify({
                icon: 'fa fa-thumbs-o-up',
                title: '<b>Ok!</b><br>',
                message: message
            }, {
                type: 'success'
            });
        },

        notifyError: function (message) {
            $.notify({
                icon: 'fa fa-thumbs-o-down',
                title: '<b>Erro!</b><br>',
                message: message
            }, {
                type: 'danger'
            });
        },

        notifyWarning: function (message) {
            $.notify({
                icon: 'fa fa-exclamation-triangle',
                title: '<b>Atenção!</b><br>',
                message: message
            }, {
                type: 'warning'
            });
        },

        openPageAsModal: function (url, title, height) {
            $('#DefaultModalTitle').html(title);
            $('#DefaultModalUrl').attr('src', url);

            if (height == undefined) {
                $('#DefaultModalUrl').attr('height', '50%');
            } else {
                $('#DefaultModalUrl').attr('height', height);
            }

            $('#DefaultModal').modal('show');
        },

        configureUIDefaultComponents: function () {
        },

        tryCatch: function (f) {
            try {
                Libra.UI.showLoading();
                f();
            } catch (e) {
                Libra.UI.hideLoading();
            }
        }
    }

    $(document).ready(function () {
        Libra.UI.initiliaze();
    });
})(window, document, Libra, jQuery);
