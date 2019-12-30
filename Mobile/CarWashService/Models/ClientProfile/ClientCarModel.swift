//
// Created by Roman Mashenkin on 10.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ClientCarModel: Codable, Identifiable {
    var id: Int
    var modelId: Int
    var governmentPlate: String
}
