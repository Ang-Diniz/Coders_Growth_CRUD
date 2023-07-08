sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox",
    "../servicos/Validacoes",
    "sap/ui/core/routing/History",
    "sap/ui/core/BusyIndicator",
    "../servicos/Repositorio"
], function (Controller, JSONModel, MessageBox, Validacoes, History, BusyIndicator, Repositorio) {
    "use strict";

    const API = "https://localhost:7147/api/cliente/";
    let i18n = null;
    const modeloi18n = "i18n";
    const stringVazia = "";

    return Controller.extend("sap.ui.cliente.controller.Cadastro", {

        onInit: function () {
            
            let rota = this.getOwnerComponent().getRouter();
            
            rota.getRoute("edicao").attachPatternMatched(this.aoCoincidirRotaEdicao, this);
            rota.getRoute("cadastro").attachPatternMatched(this.aoCoincidirRotaCadastro, this);
            i18n = this.getOwnerComponent().getModel(modeloi18n).getResourceBundle();
            Validacoes.criarModeloi18n(i18n);
        },

        setarModeloAluno: function () {

            let dadosCliente = {

                "nome": stringVazia,
                "dataDeNascimento": stringVazia,
                "cpf": stringVazia,
                "email": stringVazia
            };

            this.getView().setModel(new JSONModel(dadosCliente), "cliente");
        },
        
        aoCoincidirRotaCadastro: function () {

            const tituloTelaDeCadastro = "TituloTelaDeCadastro";
            const tituloPainelTelaDeCadastro = "TituloPainelTelaDeCadastro";

            this.setarModeloAluno();

            this.byId("tituloPagina").setTitle(i18n.getText(tituloTelaDeCadastro));
            this.byId("tituloPainel").setText(i18n.getText(tituloPainelTelaDeCadastro));

            this.limparTelaDeCadastro();
        },

        aoCoincidirRotaEdicao: function (Evento) {

            const tituloTelaDeEdicao = "TituloTelaDeEdicao";
            const tituloPainelTelaDeEdicao = "TituloPainelTelaDeEdicao";

            BusyIndicator.show(0)

            let id = Evento.getParameter("arguments").id;
            this.obterClientes(id);

            this.byId("tituloPagina").setTitle(i18n.getText(tituloTelaDeEdicao));
            this.byId("tituloPainel").setText(i18n.getText(tituloPainelTelaDeEdicao));

            BusyIndicator.hide()
        },

        obterClientes: function (id) {

            let url = API + id;

            Repositorio.gerarRequisicao(url)
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

        editarCliente: function (id) {

            let cliente = this.getView().getModel("cliente").getData();
            id = cliente.id;
            let url = API + id;
            const SucessoAoEditar = "MensagemSucessoAoEditar";
            const ErroAoEditar = "MensagemErroAoEditar";

            this.deletarDataSeForNulaOuVazia(cliente)

            return Repositorio.gerarRequisicao(url, 'PUT', cliente)
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
                this.mudarCamposAoSalvarComErros();
            })
        },

        cadastrarCliente: function () {

            let cliente = this.getView().getModel("cliente").getData();
            let url = API;
            const SucessoAoCadastrar = "MensagemSucessoAoCadastrar";
            const ErroAoCadastrar = "MensagemErroAoCadastrar";

            this.deletarDataSeForNulaOuVazia(cliente)

            return Repositorio.gerarRequisicao(url, 'POST', cliente)
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
            })
        },

        aoClicarEmSalvar: function () {

            let cliente = this.getView().getModel("cliente").getData();
            let id = cliente.id;
            
            if (id) {

                BusyIndicator.show(0)
                this.editarCliente(id)
                BusyIndicator.hide()
            }
            else {
                
                BusyIndicator.show(0)
                this.cadastrarCliente() 
                BusyIndicator.hide()
            }
        },

        checarEntradaDaData: function (data) {

            let cliente = this.getView().getModel("cliente").getData();

            this.deletarDataSeForNulaOuVazia(cliente)

            data = cliente.dataDeNascimento;
            data = new Date(data).getFullYear()

            return Validacoes.validarDataDeNascimento(data)
        },

        mudarCamposAoSalvarComErros: function () {

            const campoObrigatorio = "MensagemCampoObrigatorio";

            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);
                
                if (campo.getValue() === stringVazia) {

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

        deletarDataSeForNulaOuVazia: function (cliente) {

            if (cliente.dataDeNascimento === stringVazia || cliente.dataDeNascimento === null) {

                delete cliente.dataDeNascimento;
            }
        },

        limparTelaDeCadastro: function () {

            let campos = ["inputNome", "inputEmail", "inputCPF", "inputDataDeNascimento"];

            campos.forEach(res => {

                let campo = this.getView().byId(res);

                campo.setValue(stringVazia);
                Validacoes.limparInputs(campo)
            });
        }
    });
});