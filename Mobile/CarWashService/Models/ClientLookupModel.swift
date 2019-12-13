//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ClientLookupModel: Codable {
    var carBrandModelsModels: [CarBrandModelsModel]

    func getModelName(id: Int?) -> String? {
        guard let id = id else {
            return nil
        }
        for model in carBrandModelsModels {
            if let modelName = model.getModelName(id: id) {
                return modelName
            }
        }
        return nil
    }
}
