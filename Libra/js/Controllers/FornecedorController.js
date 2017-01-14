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
        var id = row.attr('data-id');

        row.find('.clicavel').click(function () {
            _this.carregaFornecedorPorId(id);
        });

        row.find('.btn-remove').click(function () {
            Libra.UI.showQuestion('Deseja realmente excluir o fornecedor?', function (r) {
                if (r) {
                    _this.excluirFornecedorPorId(id);
                }
            });
        });

        row.find('.btn-edit').click(function () {
        });
    },

    parseFornecedoresArray: function (array, fn) {
        var parsedArray = {};

        $.each(array, function (key, value) {
            if (parsedArray[value.Id] === null) {
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
    },

    carregaFornecedorPorId: function (fornecedorId) {
        var _this = this;
        var table = $('.modal-fornecedor').find('.table-view-fornecedor');
        var array = [];

        services.runWebMethod('GET', 'CadastroFornecedores.aspx/RetornaFornecedor', { id: fornecedorId }, function (data) {
            array.push(JSON.parse(data));

            DynamicTable.fillTable(table, array, function (row) {
                $('.modal-fornecedor').modal('show');
            });
        });
    },

    excluirFornecedorPorId: function (fornecedorId) {
        var _this = this;
        var table = $('.modal-fornecedor').find('.table-view-fornecedor');

        services.runWebMethod('GET', 'CadastroFornecedores.aspx/ExcluirFornecedor', { id: fornecedorId }, function (data) {
            var retorno = JSON.parse(data);
            Libra.UI.showSuccess(retorno.Message, function () {
                _this.loadPage();
            });
        });
    }
}

$(document).ready(function () {
    FornecedorController.initialize();
});