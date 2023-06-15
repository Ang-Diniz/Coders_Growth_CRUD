sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",

], function (Controller, JSONModel, MessageBox) {
    "use strict";
    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {

            let rota = this.getOwnerComponent().getRouter();
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

        aoClicarEmSalvar: function () {

            // this.aoMudarCampos()

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento == "") {
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
                    if (res.status != 200) {
                        return res.text();
                    }
                    return res.json()
                })
                .then(res => {
                    if (!res.status) {
                        MessageBox.error(`Erro ao cadastrar cliente \n ${res}`, {
                            emphasizedAction: MessageBox.Action.CLOSE
                        });
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

        aoMudarCampos: function (Evento) {

            let campo = Evento.getSource();

            if (campo.getName() === "inputNome") {
                let erros = this.validarNome(campo.getValue());

                this.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputEmail") {
                let erros = this.validarEmail(campo.getValue());

                this.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputCPF") {
                let erros = this.validarCpf(campo.getValue());

                this.mensagensErro(campo, erros);
            }
        },

        validarNome: function (nome) {

            let erros = [];

            let nomeRegex = /^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$/;
            nome = nome.trim();

            if (nome.length < 4) {
                erros.push("O campo Nome deve conter mais de 4 caracteres." + "\n");
            }

            if (!nome.match(nomeRegex)) {
                erros.push("Nome inválido. Por favor insira um nome válido.");
            }
            return erros;
        },

        validarEmail: function (email) {

            let erros = [];

            let emailRegex = /^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$/;
            email = email.trim();

            if (!email.match(emailRegex)) {
                erros.push("E-mail inválido. Por favor insira um e-mail válido.");
            }
            return erros;
        },

        validarCpf: function (cpf) {
            let erros = [];

            let cpfRegex = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
            cpf = cpf.trim();

            if (!cpf.match(cpfRegex)) {
                erros.push("Cpf inválido. Por favor insira um cpf válido.");
            }
            return erros;
        },

        mensagensErro: function (campo, erros) {

            if (erros.length > 0) {
                let estadosErro = '';
                campo.setValueState("Error");

                erros.forEach(erro => {

                    estadosErro = estadosErro + "\n" + erro;
                })

                campo.setValueStateText(estadosErro);
            }
            else {
                campo.setValueState("Success");
            }
        },

        limparInputs: function (campo) {
            campo.setValueState("None");
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

            MessageBox.alert("Deseja mesmo cancelar ?", {
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

            let Nome = this.byId("inputNome");
            let Email = this.byId("inputEmail");
            let CPF = this.byId("inputCPF");
            let DataDeNascimento = this.byId("inputDataDeNascimento");

            Nome.setValue("");
            Email.setValue("");
            CPF.setValue("");
            DataDeNascimento.setValue("");

            let campos = [Nome, Email, CPF, DataDeNascimento]

            campos.forEach(inputs => this.limparInputs(inputs))
        }
    });
});