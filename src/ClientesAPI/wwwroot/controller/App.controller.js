sap.ui.define([
    "sap/ui/core/mvc/Controller"
], function (Controller) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.App", {
        onInit: function () {

            const Tabela = 'https://localhost:7147/api/cliente';

            fetch(Tabela).then(res => res.json()).then(res => {
                const dataModel = new JSONModel();
                dataModel.setData({
                    items: res
                });
                this.getView().setModel(dataModel, "cliente")
            })
        }
    });
});