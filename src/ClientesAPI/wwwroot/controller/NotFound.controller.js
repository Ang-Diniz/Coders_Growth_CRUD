sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History"
 ], function (Controller, History) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.NotFound", {
       onInit: function () {
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