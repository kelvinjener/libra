/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/** ******  left menu  *********************** **/
$(function () {
    $('#sidebar-menu li ul').slideUp();
    $('#sidebar-menu li').removeClass('active');

    $('#sidebar-menu li').on('click touchstart', function () {
        var link = $('a', this).attr('href');

        if (link) {
            window.location.href = link;
        } else {
            if ($(this).is('.active')) {
                $(this).removeClass('active');
                $('ul', this).slideUp();
            } else {
                $('#sidebar-menu li').removeClass('active');
                $('#sidebar-menu li ul').slideUp();

                $(this).addClass('active');
                $('ul', this).slideDown();
            }
        }
    });

    $('#menu_toggle').click(function () {
        if ($('body').hasClass('nav-md')) {
            $('body').removeClass('nav-md').addClass('nav-sm');
            $('.left_col').removeClass('scroll-view').removeAttr('style');
            $('.sidebar-footer').hide();

            if ($('#sidebar-menu li').hasClass('active')) {
                $('#sidebar-menu li.active').addClass('active-sm').removeClass('active');
            }
        } else {
            $('body').removeClass('nav-sm').addClass('nav-md');
            $('.sidebar-footer').show();

            if ($('#sidebar-menu li').hasClass('active-sm')) {
                $('#sidebar-menu li.active-sm').addClass('active').removeClass('active-sm');
            }
        }
    });
});

/* Sidebar Menu active class */
$(function () {
    var url = window.location;
    $('#sidebar-menu a[href="' + url + '"]').parent('li').addClass('current-page');
    $('#sidebar-menu a').filter(function () {
        return this.href == url;
    }).parent('li').addClass('current-page').parent('ul').slideDown().parent().addClass('active');
});

/** ******  /left menu  *********************** **/
/** ******  right_col height flexible  *********************** **/
$(".right_col").css("min-height", $(window).height());
$(window).resize(function () {
    $(".right_col").css("min-height", $(window).height());
});
/** ******  /right_col height flexible  *********************** **/



///** ******  tooltip  *********************** **/
//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()
//})
/** ******  /tooltip  *********************** **/
/** ******  progressbar  *********************** **/
//if ($(".progress .progress-bar")[0]) {
//    $('.progress .progress-bar').progressbar(); // bootstrap 3
//}
/** ******  /progressbar  *********************** **/
/** ******  switchery  *********************** **/
if ($(".js-switch")[0]) {
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    elems.forEach(function (html) {
        var switchery = new Switchery(html, {
            color: '#26B99A'
        });
    });
}
/** ******  /switcher  *********************** **/
/** ******  collapse panel  *********************** **/
// Close ibox function
$('.close-link').click(function () {
    var content = $(this).closest('div.x_panel');
    content.remove();
});

// Collapse ibox function
$('.collapse-link').click(function () {
    var x_panel = $(this).closest('div.x_panel');
    var button = $(this).find('i');
    var content = x_panel.find('div.x_content');
    content.slideToggle(200);
    (x_panel.hasClass('fixed_height_390') ? x_panel.toggleClass('').toggleClass('fixed_height_390') : '');
    (x_panel.hasClass('fixed_height_320') ? x_panel.toggleClass('').toggleClass('fixed_height_320') : '');
    button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
    setTimeout(function () {
        x_panel.resize();
    }, 50);
});
/** ******  /collapse panel  *********************** **/
/** ******  iswitch  *********************** **/
if ($("input.flat")[0]) {
    $(document).ready(function () {
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });
    });
}
/** ******  /iswitch  *********************** **/
/** ******  star rating  *********************** **/
// Starrr plugin (https://github.com/dobtco/starrr)
//var __slice = [].slice;

//(function ($, window) {
//    var Starrr;

//    Starrr = (function () {
//        Starrr.prototype.defaults = {
//            rating: void 0,
//            numStars: 5,
//            change: function (e, value) {
//            }
//        };

//        function Starrr($el, options) {
//            var i, _, _ref,
//                    _this = this;

//            this.options = $.extend({}, this.defaults, options);
//            this.$el = $el;
//            _ref = this.defaults;
//            for (i in _ref) {
//                _ = _ref[i];
//                if (this.$el.data(i) != null) {
//                    this.options[i] = this.$el.data(i);
//                }
//            }
//            this.createStars();
//            this.syncRating();
//            this.$el.on('mouseover.starrr', 'span', function (e) {
//                return _this.syncRating(_this.$el.find('span').index(e.currentTarget) + 1);
//            });
//            this.$el.on('mouseout.starrr', function () {
//                return _this.syncRating();
//            });
//            this.$el.on('click.starrr', 'span', function (e) {
//                return _this.setRating(_this.$el.find('span').index(e.currentTarget) + 1);
//            });
//            this.$el.on('starrr:change', this.options.change);
//        }

