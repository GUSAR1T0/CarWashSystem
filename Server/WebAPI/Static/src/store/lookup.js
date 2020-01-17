import { GET_HTTP_REQUEST, LOOKUP_REQUEST } from "@/constants/actions";
import { GET_LOOKUP_ENDPOINT } from "@/constants/endpoints";

export default {
    state: {
        carBrandModelsModels: [],
        appointmentStatusModels: []
    },
    getters: {
        getCarBrandModelsModels: state => {
            return state.carBrandModelsModels;
        },
        getAppointmentStatusModels: state => {
            return state.appointmentStatusModels;
        }
    },
    mutations: {
        [LOOKUP_REQUEST]: (state, data) => {
            state.carBrandModelsModels = data.carBrandModelsModels;
            state.appointmentStatusModels = data.appointmentStatusModels;
        }
    },
    actions: {
        [LOOKUP_REQUEST]: ({commit, dispatch}) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: GET_LOOKUP_ENDPOINT
                }).then(response => {
                    commit(LOOKUP_REQUEST, response.data);
                    resolve();
                }).catch(error => reject(error));
            });
        }
    },
};