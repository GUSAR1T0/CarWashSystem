//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class Requests {
    // Authentication
    public static let SignIn = "/api/account/client/sign-in"
    public static let SignUp = "/api/account/client/sign-up"
    public static let SignOut = "/api/account/sign-out"
    public static let GetClientData = "/api/account/client"
    public static let InitializeExternalSignIn = "/api/account/client/sign-in/external/initialize"
    public static let VerifyExternalSignIn = "/api/account/client/sign-in/external/verify"
    public static let CompleteExternalSignIn = "/api/account/client/sign-in/external/complete"
    public static let RedirectExternalSignIn = "cws://sign-in"

    public static let GetLookup = "/api/lookup/client"

    // Client Profile
    public static let GetClientProfile = "/api/client/profile"
    public static let UpdateClientProfile = "/api/client/profile"
    public static let GetClientCars = "/api/client/profile/car/list"
    public static let AddClientCar = "/api/client/profile/car"
    public static let UpdateClientCar = "/api/client/profile/car/%d"
    public static let DeleteClientCar = "/api/client/profile/car/%d"

    // Car Washes
    public static let GetCarWashList = "/api/client/car-wash/list"
    public static let GetCarWashInfo = "/api/client/car-wash/%d"

    // Appointments
    public static let GetClientAppointmentList = "/api/car-wash/appointment/list"
    public static let GetClientAppointment = "/api/car-wash/%d/appointment/%d"
    public static let AddClientAppointment = "/api/car-wash/%d/appointment"
    public static let ApproveClientAppointment = "/api/car-wash/%d/appointment/%d/approve"
    public static let CancelClientAppointment = "/api/car-wash/%d/appointment/%d/client-cancel"
}
