//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarBrandModelsModel: Codable, Model, Identifiable {
    var id: Int
    var name: String
    var models: [CarBrandModelModel]

    func getModelName(id: Int) -> String? {
        if models.contains(where: { model in model.id == id }) {
            return "\(name) \(models.first(where: { model in model.id == id })?.name ?? "—")"
        }
        return nil
    }
}
