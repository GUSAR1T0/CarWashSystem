import {
    DELETE_HTTP_REQUEST,
    GET_HTTP_REQUEST,
    POST_HTTP_REQUEST,
    PUT_HTTP_REQUEST
} from "@/constants/actions";
import axios from "axios";

let printResponseErrors = error => {
    let message = "";
    if (error.response) {
        message += `\n -> STATUS: ${error.response.statusText} (${error.response.status})`;
        if (error.response.data) {
            message += `\n -> SOURCE: ${error.response.data.source}`;
            message += `\n -> TARGET: ${error.response.data.target}`;
            message += `\n -> MESSAGE: ${error.response.data.message}`;
            message += `\n -> STACK TRACE: ${error.response.data.stackTrace}`;
        }
        if (error.response.config) {
            message += `\n -> URL: ${error.response.config.method.toUpperCase()} ${error.response.config.url}`;
            message += `\n -> REQUEST: ${JSON.stringify(error.response.config.data, null, 4)}`;
        }
    }

    // eslint-disable-next-line no-console
    console.error(`The error issue is caused: ${message ? message : "Unhandled exception"}`);
    return Promise.reject(error);
};

let sendRequest = (state, payload, request) => {
    let client = axios.create();
    client.interceptors.response.use(undefined, error => {
        if (error.response.status === 401) {
            if (payload.ignoreReloadPage !== true) {
                window.location.reload();
            }
        }
        return printResponseErrors(error);
    });
    return request(client);
};

export default {
    actions: {
        [GET_HTTP_REQUEST]: ({state}, payload = {}) => {
            return sendRequest(state, payload, client => client.get(`/${payload.endpoint}`, payload.config));
        },
        [POST_HTTP_REQUEST]: ({state}, payload = {}) => {
            return sendRequest(state, payload, client => client.post(`/${payload.endpoint}`, payload.data, payload.config));
        },
        [PUT_HTTP_REQUEST]: ({state}, payload = {}) => {
            return sendRequest(state, payload, client => client.put(`/${payload.endpoint}`, payload.data, payload.config));
        },
        [DELETE_HTTP_REQUEST]: ({state}, payload = {}) => {
            return sendRequest(state, payload, client => client.delete(`/${payload.endpoint}`, payload.config));
        }
    }
};