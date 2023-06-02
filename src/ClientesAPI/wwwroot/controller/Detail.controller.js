sap.ui.define([
    "sap/ui/core/mvc/Controller",
], function (Controller) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Detail", {

        onInit: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("detail").attachPatternMatched(this, this);
        },

    })
});