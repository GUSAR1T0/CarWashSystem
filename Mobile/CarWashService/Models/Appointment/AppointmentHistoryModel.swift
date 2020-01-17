//
// Created by Roman Mashenkin on 17.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct AppointmentHistoryModel : Codable, Identifiable {
    var id: Int
    var action: String = ""
    var timestamp: String = ""
}
