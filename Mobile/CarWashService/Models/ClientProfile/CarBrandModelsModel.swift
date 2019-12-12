//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarBrandModelsModel: Codable, Model, Identifiable {
    var id: Int
    var name: String
    var models: [CarBrandModelModel]
}