//        Starrr.prototype.createStars = function () {
//            var _i, _ref, _results;

//            _results = [];
//            for (_i = 1, _ref = this.options.numStars; 1 <= _ref ? _i <= _ref : _i >= _ref; 1 <= _ref ? _i++ : _i--) {
//                _results.push(this.$el.append("<span class='glyphicon .glyphicon-star-empty'></span>"));
//            }
//            return _results;
//        };

//        Starrr.prototype.setRating = function (rating) {
//            if (this.options.rating === rating) {
//                rating = void 0;
//            }
//            this.options.rating = rating;
//            this.syncRating();
//            return this.$el.trigger('starrr:change', rating);
//        };

//        Starrr.prototype.syncRating = function (rating) {
//            var i, _i, _j, _ref;

//            rating || (rating = this.options.rating);
//            if (rating) {
//                for (i = _i = 0, _ref = rating - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; i = 0 <= _ref ? ++_i : --_i) {
//                    this.$el.find('span').eq(i).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
//                }
//            }
//            if (rating && rating < 5) {
//                for (i = _j = rating; rating <= 4 ? _j <= 4 : _j >= 4; i = rating <= 4 ? ++_j : --_j) {
//                    this.$el.find('span').eq(i).removeClass('glyphicon-star').addClass('glyphicon-star-empty');
//                }
//            }
//            if (!rating) {
//                return this.$el.find('span').removeClass('glyphicon-star').addClass('glyphicon-star-empty');
//            }
//        };

//        return Starrr;

//    })();
//    return $.fn.extend({
//        starrr: function () {
//            var args, option;

//            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
//            return this.each(function () {
//                var data;

//                data = $(this).data('star-rating');
//                if (!data) {
//                    $(this).data('star-rating', (data = new Starrr($(this), option)));
//                }
//                if (typeof option === 'string') {
//                    return data[option].apply(data, args);
//                }
//            });
//        }
//    });
//})(window.jQuery, window);

//$(function () {
//    return $(".starrr").starrr();
//});

//$(document).ready(function () {

//    $('#stars').on('starrr:change', function (e, value) {
//        $('#count').html(value);
//    });


//    $('#stars-existing').on('starrr:change', function (e, value) {
//        $('#count-existing').html(value);
//    });

