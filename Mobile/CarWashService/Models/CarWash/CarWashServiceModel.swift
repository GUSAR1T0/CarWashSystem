//
//  ServiceModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarWashServiceModel: Identifiable, Codable {
    var id: Int
    var serviceName: String
    var description: String? = nil
    var price: Float
    var duration: String
    var isAvailable: Bool
}
