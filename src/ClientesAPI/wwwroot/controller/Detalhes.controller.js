sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",
    "sap/ui/core/BusyIndicator"
], function (Controller, JSONModel, MessageBox, BusyIndicator) {
    "use strict";

    const API = "https://localhost:7147/api/cliente/";
    let i18n = null;
    const modeloi18n = "i18n";

    return Controller.extend("sap.ui.cliente.controller.Detalhes", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("detalhes").attachPatternMatched(this.aoCoincidirRota, this);
            i18n = this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
        },

        aoCoincidirRota: function (Evento) {

            let id = Evento.getParameter("arguments").id;
            this.obterClientes(id);
        },

        removerCliente: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id;
            
            return fetch(API + id, {
                method: 'DELETE',
                headers: {
                    'Content-type': 'application/json'
                }
            })
        },

        aoClicarEmRemover: function () {

            let cliente = this.getView().getModel("cliente").getData();
            let id = cliente.id;
            const confirmarRemocaoDoCliente = "MensagemConfirmarRemocaoDoCliente";
            const sucessoAoRemover = "MensagemSucessoAoRemover";
            const erroAoRemover = "MensagemErroAoRemover"

            MessageBox.confirm(i18n.getText(confirmarRemocaoDoCliente), {
                emphasizedAction: MessageBox.Action.YES,
                initialFocus: MessageBox.Action.CANCEL,
                icon: MessageBox.Icon.WARNING,
                actions: [MessageBox.Action.YES, MessageBox.Action.CANCEL],
                onClose: (acao) => {
                    if (acao === MessageBox.Action.YES) {
                        BusyIndicator.show(0)
                        this.removerCliente(id)
                        .then(res => {
                            if (res.status === 200) {
                                MessageBox.success(i18n.getText(sucessoAoRemover), {
                                    emphasizedAction: MessageBox.Action.OK,
                                    title: "Sucesso",
                                    actions: [MessageBox.Action.OK], onClose : (acao) => {
                                        if (acao === MessageBox.Action.OK) {

                                            this.aoClicarEmVoltar();
                                        }
                                    }
                            });
                        } 
                        else {
                                MessageBox.error(i18n.getText(erroAoRemover), {
                                    emphasizedAction: MessageBox.Action.CLOSE
                                });
                            }
                            BusyIndicator.hide()
                        });
                    }
                }
            });
        },

        obterClientes: function (id) {

            fetch(API + id)
                .then(res => {
                    if(res.status === 404) {

                        let rota = this.getOwnerComponent().getRouter();
                        rota.navTo("notFound", {}, true);
                    }
                    else {
                        res.json()
                        .then(res => this.getView().setModel(new JSONModel(res), "cliente"))
                    }
                })
        },

        aoClicarEmEditar: function (id) {

            BusyIndicator.show(0)

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("edicao", { id: id });

            BusyIndicator.hide()
        },

        aoClicarEmVoltar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("ListaClientes", {}, true);
        }
    });
});