//});
/** ******  /star rating  *********************** **/
/** ******  table  *********************** **/
$('table input').on('ifChecked', function () {
    check_state = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('table input').on('ifUnchecked', function () {
    check_state = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});

var check_state = '';
$('.bulk_action input').on('ifChecked', function () {
    check_state = '';
    $(this).parent().parent().parent().addClass('selected');
    countChecked();
});
$('.bulk_action input').on('ifUnchecked', function () {
    check_state = '';
    $(this).parent().parent().parent().removeClass('selected');
    countChecked();
});
$('.bulk_action input#check-all').on('ifChecked', function () {
    check_state = 'check_all';
    countChecked();
});
$('.bulk_action input#check-all').on('ifUnchecked', function () {
    check_state = 'uncheck_all';
    countChecked();
});

function countChecked() {
    if (check_state == 'check_all') {
        $(".bulk_action input[name='table_records']").iCheck('check');
    }
    if (check_state == 'uncheck_all') {
        $(".bulk_action input[name='table_records']").iCheck('uncheck');
    }
    var n = $(".bulk_action input[name='table_records']:checked").length;
    if (n > 0) {
        $('.column-title').hide();
        $('.bulk-actions').show();
        $('.action-cnt').html(n + ' Records Selected');
    } else {
        $('.column-title').show();
        $('.bulk-actions').hide();
    }
}
/** ******  /table  *********************** **/
/** ******    *********************** **/
/** ******    *********************** **/
/** ******    *********************** **/
/** ******    *********************** **/
/** ******    *********************** **/
/** ******    *********************** **/
/** ******  Accordion  *********************** **/

$(function () {
    $(".expand").on("click", function () {
        $(this).next().slideToggle(200);
        $expand = $(this).find(">:first-child");

        if ($expand.text() == "+") {
            $expand.text("-");
        } else {
            $expand.text("+");
        }
    });
});

/** ******  Accordion  *********************** **/

/** ******  scrollview  *********************** **/
$(document).ready(function () {

    $(".scroll-view").niceScroll({
        touchbehavior: true,
        cursorcolor: "rgba(42, 63, 84, 0.35)"
    });

});
/** ******  /scrollview  *********************** **/

/** ******  NProgress  *********************** **/
//if (typeof NProgress != 'undefined') {
//    $(document).ready(function () {
//        NProgress.start();
//    });

//    $(window).load(function () {
//        NProgress.done();
//    });
//}
/** ******  NProgress  *********************** **/

/** ******  PNotify  *********************** **/

//Regular

function NotifySucess(title, message) {
    new PNotify({
        title: title,
        text: message,
        type: 'success',
        animate: {
            animate: true,
            in_class: 'fadeInRight',
            out_class: 'bounceOut'
        }
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}


function NotifyInfo(title, message) {
    new PNotify({
        title: title,
        text: message,
        type: 'info',
        animate: {
            animate: true,
            in_class: 'fadeInRight',
            out_class: 'bounceOut'
        }
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function NotifyRegular(title, message) {
    new PNotify({
        title: title,
        text: message,
        animate: {
            animate: true,
            in_class: 'fadeInRight',
            out_class: 'bounceOut'
        }
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function NotifyError(title, message) {
    new PNotify({
        title: title,
        text: message,
        type: 'error',
        animate: {
            animate: true,
            in_class: 'fadeInRight',
            out_class: 'bounceOut'
        }
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function NotifyDark(title, message) {
    new PNotify({
        title: title,
        text: message,
        type: 'dark',
        animate: {
            animate: true,
            in_class: 'fadeInRight',
            out_class: 'bounceOut'
        }
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

//Tabbed

$(function () {
    var cnt = 10; //$("#custom_notifications ul.notifications li").length + 1;
    TabbedNotification = function (options) {
        var message = "<div id='ntf" + cnt + "' class='text alert-" + options.type + "' style='display:none'><h2><i class='fa fa-bell'></i> " + options.title +
          "</h2><div class='close'><a href='javascript:;' class='notification_close'><i class='fa fa-close'></i></a></div><p>" + options.text + "</p></div>";

        if (document.getElementById('custom_notifications') == null) {
            alert('doesnt exists');
        } else {
            $('#custom_notifications ul.notifications').append("<li><a id='ntlink" + cnt + "' class='alert-" + options.type + "' href='#ntf" + cnt + "'><i class='fa fa-bell animated shake'></i></a></li>");
            $('#custom_notifications #notif-group').append(message);
            cnt++;
            CustomTabs(options);
        }
    }

    CustomTabs = function (options) {
        $('.tabbed_notifications > div').hide();
        $('.tabbed_notifications > div:first-of-type').show();
        $('#custom_notifications').removeClass('dsp_none');
        $('.notifications a').click(function (e) {
            e.preventDefault();
            var $this = $(this),
              tabbed_notifications = '#' + $this.parents('.notifications').data('tabbed_notifications'),
              others = $this.closest('li').siblings().children('a'),
              target = $this.attr('href');
            others.removeClass('active');
            $this.addClass('active');
            $(tabbed_notifications).children('div').hide();
            $(target).show();
        });
    }

    CustomTabs();

    var tabid = idname = '';
    $(document).on('click', '.notification_close', function (e) {
        idname = $(this).parent().parent().attr("id");
        tabid = idname.substr(-2);
        $('#ntf' + tabid).remove();
        $('#ntlink' + tabid).parent().remove();
        $('.notifications a').first().addClass('active');
        $('#notif-group div').first().css('display', 'block');
    });
})

function TabbedNotifySucess(title, message) {
    new TabbedNotification({
        title: title,
        text: message,
        type: 'success',
        sound: false
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}


function TabbedNotifyInfo(title, message) {
    new TabbedNotification({
        title: title,
        text: message,
        type: 'info',
        sound: false
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function TabbedNotifyRegular(title, message) {
    new TabbedNotification({
        title: title,
        text: message,
        type: 'warning',
        sound: false
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function TabbedNotifyError(title, message) {
    new TabbedNotification({
        title: title,
        text: message,
        type: 'error',
        sound: false
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}

function TabbedNotifyDark(title, message) {
    new TabbedNotification({
        title: title,
        text: message,
        type: 'dark',
        sound: false
        //delay: 2000 /**Tempo de execução em Milisegundos**/
    });
}


/** ******  PNotify  *********************** **/

function atualizarCaracteresRestantes(textBox, label, limite) {
    var restantes = limite - textBox.value.length;

    if (restantes <= 0) {
        textBox.value = textBox.value.substr(0, limite);

        restantes = 0;
    }

    label.innerHTML = restantes;
}

function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e){
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    key = String.fromCharCode(whichCode); // Valor para o código da Chave
    if (strCheck.indexOf(key) == -1) return false; // Chave inválida
    len = objTextBox.value.length;
    for(i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for(; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i))!=-1) aux += objTextBox.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0'+ SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0'+ SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
        objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }
    return false;
}

function formatarTelefone(parEvent, parTextBox) {
    if (maskKeyPress(parEvent))
        mascaraTelefone(parTextBox);
    else
        return false;
}

function mascaraTelefone(campo) {

    function trata(valor, isOnBlur) {

        valor = valor.replace(/\D/g, "");
        valor = valor.replace(/^(\d{2})(\d)/g, "($1) $2");

        if (isOnBlur) {

            valor = valor.replace(/(\d)(\d{4})$/, "$1-$2");
        } else {

            valor = valor.replace(/(\d)(\d{3})$/, "$1-$2");
        }
        return valor;
    }

    campo.onkeypress = function (evt) {

        var code = (window.event) ? window.event.keyCode : evt.which;
        var valor = this.value

        if (code > 57 || (code < 48 && code != 8)) {
            return false;
        } else {
            this.value = trata(valor, false);
        }
    }

    campo.onblur = function () {

        var valor = this.value;
        if (valor.length < 11) {
            this.value = ""
        } else {
            this.value = trata(this.value, true);
        }
    }

    campo.maxLength = 15;
}

function onlyNumbers(evt) {
    var key_code = evt.keyCode ? evt.keyCode :
                       evt.charCode ? evt.charCode :
                       evt.which ? evt.which : void 0;

    // Habilita teclas <DEL>, <TAB>, <ENTER>, <ESC> e <BACKSPACE>
    if (key_code == 8 || key_code == 9 || key_code == 13 || key_code == 27 || key_code == 46)
        return true;


        // Habilita teclas <HOME>, <END>, mais as quatros setas de navegação (cima, baixo, direta, esquerda)
    else if ((key_code >= 35) && (key_code <= 40))
        return true

        // Habilita números de 0 a 9
    else if ((key_code >= 48) && (key_code <= 57)) {
        return true
    }

    if (window.event)
        event.keyCode = 0
    else
        return false;
}

function FormatMaskOnlyNumbers(evt, source, mask) {

    // Ajuste para campo tipo CPF/CNPJ - Fábio Martins
    // caso não seja informada a máscara, trata-se de campo em que não sabemos se será CPF ou CNPJ;
    // neste caso, a máscara inicia-se gerada como CPF; caso ultrapasse o tamanho máximo do CPF,
    // a máscara é automaticamente convertida para CNPJ
    // OBS.: esta alteração não afeta os demais campos que utilizam esta função
    if (mask.length == 0) {
        if (source.value.length <= 14)
            mask = '###.###.###-##';
        else {
            mask = '##.###.###/####-##';

            if (source.value.length == 15) // converte a máscara existente para CNPJ
            {
                var temp = source.value.replace('.', '').replace('.', '').replace('-', '');
                source.value = temp.substring(0, 2) + '.' + temp.substring(2, 5) + '.' + temp.substring(5, 8) + '/' + temp.substring(8);
            }
        }
    }

    if (maskKeyPress(evt)) {
        var key_code = evt.keyCode ? evt.keyCode :
                           evt.charCode ? evt.charCode :
                           evt.which ? evt.which : void 0;

        // Habilita teclas <DEL>, <TAB>, <ENTER>, <ESC>, <BACKSPACE>, <CTRL+C> e <CTRL+V>
        if (key_code == 8 || key_code == 9 || key_code == 13 || key_code == 27 || key_code == 46 || evt.ctrlKey)
            return true;


            // Habilita teclas <HOME>, <END>, mais as quatros setas de navegação (cima, baixo, direta, esquerda)
        else if ((key_code >= 35) && (key_code <= 40))
            return true;

            // Habilita números de 0 a 9
        else if ((key_code >= 48) && (key_code <= 57)) {
            if (mask == '(##) ####-####') {
                fone(source);
            }
            else
                formatar_mascara(source, mask);

            return true;
        }
    }
    else
        return false;

}

function maskKeyPress(objEvent) {
    var iKeyCode;
    if (window.event) // IE                        
    {
        iKeyCode = objEvent.keyCode;
        if (iKeyCode >= 48 && iKeyCode <= 57)
            return true;
        return false;
    }
    else if (e.which) // Netscape/Firefox/Opera                        
    {
        iKeyCode = objEvent.which;
        if (iKeyCode >= 48 && iKeyCode <= 57)
            return true;

        return false;
    }
}

function formatar_mascara(src, mascara) {
    var campo = src.value.length;
    var saida = mascara.substring(0, 1);
    var texto = mascara.substring(campo);
    if (texto.substring(0, 1) != saida) {
        src.value += texto.substring(0, 1);
    }

}

function formatarCPF(parEvent, parTextBox) {
    if (maskKeyPress(parEvent))
        formatar_mascara(parTextBox, '###.###.###-##');
    else
        return false;
}

function ShowAviso() {
    this._popup = $find('modalloding');

    //  find the confirm ModalPopup and show it

    this._popup.show();
    return;
}

function HideAviso() {
    this._popup = $find('modalloding');

    //  find the confirm ModalPopup and show it
    if (this._popup != null)
        this._popup.hide();

    return;
}