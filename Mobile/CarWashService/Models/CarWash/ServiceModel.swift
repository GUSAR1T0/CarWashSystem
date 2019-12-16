//
//  ServiceModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ServiceModel: Identifiable {
    var id: Int
    let serviceName: String
    var description: String? = nil
    let price: Float
    let duration: String
}
