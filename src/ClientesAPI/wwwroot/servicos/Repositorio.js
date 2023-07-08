sap.ui.define([
], function () {
    "use strict";

    return {

        gerarRequisicao: function (url, metodo, corpo) {

            return fetch(url, {
                method: metodo,
                headers: {
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(corpo)
            });
        }
    };
});