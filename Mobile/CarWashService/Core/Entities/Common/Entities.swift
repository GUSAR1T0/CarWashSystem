//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

protocol Entity {
    associatedtype TModel: Model
    func toEntity(_ model: TModel)
    func toModel() -> TModel
}
