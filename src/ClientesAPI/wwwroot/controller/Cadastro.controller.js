sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel"
], function (Controller, History, JSONModel) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("cadastro").attachPatternMatched(this.aoCoincidirRota, this);
        },

        aoCoincidirRota: function () {

            let dadosCliente = {
                "nome": "",
                "dataDeNascimento": "",
                "cpf": "",
                "email": ""
            };

            let jsonCliente = new JSONModel(dadosCliente);
            this.getView().setModel(jsonCliente, "cliente");

            this.limparTelaDeCadastro();
        },

        aoClicarEmSalvar: function () {



        },

        aoClicarEmVoltar: function () {

            let historico = History.getInstance();
            let paginaAnterior = historico.getPreviousHash();

            if (paginaAnterior !== undefined) {
                window.history.go(-1);
            }
            else {
                let rota = this.getOwnerComponent().getRouter();
                rota.navTo("ListaClientes", {}, true);
            }
        },

        aoClicarEmCancelar: function () {

            this.aoClicarEmVoltar();
            this.limparTelaDeCadastro();
        },

        limparTelaDeCadastro: function () {

            let Nome = this.byId("inputNome");
            let Email = this.byId("inputEmail");
            let CPF = this.byId("inputCPF");
            let DataDeNascimento = this.byId("inputDataDeNascimento");

            Nome.setValue("");
            Email.setValue("");
            CPF.setValue("");
            DataDeNascimento.setValue("");
        }
    });
});