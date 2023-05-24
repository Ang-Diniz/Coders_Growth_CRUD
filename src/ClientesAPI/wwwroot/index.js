sap.ui.define([
    "sap/ui/core/ComponentContainer"
], function (ComponentContainer) {
    "use strict";

    new ComponentContainer({
        name: "sap.ui.cliente",
        settings: {
            id: "cliente"
        },
        async: true
    }).placeAt("content");
});