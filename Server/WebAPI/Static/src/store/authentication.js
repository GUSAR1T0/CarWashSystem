import {
    GET_COMPANY_AUTHENTICATION_PROFILE_ENDPOINT,
    SIGN_IN_ENDPOINT,
    SIGN_OUT_ENDPOINT,
    SIGN_UP_ENDPOINT
} from "@/constants/endpoints";
import {
    DELETE_HTTP_REQUEST,
    GET_HTTP_REQUEST, LOOKUP_REQUEST,
    ON_LOAD_REQUEST,
    POST_HTTP_REQUEST,
    SIGN_IN_REQUEST,
    SIGN_OUT_REQUEST,
    SIGN_UP_REQUEST
} from "@/constants/actions";

export default {
    state: {
        isAuthenticated: false,
        id: null,
        name: null,
        yandexMapsApiKey: null,
        dadataApiKey: null
    },
    getters: {
        isAuthenticated: state => {
            return state.isAuthenticated;
        },
        getCompanyName: state => {
            return state.name;
        },
        getYandexMapsApiKey: state => {
            return state.yandexMapsApiKey;
        },
        getDadataApiKey: state => {
            return state.dadataApiKey;
        }
    },
    mutations: {
        [SIGN_IN_REQUEST]: (state, data) => {
            state.isAuthenticated = true;
            state.id = data.id;
            state.name = data.name;
            state.yandexMapsApiKey = data.yandexMapsApiKey;
            state.dadataApiKey = data.dadataApiKey;
        },
        [SIGN_OUT_REQUEST]: state => {
            state.isAuthenticated = false;
            state.id = null;
            state.name = null;
            state.yandexMapsApiKey = null;
            state.dadataApiKey = null;
        }
    },
    actions: {
        [ON_LOAD_REQUEST]: ({commit, dispatch}, redirectTo) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: GET_COMPANY_AUTHENTICATION_PROFILE_ENDPOINT,
                    ignoreReloadPage: true
                }).then(response => {
                    commit(SIGN_IN_REQUEST, response.data);
                    dispatch(LOOKUP_REQUEST);
                    resolve(redirectTo);
                }).catch(error => reject(error));
            });
        },
        [SIGN_IN_REQUEST]: ({commit, dispatch}, signInForm) => {
            return new Promise((resolve, reject) => {
                dispatch(POST_HTTP_REQUEST, {
                    endpoint: SIGN_IN_ENDPOINT,
                    data: signInForm,
                    ignoreReloadPage: true
                }).then(response => {
                    commit(SIGN_IN_REQUEST, response.data);
                    dispatch(LOOKUP_REQUEST);
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [SIGN_UP_REQUEST]: ({commit, dispatch}, signUpForm) => {
            return new Promise((resolve, reject) => {
                dispatch(POST_HTTP_REQUEST, {
                    endpoint: SIGN_UP_ENDPOINT,
                    data: signUpForm,
                    ignoreReloadPage: true
                }).then(response => {
                    commit(SIGN_IN_REQUEST, response.data);
                    dispatch(LOOKUP_REQUEST);
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [SIGN_OUT_REQUEST]: ({commit, dispatch}) => {
            return new Promise((resolve, reject) => {
                dispatch(DELETE_HTTP_REQUEST, {
                    endpoint: SIGN_OUT_ENDPOINT,
                    ignoreReloadPage: true
                }).then(() => {
                    commit(SIGN_OUT_REQUEST);
                    resolve();
                }).catch(error => reject(error));
            });
        }
    }
};