sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
], function (Controller, JSONModel, MessageBox) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Detalhes", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("detalhes").attachPatternMatched(this.aoCoincidirRota, this);
        },

        aoCoincidirRota: function (Evento) {

            let id = Evento.getParameter("arguments").id;
            this.obterClientes(id);
        },

        removerCliente: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id;
            
            return fetch("https://localhost:7147/api/cliente/" + id, {
                    method: 'DELETE',
                    headers: {
                        'Content-type': 'application/json'
                    }
                })
        },

        aoClicarEmRemover: function () {

            let cliente = this.getView().getModel("cliente").getData();
            let id = cliente.id;

            MessageBox.confirm("Tem certeza que deseja remover esse cliente ?", {
                emphasizedAction: MessageBox.Action.YES,
                initialFocus: MessageBox.Action.NO,
                icon: MessageBox.Icon.WARNING,
                actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                onClose: (acao) => {
                    if (acao === MessageBox.Action.YES) {
                        this.removerCliente(id)
                        .then(res => {
                            if (res.status == 200) {
                                MessageBox.success("Cliente removido com sucesso !", {
                                    emphasizedAction: MessageBox.Action.OK,
                                    title: "Sucesso",
                                    actions: [MessageBox.Action.OK], onClose : (acao) => {
                                        if (acao == MessageBox.Action.OK) {
                                            this.aoClicarEmVoltar();
                                        }
                                    }
                            });
                        } else {
                                MessageBox.error("Erro ao remover cliente.", {
                                    emphasizedAction: MessageBox.Action.CLOSE
                                });
                            }
                        });
                    }
                }
            });
        },

        obterClientes: function (id) {

            fetch("https://localhost:7147/api/cliente/" + id)
                .then(res => res.json())
                .then(res => this.getView().setModel(new JSONModel(res), "cliente"))
        },

        aoClicarEmEditar: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("edicao", { id: id });
        },

        aoClicarEmVoltar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("ListaClientes", {}, true);
        }
    });
});