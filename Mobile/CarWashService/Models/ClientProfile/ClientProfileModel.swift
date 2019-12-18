//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ClientProfileModel: Codable {
    var firstName: String = ""
    var lastName: String = ""
    var email: String?
    var phone: String?
    var birthday: String?
}
