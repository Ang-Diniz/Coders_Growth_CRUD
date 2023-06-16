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

            let cliente = this.getView().getModel("cliente").getData();

            if (cliente.dataDeNascimento == "") 
            {
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
                if (res.status != 200) 
                {
                    return res.text();
                }
                return res.json()
            })
            .then(res => {
                if (typeof res == "string") 
                {
                        MessageBox.error(`Erro ao cadastrar cliente: \n\n ${res}`, {
                        emphasizedAction: MessageBox.Action.CLOSE
                    });
                }
                else 
                {
                    MessageBox.success("Cliente cadastrado com sucesso !", {
                        emphasizedAction: MessageBox.Action.OK,
                        title: "Sucesso",
                        actions: [MessageBox.Action.OK], onClose: (acao) => {
                            if (acao == MessageBox.Action.OK) 
                            {
                                this.limparTelaDeCadastro();
                                this.navegarTelaDeDetalhes(res);
                            }
                        }
                    })
                }
            })
        },

        checarEntradaDaData: function (data) 
        {
            let cliente = this.getView().getModel("cliente").getData();
            
            if (cliente.dataDeNascimento == "") 
            {
                delete cliente.dataDeNascimento;
            }
            
            data = cliente.dataDeNascimento;
            
            data = new Date(data).getFullYear()

            return Validacoes.validarDataDeNascimento(data)
        },

        aoMudarCampos: function (Evento) {

            let campo = Evento.getSource();

            if (campo.getName() === "inputNome") 
            {
                let erros = Validacoes.validarNome(campo.getValue());

                Validacoes.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputEmail") 
            {
                let erros = Validacoes.validarEmail(campo.getValue());

                Validacoes.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputCPF") 
            {
                let erros = Validacoes.validarCpf(campo.getValue());

                Validacoes.mensagensErro(campo, erros);
            }
            if (campo.getName() === "inputDataDeNascimento") 
            {
                let erros = this.checarEntradaDaData(campo.getValue());

                Validacoes.mensagensErro(campo, erros);
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

            campos.forEach(inputs => Validacoes.limparInputs(inputs))
        }
    });
});