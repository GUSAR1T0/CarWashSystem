// Lookup endpoints
export const GET_LOOKUP_ENDPOINT = "api/lookup/company";

// Account endpoints
export const GET_COMPANY_AUTHENTICATION_PROFILE_ENDPOINT = "api/account/company";
export const SIGN_IN_ENDPOINT = "api/account/company/sign-in";
export const SIGN_UP_ENDPOINT = "api/account/company/sign-up";
export const SIGN_OUT_ENDPOINT = "api/account/sign-out";

// Company profile endpoints
export const GET_COMPANY_PROFILE_ENDPOINT = "api/company/profile";
export const UPDATE_COMPANY_PROFILE_ENDPOINT = "api/company/profile";
export const GET_CAR_WASH_LIST_ENDPOINT = "api/company/profile/car-wash/list";
export const GET_CAR_WASH_ENDPOINT = "api/company/profile/car-wash/{carWashId}";
export const ADD_CAR_WASH_ENDPOINT = "api/company/profile/car-wash";
export const UPDATE_CAR_WASH_ENDPOINT = "api/company/profile/car-wash/{carWashId}";
export const DELETE_CAR_WASH_ENDPOINT = "api/company/profile/car-wash/{carWashId}";
export const GET_CAR_WASH_SERVICE_LIST_ENDPOINT = "api/company/profile/car-wash/{carWashId}/service";
export const ADD_CAR_WASH_SERVICE_ENDPOINT = "api/company/profile/car-wash/{carWashId}/service";
export const UPDATE_CAR_WASH_SERVICE_ENDPOINT = "api/company/profile/car-wash/{carWashId}/service/{serviceId}";
export const DELETE_CAR_WASH_SERVICE_ENDPOINT = "api/company/profile/car-wash/{carWashId}/service/{serviceId}";

// Appointment endpoints
export const GET_APPOINTMENTS_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/list";
export const GET_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}";
export const UPDATE_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}";
export const APPROVE_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/approve";
export const REQUIRE_RESPONSE_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/response";
export const IN_PROGRESS_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/in-progress";
export const PROCESSED_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/processed";
export const INCIDENT_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/incident";
export const CLOSE_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/close";
export const CANCEL_APPOINTMENT_TO_CAR_WASH_ENDPOINT = "api/car-wash/{carWashId}/appointment/{appointmentId}/company-cancel";
