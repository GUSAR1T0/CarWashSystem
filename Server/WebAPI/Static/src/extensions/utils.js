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
