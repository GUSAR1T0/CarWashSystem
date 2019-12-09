//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ClientSignInModel: Codable, Model {
    var email: String?
    var password: String?
}
