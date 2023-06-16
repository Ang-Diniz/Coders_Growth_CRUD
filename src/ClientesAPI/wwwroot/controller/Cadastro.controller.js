sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
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

        aoMudarCampos: function (Evento) {

            let campo = Evento.getSource();

            if (campo.getName() === "inputNome") 
            {
                let erros = this.validarNome(campo.getValue());

                this.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputEmail") 
            {
                let erros = this.validarEmail(campo.getValue());

                this.mensagensErro(campo, erros);
            }

            if (campo.getName() === "inputCPF") 
            {
                let erros = this.validarCpf(campo.getValue());

                this.mensagensErro(campo, erros);
            }
            if (campo.getName() === "inputDataDeNascimento") 
            {
                let erros = this.validarDataDeNascimento(campo.getValue());

                this.mensagensErro(campo, erros);
            }
        },

        validarNome: function (nome) {

            let erros = [];
            const decimalZero = 0;
            const minimoEntradaInput = 1;

            let nomeRegex = /^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$/;
            nome = nome.trim();

            if (nome == null || nome == "") 
            {
                erros.push("O campo 'Nome' deve ser preenchido." + "\n");
            }

            if (nome.length < 4 && nome.length > decimalZero) 
            {
                erros.push("O campo 'Nome' deve conter mais de 4 caracteres." + "\n");
            }

            if (!nome.match(nomeRegex) && nome.length > minimoEntradaInput) 
            {
                erros.push("Nome inválido. Por favor insira um nome válido.");
            }
            return erros;
        },

        validarEmail: function (email) {

            let erros = [];

            let emailRegex = /^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+))\.([A-Za-z]{2,})$/;
            email = email.trim();

            if (email == null || email == "") 
            {
                erros.push("O campo 'E-mail' deve ser preenchido." + "\n");
            }
            if (!email.match(emailRegex) && email.length > 0) 
            {
                erros.push("E-mail inválido. Por favor insira um e-mail válido.");
            }
            return erros;
        },

        validarCpf: function (cpf) {

            let erros = [];
            const minimoEntradaInput = 1;
            const decimalZero = 0;
            const tamanhoMaxCaracteresRepetidos = 11;
            let strCPF = cpf.replaceAll(".", "").replace("-", "").replace(" ", "");
            let entradaCPF = new RegExp(`${strCPF[0]}`, 'g');
            let caracteresRepetidos = (strCPF.match(entradaCPF) || []).length;
            let cpfRegex = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
            let Soma;
            let Resto;
            Soma = 0;

            if (caracteresRepetidos == tamanhoMaxCaracteresRepetidos ) 
            {
                erros.push("Cpf inválido. Por favor insira um cpf válido.");
            }

            if (cpf == null || cpf == "" || cpf !== "_._._-__" && cpf.length < minimoEntradaInput) 
            {
                erros.push("O campo 'Cpf' deve ser preenchido." + "\n");
            }
            
            for (let i=1; i<=9; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (11 - i);
            Resto = (Soma * 10) % 11;
            
            if ((Resto == 10) || (Resto == 11))  Resto = 0;
            if (Resto != parseInt(strCPF.substring(9, 10)) );
            
            Soma = 0;
            for (let i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (12 - i);
            Resto = (Soma * 10) % 11;
            
            if ((Resto == 10) || (Resto == 11))  Resto = 0;
            if (Resto != parseInt(strCPF.substring(10, 11) ) ) 

            if ( strCPF > 0 ) { erros.push("Cpf inválido. Por favor insira um cpf válido.")} 
            
            if (!cpf.match(cpfRegex) && cpf.length > decimalZero) 
            {
                erros.push("Cpf inválido. Por favor insira um cpf válido.");
            }
            return erros;
        },

        validarDataDeNascimento: function (data) 
        {
            const idadeMaxima = 80;
            const idadeMinima = 18;
            let erros = [];
            let cliente = this.getView().getModel("cliente").getData();
            
            if (cliente.dataDeNascimento == "") 
            {
                delete cliente.dataDeNascimento;
            }

            data = cliente.dataDeNascimento;

            data = new Date(data).getFullYear()

            let dataAtual = new Date(Date.now()).getFullYear()

            if (dataAtual - data > idadeMaxima) 
            {
                erros.push("Data inválida. A idade máxima é de 80 anos.")
            }
            
            if (dataAtual - data < idadeMinima) 
            {
                erros.push("Data inválida. Cliente menor de 18 anos.")
            }
            return erros;
        },

        mensagensErro: function (campo, erros) {

            const decimalZero = 0;

            if (erros.length > decimalZero) 
            {
                let estadosErro = '';
                campo.setValueState("Error");

                erros.forEach(erro => {

                    estadosErro = estadosErro + "\n" + erro;
                })
                campo.setValueStateText(estadosErro);
            }
            else 
            {
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