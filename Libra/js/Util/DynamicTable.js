DynamicTable = {
    isInitialized: false,
    formatters: new Array(),
    defaultClasses: 'table table-striped table-bordered',

    initiliaze: function () {
        if (this.isInitialized) {
            return;
        }
        this.isInitialized = true;

        this.addFormatter('currency', function (v) {
            return SysMap.formatMoney(v);
        });

        this.addFormatter('custom-currency', function (v) {
            return SysMap.formatMoney(v).replace('R$', '');
        });

        this.addFormatter('percentage', function (v) {
            return SysMap.formatDouble(v, 2) + '%';
        });

        this.addFormatter('double', function (v) {
            return SysMap.formatDouble(v, 2);
        });

        this.addFormatter('yes-no', function (v) {
            return v ? '<span class="label label-success"><i class="fa fa-thumbs-o-up"></i> Sim</span>' : '<span class="label label-danger"><i class="fa fa-thumbs-o-down"></i> Não</span>';
        });

        this.addFormatter('checkbox', function (v, entity, row) {
            var item = $('<input type="checkbox" />');
            v ? item.prop('checked', 'true') : null;

            item.click(function () {
                entity[row.attr('data-property')] = $(this).is(':checked');
                row.attr('data-value', entity[row.attr('data-property')]);
            });

            return item;
        });

        this.addFormatter('textbox', function (v, entity, row) {
            var maskFormatterName = row.attr('data-mask-formatter');
            var item = $('<input type="text" class="form-control" />');

            item.val(entity[row.attr('data-property')]);

            if (maskFormatterName != null) {
                item.addClass(maskFormatterName)
            }

            if (row.attr('data-on-change')) {
                item.attr('data-on-change', row.attr('data-on-change'));
            }
            if (row.attr('data-on-focus')) {
                item.attr('data-on-focus', row.attr('data-on-focus'));
            }
            if (row.attr('data-on-blur')) {
                item.attr('data-on-blur', row.attr('data-on-blur'));
            }

            item.change(function () {
                entity[row.attr('data-property')] = $(this).val();
                row.attr('data-value', entity[row.attr('data-property')]);
            });

            return item;
        });

        this.addFormatter('textbox-autocomplete', function (v, entity, row) {
            var maskFormatterName = row.attr('data-mask-formatter');
            var dataMethod = row.attr('data-method');
            var isMultiple = row.attr('is-multiple');
            var elementClass = row.attr('class');
            var dynamicAttributes = row.attr('data-dynamic-attributes');


            if (!dataMethod) {
                outputWindow.debug('The attribute data-method is missing.');
                return;
            }

            if (!isMultiple) {
                outputWindow.debug('The attribute is-multiple is missing.');
                return;
            }

            if (!elementClass) {
                outputWindow.debug('The attribute elementClass is missing.');
                return;
            }

            var item = $('<input type="text" class="form-control dynamic-autocomplete ' + elementClass + ' " is-multiple="' + isMultiple + '" data-method="' + dataMethod + '" data-dynamic-attributes="' + dynamicAttributes + '" />');

            item.val(entity[row.attr('data-property')]);

            if (maskFormatterName != null) {
                item.addClass(maskFormatterName)
            }

            item.change(function () {
                entity[row.attr('data-property')] = $(this).val();
                row.attr('data-value', entity[row.attr('data-property')]);
            });

            return item;
        });

        this.addFormatter('date', function (v, entity, row) {
            return SysMap.toStringByType('date', v);
        });

        this.addFormatter('time', function (v, entity, row) {
            return SysMap.toStringByType('datetime', v);
        });

        this.addFormatter('datetime', function (v, entity, row) {
            return v.toString();
        });

        this.addFormatter('minutes-to-hourHHMM', function (v, entity, row) {
            return SysMap.convertMinutesToHHMM(v);
        });

        this.addFormatter('progress-bar', function (v) {
            if (v <= 1) {
                v *= 100;
            }

            var r = '<div class="progress progress-xs"><div class="progress-bar progress-bar-primary" style="width: ' + v + '%"></div></div>';
            r += SysMap.formatDouble(v) + '%';

            return r;
        })
    },

    addFormatter: function (name, fnCallBack) {
        this.formatters[name] = fnCallBack;
    },

    addRow: function (table, entity, fn) {
        var currentObject = this;

        //getting the html template from a template row
        var html = new String(table.find('.template').wrapAll('<div>').parent().html());;

        //parsing variables on html
        $.each(entity, function (key, value) {
            html = html.replace('${entity.' + key + '}', entity[key]);
        });

        //restoring the object
        newRow = $(html);

        //navegando pelas colunas da linha para vincular o conteúdo das propriedades do objeto
        newRow.find('td').each(function () {
            var property = $(this).attr('data-property');

            if (property) {
                var formatterName = $(this).attr('data-formatter');
                var formatter = currentObject.formatters[formatterName];

                if (entity[property] != null) {
                    if (formatter != null) {
                        outputWindow.debug('The formatter <b>' + formatterName + '</b> will be applied to the property ' + entity[property]);

                        $(this).attr('data-value', entity[property]);

                        var result = formatter(entity[property], entity, $(this));

                        if ($.type(result) === 'string') {
                            $(this).html(result);
                        } else {
                            $(this).append(result);
                        }
                    } else {
                        $(this).html(entity[property]);
                    }

                } else {
                    //outputWindow.error('There is no value to the property <b>' + property + '</b>');
                }
            }
        });

        newRow.removeClass('hide');
        newRow.removeClass('template');
        newRow.addClass('dynamic');

        newRow.attr('data-id', entity.Id);

        table.find('tbody').append(newRow);

        fn(newRow);
    },

    fillTable: function (table, entities, fn) {
        var currentObject = this;

        //limpando as linhas geradas anteriormente, caso haja
        table.find('.dynamic').remove();
        table.parent().find('.dynamic-result').empty();

        //ocultando a  linha indicativa de nenhum registro
        table.find('.no-data').addClass('hide');

        table.removeClass('hide');

        if (entities != null && entities.length > 0) {
            $.each(entities, function (i, entity) {
                currentObject.addRow(table, entity, function (row) {
                    fn(row);
                });
            });
        } else {
            if (table.find('.no-data').length == 0) {
                outputWindow.info('Nenhum registro foi informado e não há linha informativa para o usuário!');
            } else {
                table.find('.no-data').removeClass('hide');
            }
        }
    }
}

$(document).ready(function () {
    DynamicTable.initiliaze();
});