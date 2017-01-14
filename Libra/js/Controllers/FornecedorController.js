FornecedorController = {
    initialized: false,
    arrayFornecedores: [],

    initialize: function () {
        var _this = this;

        if (this.initialized) {
            return;
        }

        this.loadPage();
    },

    loadPage: function () {
        var _this = this;
        var table = $('.table-fornecedor');

        services.runWebMethod('GET', 'CadastroFornecedores.aspx/RetornaFornecedores', {}, function (data) {
            _this.parseFornecedoresArray(JSON.parse(data), function (parsedArray) {
                _this.arrayFornecedores = parsedArray;

                DynamicTable.fillTable(table, JSON.parse(data), function (row) {
                    _this.addAccessKey(table);
                    _this.carregarEventosLinha(row);
                });
            });
        });
    },

    carregarEventosLinha: function (row) {
        var _this = this;

        row.find('.clicavel').click(function () {
            var modal = $('.modal-fornecedor');
            modal.modal('show');
        });

        row.find('.btn-remove').click(function () {
        });

        row.find('.btn-edit').click(function () {
        });
    },

    parseFornecedoresArray: function (array, fn) {
        var parsedArray = {};

        $.each(array, function (key, value) {
            if (parsedArray[value.Id] == null) {
                parsedArray[value.Id] = value;
            }
        });

        fn(parsedArray);
    },

    addAccessKey: function (table) {
        var counter = 1;

        $.each(table.find('.dynamic'), function () {
            var row = $(this);

            row.find('td').attr('accesskey', counter);
            counter++;
        });
    }
}

$(document).ready(function () {
    FornecedorController.initialize();
});