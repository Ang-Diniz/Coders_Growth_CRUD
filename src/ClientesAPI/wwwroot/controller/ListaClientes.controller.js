﻿sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
], function (Controller, JSONModel, Filter, FilterOperator) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.ListaClientes", {

        onInit: function () {
            
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("ListaClientes").attachPatternMatched(this.obterClientes, this);
        },

        obterClientes: function () {

            let jsonCliente = new JSONModel();

            fetch("https://localhost:7147/api/cliente/")
                .then(res => res.json())
                .then(res => jsonCliente.setData({ cliente: res }))

            this.getView().setModel(jsonCliente);
        },

        buscarClientes: function (Evento) {

            let buscar = Evento.getParameter("query");
            let filtro = [];

            if (buscar) 
            {
                filtro.push(new Filter("nome", FilterOperator.Contains, buscar));
            }

            let tabela = this.byId("ListaClientes")
            let items = tabela.getBinding("items");
            items.filter(filtro);
        },

        aoClicarEmCadastrar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("cadastro", {}, true);
        },

        aoClicarNaLinha: function (Evento) {

            let Item = Evento.getSource();
            let rota = this.getOwnerComponent().getRouter();
            let idDaLinhaSelecionada = Item.getBindingContext().getProperty("id")
            rota.navTo("detalhes", { id: idDaLinhaSelecionada })
        }, 

        atualizarTabela: function () {

            this.obterClientes();
       },
    });
});
