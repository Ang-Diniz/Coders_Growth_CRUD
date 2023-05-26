sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/m/MessageToast"   
], function (Controller, MessageToast) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.App", {
        onShowHello: function () {
            var oBundle = this.getView().getModel("i18n").getResourceBundle();
            var sRecipient = this.getView().getModel().getProperty("/recipient/name");
            var sMsg = oBundle.getText("Cliente cadastrado", [sRecipient]);
            MessageToast.show(sMsg);
        }
    });
});