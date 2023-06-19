sap.ui.define([
], function () {
    "use strict";
    
    return {

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

            if (nome.length > 60) 
            {
                erros.push("O campo 'Nome' não deve conter mais de 60 caracteres." + "\n");
            }

            if (!nome.match(nomeRegex) && nome.length > minimoEntradaInput) 
            {
                erros.push("Nome inválido. Por favor insira um nome válido.");
            }
            return erros;
        },

        validarEmail: function (email) {

            let erros = [];
            const decimalZero = 0;

            let emailRegex = /^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+))\.([A-Za-z]{2,})$/;
            email = email.trim();

            if (email == null || email == "") 
            {
                erros.push("O campo 'E-mail' deve ser preenchido." + "\n");
            }

            if (!email.match(emailRegex) && email.length > decimalZero) 
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

        validarDataDeNascimento: function (dataValida) 
        {
            let erros = [];
            const idadeMaxima = 120;
            const idadeMinima = 18;
            
            let dataAtual = new Date(Date.now()).getFullYear()

            if (dataAtual - dataValida > idadeMaxima) 
            {
                erros.push("Data inválida. A idade máxima é de 120 anos.")
            }

            if (dataValida <= dataAtual) 
            {
                if (dataAtual - dataValida < idadeMinima) 
                {
                    erros.push("Data inválida. Cliente menor de 18 anos.")
                }
            }
            else 
            {
                erros.push("Datas futuras não são permitidas.")
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

    };
});