import {
    GET_COMPANY_PROFILE_ENDPOINT,
    SIGN_IN_ENDPOINT,
    SIGN_OUT_ENDPOINT,
    SIGN_UP_ENDPOINT
} from "@/constants/endpoints";
import {
    GET_HTTP_REQUEST,
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
        email: null,
        name: null
    },
    getters: {
        isAuthenticated: state => {
            return state.isAuthenticated;
        },
        getCompanyName: state => {
            return state.name;
        }
    },
    mutations: {
        [SIGN_IN_REQUEST]: (state, data) => {
            state.isAuthenticated = true;
            state.id = data.id;
            state.email = data.email;
            state.name = data.name;
        },
        [SIGN_OUT_REQUEST]: state => {
            state.isAuthenticated = false;
            state.id = null;
            state.email = null;
            state.name = null;
        }
    },
    actions: {
        [ON_LOAD_REQUEST]: ({commit, dispatch}, redirectTo) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: GET_COMPANY_PROFILE_ENDPOINT,
                    ignoreReloadPage: true
                }).then(response => {
                    commit(SIGN_IN_REQUEST, response.data);
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
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [SIGN_OUT_REQUEST]: ({commit, dispatch}) => {
            return new Promise((resolve, reject) => {
                dispatch(POST_HTTP_REQUEST, {
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