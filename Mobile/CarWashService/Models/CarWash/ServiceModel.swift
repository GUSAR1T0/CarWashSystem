//
//  ServiceModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ServiceModel: Identifiable {
    var id: String = UUID().uuidString
    
    let name: String
    let description: String
    let price: Int
    let duration: String
    
    init(name: String, description: String, price: Int, duration: String) {
        self.name = name
        self.description = description
        self.price = price
        self.duration = duration
    }
}
