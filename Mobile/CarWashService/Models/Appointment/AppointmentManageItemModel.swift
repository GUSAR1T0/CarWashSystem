//
// Created by Roman Mashenkin on 17.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct AppointmentManageItemModel: Codable {
    var carId: Int
    var carWashId: Int
    var startTime: String
    var carWashServiceIds: [Int]
}
