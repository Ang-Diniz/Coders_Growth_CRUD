sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
    "sap/ui/core/routing/History"
], function (Controller, JSONModel, Filter, FilterOperator, History) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.ListaClientes", {

        onInit: function () {

            this.obterClientes();
        },

        obterClientes: function () {

            let jsonCliente = new JSONModel();

            fetch("https://localhost:7147/api/cliente")
                .then(res => res.json())
                .then(res => jsonCliente.setData({ cliente: res }))

            this.getView().setModel(jsonCliente);
        },

        buscarClientes: function (oEvent) {

            let filtro = [];
            let buscar = oEvent.getParameter("query");
            if (buscar) {
                filtro.push(new Filter("nome", FilterOperator.Contains, buscar));
            }

            let tabela = this.byId("ListaClientes")
            let items = tabela.getBinding("items");
            items.filter(filtro);
        },

        aoClicarEmCadastrar: function () {

            let historico = History.getInstance();
            let paginaAnterior = historico.getPreviousHash();

            if (paginaAnterior !== undefined) {
                window.history.go(+1);
            }
            else {
                let rota = this.getOwnerComponent().getRouter();
                rota.navTo("cadastro", {}, true);
            }
        },

        aoClicarNaLinha: function (oEvent) {
            let Item = oEvent.getSource();
            let rota = this.getOwnerComponent().getRouter();
            let idDaLinhaSelecionada = Item.getBindingContext().getProperty("id")
            rota.navTo("detalhes", { id: idDaLinhaSelecionada })
        }
    });
});
