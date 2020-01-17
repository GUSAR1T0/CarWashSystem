export function renderErrorNotificationMessage(h, response) {
    if (response && response.data) {
        return h("div", null, [
            "Response (", h("strong", null, response.status), "): ",
            h("div", {style: "margin-top: 5px; margin-left: 5px"}, [
                h("strong", null, response.data.message)
            ])
        ]);
    } else {
        return "Unhandled exception";
    }
}

export function getCarBrandModelName(carBrandModelsModels, carModelId) {
    for (let i = 0; i < carBrandModelsModels.length; i++) {
        let brand = carBrandModelsModels[i];
        let models = brand.models.filter(model => model.id === carModelId);
        if (models.length > 0) {
            return `${brand.name} ${models[0].name}`;
        }
    }
    return "";
}

export function getAppointmentStatusModels(appointmentStatusModels, statusId) {
    let models = appointmentStatusModels.filter(model => model.id === statusId);
    return models.length > 0 ? models[0].name : "";
}
