sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], function (Controller, JSONModel) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Detalhes", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("detalhes").attachPatternMatched(this.aoCoincidirRota, this);
        },

        aoCoincidirRota: function (Evento) {

            let id = Evento.getParameter("arguments").id;
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

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("ListaClientes", {}, true);
        }
    });
});