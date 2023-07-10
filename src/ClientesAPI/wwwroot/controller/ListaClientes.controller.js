sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
    "sap/ui/core/BusyIndicator",
    "../servicos/Repositorio"
], function (Controller, JSONModel, Filter, FilterOperator, BusyIndicator, Repositorio) {
    "use strict";

    const API = "https://localhost:7147/api/cliente/";

    return Controller.extend("sap.ui.cliente.controller.ListaClientes", {

        onInit: function () {
            
            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("ListaClientes").attachPatternMatched(this.obterClientes, this);

            this.byId("searchField").setVisible(false);
        },

        obterClientes: function () {

            BusyIndicator.show(0)

            let url = API;

            Repositorio.gerarRequisicao(url)
                .then(res => res.json())
                .then(res => { 
                    let jsonCliente = new JSONModel({ cliente: res })
                    this.getView().setModel(jsonCliente);
                })

            BusyIndicator.hide()
        },

        alternarBarraDePesquisa: function () {

            if (this.byId("searchField").getVisible() === false) {

                const searchField = this.byId("searchField");
                const searchButton = this.byId("searchButton");
                const closeButton = this.byId("closeButton");
             
                searchField.setVisible(true);
                searchButton.setVisible(false);
                closeButton.setVisible(true);
            }
            else {

                const searchField = this.byId("searchField");
                const searchButton = this.byId("searchButton");
                const closeButton = this.byId("closeButton");
            
                searchField.setVisible(false);
                searchButton.setVisible(true);
                closeButton.setVisible(false);
            }
        },

        buscarClientes: function (Evento) {

            let buscar = Evento.getParameter("newValue");
            let filtro = [];

            if (buscar) {
                filtro.push(new Filter("nome", FilterOperator.Contains, buscar));
            }

            this.byId("ListaClientes").getBinding("items").filter(filtro);
        },

        aoClicarEmCadastrar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("cadastro", {}, true);
        },

        aoClicarNaLinha: function (Evento) {

            BusyIndicator.show(0)

            let rota = this.getOwnerComponent().getRouter();
            let idDaLinhaSelecionada = Evento.getSource().getBindingContext().getProperty("id")
            rota.navTo("detalhes", { id: idDaLinhaSelecionada })

            BusyIndicator.hide()
        }, 
    });
});
