sap.ui.define([
   "sap/ui/core/UIComponent"
], function (UIComponent) {
   "use strict";
   return UIComponent.extend("sap.ui.cliente.component", {
      metadata: {
         "interfaces": ["sap.ui.core.IAsyncContentCreation"],
         "rootView": {
            "viewName": "sap.ui.cliente.view.Tabela",
            "type": "XML",
            "async": true,
            "id": "app"
         }
      },

      init: function () {

         UIComponent.prototype.init.apply(this, arguments);

         this.getRouter().initialize();
      }
   });
});
