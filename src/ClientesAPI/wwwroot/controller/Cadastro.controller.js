sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
], function (Controller, History, JSONModel, MessageBox) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.getRoute("cadastro").attachPatternMatched(this.aoCoincidirRota, this);
        },

        aoCoincidirRota: function () {

            let dadosCliente =
            {
                "nome": "",
                "dataDeNascimento": "",
                "cpf": "",
                "email": ""
            };

            let jsonCliente = new JSONModel(dadosCliente);
            this.getView().setModel(jsonCliente, "cliente");

            this.limparTelaDeCadastro();
        },

        aoClicarEmSalvar: function () {

            let cliente = this.getView().getModel("cliente").getData();

            fetch("https://localhost:7147/api/cliente/", {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },

                body: JSON.stringify(cliente)
            })
                .then(res => res.json())
                .then(res => {
                    if (res.status == 400) 
                    {
                        MessageBox.error("Erro ao cadastrar cliente");
                    }
                    else {
                        MessageBox.success("Cliente cadastrado com sucesso !", {
                            title: "Sucesso",
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao == MessageBox.Action.OK) {
                                    this.limparTelaDeCadastro();
                                    this.navegarTelaDetalhes(res);
                                }
                            }
                        })
                    }
                });
        },

        navegarTelaDetalhes: function (id) {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("detalhes", { id: id });
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
        },

        aoClicarEmCancelar: function () {

            MessageBox.alert("Deseja mesmo cancelar ?", {
                icon: MessageBox.Icon.WARNING,
                actions: [MessageBox.Action.YES, MessageBox.Action.NO], onClose: (acao) => {
                    if (acao == MessageBox.Action.YES) {
                        this.aoClicarEmVoltar();
                        this.limparTelaDeCadastro();
                    }
                }
            })
        },

        limparTelaDeCadastro: function () {

            let Nome = this.byId("inputNome");
            let Email = this.byId("inputEmail");
            let CPF = this.byId("inputCPF");
            let DataDeNascimento = this.byId("inputDataDeNascimento");

            Nome.setValue("");
            Email.setValue("");
            CPF.setValue("");
            DataDeNascimento.setValue("");
        }
    });
});