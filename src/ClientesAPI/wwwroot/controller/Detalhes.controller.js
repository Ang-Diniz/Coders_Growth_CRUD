sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History"
], function (Controller, JSONModel, History) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Detalhes", {

        onInit: function () {
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("detalhes").attachPatternMatched(this.aoCoincidirRota, this);
        },

        aoCoincidirRota: function (oEvento) {

            let id = oEvento.getParameter("arguments").id;

            this.ObterClientes(id);
        },

        ObterClientes: function (id) {

            let jsonCliente = new JSONModel();

            fetch("https://localhost:7147/api/cliente/" + id)
                .then(res => res.json())
                .then(res => jsonCliente.setData({ cliente: res }))

            this.getView().setModel(jsonCliente);
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
        }
    });
});