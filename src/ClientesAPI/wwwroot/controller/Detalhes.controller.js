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
            this.obterClientes(id);
        },

        obterClientes: function (id) {

            fetch("https://localhost:7147/api/cliente/" + id)
                .then(res => res.json())
                .then(res => this.getView().setModel(new JSONModel(res), "cliente"))
        },

        aoClicarEmEditar: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("edicao", { id: id });
        },

        aoClicarEmVoltar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("ListaClientes", {}, true);
        }
    });
});