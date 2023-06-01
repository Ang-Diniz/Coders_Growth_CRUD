sap.ui.define([
   "sap/ui/core/UIComponent",
   "sap/ui/model/json/JSONModel",
   "sap/ui/model/resource/ResourceModel"
], function (UIComponent, JSONModel, ResourceModel) {
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
         var oData = {
            recipient: {
               name: " "
            }
         };

         var oModel = new JSONModel(oData);
         this.setModel(oModel);

         var i18nModel = new ResourceModel({
            bundleName: "sap.ui.cliente.i18n.i18n"
         });

         this.setModel(i18nModel, "i18n");
      }
   });
});
