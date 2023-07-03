sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",
    "../servicos/Validacoes",
    "sap/ui/core/routing/History",
    "sap/ui/core/BusyIndicator"
], function (Controller, JSONModel, MessageBox, Validacoes, History, BusyIndicator) {
    "use strict";

    const API = "https://localhost:7147/api/cliente/";
    let i18n = null;
    const modeloi18n = "i18n";

    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {
            
            let rota = this.getOwnerComponent().getRouter();
            
            rota.getRoute("edicao").attachPatternMatched(this.aoCoincidirRotaEdicao, this);
            rota.getRoute("cadastro").attachPatternMatched(this.aoCoincidirRota, this);
            i18n = this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
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

            this.byId("tituloPagina").setTitle("Tela de cadastro")
            this.byId("tituloPainel").setText("Cadastrar cliente")

            this.limparTelaDeCadastro();
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
                        .then(res => {
                            res.dataDeNascimento = new Date(res.dataDeNascimento);
                            this.getView().setModel(new JSONModel(res), "cliente")
                    })
                }
            })
        },

        aoCoincidirRotaEdicao: function (Evento) {

            BusyIndicator.show(0)

            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);
                campo.setValueState("Success")
            })

            let id = Evento.getParameter("arguments").id;
            this.obterClientes(id);

            this.byId("tituloPagina").setTitle("Tela de edição");
            this.byId("tituloPainel").setText("Editar cliente");

            BusyIndicator.hide()
        },

        editarCliente: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id;

            if (cliente.dataDeNascimento === "" || cliente.dataDeNascimento === null) {

                delete cliente.dataDeNascimento;
            }

            return fetch(API + id, {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json'
                },

                body: JSON.stringify(cliente)
            });
        },

        cadastrarCliente: function () {

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento === "" || cliente.dataDeNascimento === null) {

                delete cliente.dataDeNascimento;
            }

            return fetch(API, {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },

                body: JSON.stringify(cliente)
            })
        },

        aoClicarEmSalvar: function () {

            let cliente = this.getView().getModel("cliente").getData();
            let id = cliente.id;
            const SucessoAoEditar = "MensagemSucessoAoEditar";
            const SucessoAoCadastrar = "MensagemSucessoAoCadastrar";
            const ErroAoEditar = "MensagemErroAoEditar";
            const ErroAoCadastrar = "MensagemErroAoCadastrar";
            
            if (id) {
                BusyIndicator.show(0)

                this.editarCliente(id)
                .then(res => {
                    if (res.status !== 200) {

                        return res.text();
                    }
                    return res.json()
                })
                .then(res => {
                    if (typeof res === "string") {
                        MessageBox.error(i18n.getText(ErroAoEditar) + `\n\n ${res}`, {
                            emphasizedAction: MessageBox.Action.CLOSE
                        });
                    }
                    else {
                        MessageBox.success(i18n.getText(SucessoAoEditar), {
                            emphasizedAction: MessageBox.Action.OK,
                            title: "Sucesso",
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao === MessageBox.Action.OK) {
                                    this.limparTelaDeCadastro();
                                    this.aoClicarEmVoltar();
                                }
                            }
                        })
                    }
                })
                BusyIndicator.hide()

                this.mudarCamposAoSalvarComErros();
            }
            else {
                BusyIndicator.show(0)

                this.cadastrarCliente() 
                .then(res => {
                    if (res.status !== 200) {

                        return res.text();
                    }
                    return res.json()
                })
                .then(res => {
                    if (typeof res === "string") {
                        MessageBox.error(i18n.getText(ErroAoCadastrar) + `\n\n ${res}`, {
                            emphasizedAction: MessageBox.Action.CLOSE
                        });

                        this.mudarCamposAoSalvarComErros();
                    }
                    else {
                        MessageBox.success(i18n.getText(SucessoAoCadastrar), {
                            emphasizedAction: MessageBox.Action.OK,
                            title: "Sucesso",
                            actions: [MessageBox.Action.OK], onClose: (acao) => {
                                if (acao === MessageBox.Action.OK) {
                                    this.limparTelaDeCadastro();
                                    this.navegarTelaDeDetalhes(res);
                                }
                            }
                        })
                    }
                    BusyIndicator.hide()

                })
            }
        },

        checarEntradaDaData: function (data) {

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento === "" || cliente.dataDeNascimento === null) {

                delete cliente.dataDeNascimento;
            }

            data = cliente.dataDeNascimento;

            data = new Date(data).getFullYear()

            return Validacoes.validarDataDeNascimento(data)
        },

        mudarCamposAoSalvarComErros: function () {

            const campoObrigatorio = "MensagemCampoObrigatorio";
            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);
                
                if (campo.getValueState() !== "Success" || campo.getValueState() === "Success" && campo.getValue() === "") {

                    campo.setValueState("Error")
                    campo.setValueStateText(i18n.getText(campoObrigatorio))
                }
            })
        },

        aoMudarCampos: function (Evento) {

            let campo = Evento.getSource();

            if (campo.getName() === "inputNome") {

                let erros = Validacoes.validarNome(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }

            if (campo.getName() === "inputEmail") {

                let erros = Validacoes.validarEmail(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }

            if (campo.getName() === "inputCPF") {

                let erros = Validacoes.validarCpf(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }
            if (campo.getName() === "inputDataDeNascimento") {

                let erros = this.checarEntradaDaData(campo.getValue());

                Validacoes.mensagensDeErros(campo, erros);
            }
        },

        navegarTelaDeDetalhes: function (id) {

            BusyIndicator.show(0)

            let rota = this.getOwnerComponent().getRouter();
            rota.navTo("detalhes", { id: id });
            
            BusyIndicator.hide()
        },

        aoClicarEmVoltar: function () {

            BusyIndicator.show(0)

            let historico = History.getInstance();
			let paginaAnterior = historico.getPreviousHash();

			if (paginaAnterior !== undefined) {

				window.history.go(-1);
			} 
            else {

                let rota = this.getOwnerComponent().getRouter();
                rota.navTo("ListaClientes", {}, true);
            }

            this.limparTelaDeCadastro();

            BusyIndicator.hide()
        },

        aoClicarEmCancelar: function () {

            const confirmarCancelamento = "MensagemConfirmarCancelamento";

            MessageBox.confirm(i18n.getText(confirmarCancelamento), {
                emphasizedAction: MessageBox.Action.YES,
                initialFocus: MessageBox.Action.NO,
                icon: MessageBox.Icon.WARNING,
                actions: [MessageBox.Action.YES, MessageBox.Action.NO], onClose: (acao) => {
                    BusyIndicator.show(0)
                    if (acao === MessageBox.Action.YES) {

                        this.aoClicarEmVoltar();
                        this.limparTelaDeCadastro();
                    }
                    BusyIndicator.hide()
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