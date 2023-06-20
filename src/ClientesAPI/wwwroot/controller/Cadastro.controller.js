sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",
    "../servicos/Validacoes"
], function (Controller, JSONModel, MessageBox, Validacoes) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();

            rota.getRoute("edicao").attachPatternMatched(this.aoCoincidirRotaEdicao, this);

            rota.getRoute("cadastro").attachPatternMatched(this.aoCoincidirRota, this);

        },

        aoCoincidirRota: function () {

            let dadosCliente = {

                "nome": "",
                "dataDeNascimento": "",
                "cpf": "",
                "email": ""
            };

            let jsonCliente = new JSONModel(dadosCliente);
            this.getView().setModel(jsonCliente, "cliente");

            this.limparTelaDeCadastro();
        },

        obterClientes: function (id) {

            fetch("https://localhost:7147/api/cliente/" + id)
                .then(res => res.json())
                .then(res => this.getView().setModel(new JSONModel(res), "cliente"))
        },

        aoCoincidirRotaEdicao: function (Evento) {

            let id = Evento.getParameter("arguments").id;
            this.obterClientes(id);
        },

        aoClicarEmSalvar: function () {

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento == "" || cliente.dataDeNascimento == null) {
                delete cliente.dataDeNascimento;
            }

            fetch("https://localhost:7147/api/cliente/", {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },

                body: JSON.stringify(cliente)
            })
                .then(res => {
                    if (res.status !== 200) {
                        return res.text();
                    }
                    return res.json()
                })
                .then(res => {
                    if (typeof res == "string") {
                        MessageBox.error(`Erro ao cadastrar cliente: \n\n ${res}`, {
                            emphasizedAction: MessageBox.Action.CLOSE
                        });

                        this.mudarCamposAoSalvarComErros();
                    }
                    else {
                        MessageBox.success("Cliente cadastrado com sucesso !", {
                            emphasizedAction: MessageBox.Action.OK,
                            title: "Sucesso",
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao == MessageBox.Action.OK) {
                                    this.limparTelaDeCadastro();
                                    this.navegarTelaDeDetalhes(res);
                                }
                            }
                        })
                    }
                })
        },

        checarEntradaDaData: function (data) {

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento == "" || cliente.dataDeNascimento == null) {
                delete cliente.dataDeNascimento;
            }

            data = cliente.dataDeNascimento;

            data = new Date(data).getFullYear()

            return Validacoes.validarDataDeNascimento(data)
        },

        mudarCamposAoSalvarComErros: function () {

            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);

                if (campo.getValueState() !== "Success") {
                    campo.setValueState("Error")
                }
            })
        },

        aoMudarCampos: function (Evento) {

            let campo = Evento.getSource();

            if (campo.getName() == "inputNome") {
                let erros = Validacoes.validarNome(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }

            if (campo.getName() == "inputEmail") {
                let erros = Validacoes.validarEmail(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }

            if (campo.getName() == "inputCPF") {
                let erros = Validacoes.validarCpf(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }
            if (campo.getName() == "inputDataDeNascimento") {
                let erros = this.checarEntradaDaData(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }
        },

        navegarTelaDeDetalhes: function (id) {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("detalhes", { id: id });
        },

        aoClicarEmVoltar: function () {

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("ListaClientes", {}, true);

            this.limparTelaDeCadastro();
        },

        aoClicarEmCancelar: function () {

            MessageBox.alert("Deseja mesmo cancelar ? \n\n\n Os dados preenchidos serão perdidos.", {
                emphasizedAction: MessageBox.Action.YES,
                initialFocus: MessageBox.Action.NO,
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

            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);

                campo.setValue("");

                Validacoes.limparInputs(campo)
            });
        }
    });
});