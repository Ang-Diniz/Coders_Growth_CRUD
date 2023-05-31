sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], function (Controller, JSONModel) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.App", {
        onInit: function () {

            this.obterClientes();
        },

        obterClientes: function () {

            var jsonCliente = new JSONModel();

            fetch("https://localhost:7147/api/cliente/")
                .then(res => res.json())
                .then(res => jsonCliente.setData({ cliente:res }))

            this.getView().setModel(jsonCliente);
        }
    });
});
