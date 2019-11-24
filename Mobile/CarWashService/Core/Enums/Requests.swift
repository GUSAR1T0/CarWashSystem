//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class Requests {
    public static let SignIn = "/api/account/client/sign-in"
    public static let SignUp = "/api/account/client/sign-up"
    public static let SignOut = "/api/account/sign-out"
    public static let GetClientData = "/api/account/client"
    public static let InitializeExternalSignIn = "/api/account/client/sign-in/external/initialize"
    public static let CompleteExternalSignIn = "/api/account/client/sign-in/external/complete"
}
