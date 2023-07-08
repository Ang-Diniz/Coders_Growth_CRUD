sap.ui.define([
], function () {
    "use strict";
    
    const decimalZero = 0;
    let i18n = null;
    const stringVazia = "";

    return {

        criarModeloi18n: function (modeloI18n) {

            this.i18n = modeloI18n;
        },
        
        validarNome: function (nome) {

            let erros = [];
            const tamanhoMax = 60;
            const nomeDeveSerPreenchido = "MensagemNomeDeveSerPreenchido";
            const tamanhoMaxNome = "MensagemTamanhoMaxCaracteres";
            const nomeInválido = "MensagemNomeInvalido";

            let nomeRegex = /^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$/;
            nome = nome.trim();

            if (nome == null || nome == stringVazia) 
            {
                erros.push(this.i18n.getText(nomeDeveSerPreenchido) + "\n");
            }

            if (nome.length > tamanhoMax) 
            {
                erros.push(this.i18n.getText(tamanhoMaxNome) + "\n");
            }

            if (!nome.match(nomeRegex) && nome.length > decimalZero) 
            {
                erros.push(this.i18n.getText(nomeInválido));
            }
            return erros;
        },

        validarEmail: function (email) {

            let erros = [];
            const emailDeveSerPreenchido = "MensagemEmailDeveSerPreenchido";
            const emailInválido = "MensagemEmailInvalido";

            let emailRegex = /^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+))\.([A-Za-z]{2,})$/;
            email = email.trim();

            if (email == null || email == stringVazia) 
            {
                erros.push(this.i18n.getText(emailDeveSerPreenchido) + "\n");
            }

            if (!email.match(emailRegex) && email.length > decimalZero) 
            {
                erros.push(this.i18n.getText(emailInválido));
            }
            return erros;
        },

        validarCpf: function (cpf) {

            let erros = [];
            const cpfInválido = "MensagemCpfInvalido";
            const cpfDeveSerPreenchido = "MensagemCpfDeveSerPreenchido";
            const minimoEntradaInput = 1;
            const tamanhoMaxCaracteresRepetidos = 11;
            let strCPF = cpf.replaceAll(".", stringVazia).replace("-", stringVazia).replace(" ", stringVazia);
            let entradaCPF = new RegExp(`${strCPF[0]}`, 'g');
            let caracteresRepetidos = (strCPF.match(entradaCPF) || []).length;
            let cpfRegex = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
            let Soma;
            let Resto;
            Soma = decimalZero;

            if (caracteresRepetidos == tamanhoMaxCaracteresRepetidos ) 
            {
                erros.push(this.i18n.getText(cpfInválido));
            }

            if (cpf == null || cpf == stringVazia || cpf !== "_._._-__" && cpf.length < minimoEntradaInput) 
            {
                erros.push(this.i18n.getText(cpfDeveSerPreenchido) + "\n");
            }
            
            for (let i=1; i<=9; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (11 - i);
            Resto = (Soma * 10) % 11;
            
            if ((Resto == 10) || (Resto == 11))  Resto = 0;
            if (Resto != parseInt(strCPF.substring(9, 10)) );
            
            Soma = decimalZero;
            for (let i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (12 - i);
            Resto = (Soma * 10) % 11;
            
            if ((Resto == 10) || (Resto == 11))  Resto = decimalZero;
            if (Resto != parseInt(strCPF.substring(10, 11) ) ) 

            if ( strCPF > decimalZero ) { erros.push(this.i18n.getText(cpfInválido))} 
            
            if (!cpf.match(cpfRegex) && cpf.length > decimalZero) 
            {
                erros.push(this.i18n.getText(cpfInválido));
            }
            return erros;
        },

        validarDataDeNascimento: function (dataValida) 
        {
            let erros = [];
            const idadeMaximaPermitida = 120;
            const idadeMinimaPermitida = 18;
            const idadeMaximaCliente = "MensagemIdadeMax";
            const dataMininaPermitida = "MensagemDataMinima";
            const dataFuturaInválida = "MensagemDatasFuturas"; 
            const dataDeveSerPreenchida = "MensagemDataDeveSerPreenchida";
            
            let dataAtual = new Date(Date.now()).getFullYear()

            if (dataAtual - dataValida > idadeMaximaPermitida) 
            {
                erros.push(this.i18n.getText(idadeMaximaCliente))
            }

            if (!dataValida) 
            {
                erros.push(this.i18n.getText(dataDeveSerPreenchida)) 
            } 
            else if (dataValida <= dataAtual) 
            {
                if (dataAtual - dataValida < idadeMinimaPermitida) 
                {
                    erros.push(this.i18n.getText(dataMininaPermitida))
                }
            }
            else 
            {
                erros.push(this.i18n.getText(dataFuturaInválida))
            }
            return erros;
        },

        mensagensDeErros: function (campo, erros) {

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
                campo.setValueState("None");
            }
        },

        limparInputs: function (campo) {

            campo.setValueState("None");
        },

    };
});