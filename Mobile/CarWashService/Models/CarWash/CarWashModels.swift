//
//  CarWashModels.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarWashFullModel: Identifiable, Codable {
    var id: Int
    var name: String
    var email: String?
    var phone: String?
    var location: String
    var coordinateX: Decimal
    var coordinateY: Decimal
    var description: String?
    var hasCafe: Bool
    var hasRestZone: Bool
    var hasParking: Bool
    var hasWC: Bool
    var hasCardPayment: Bool
    var workingHours: WorkingHoursModel
    var isOpen: Bool
    var services: [CarWashServiceModel]
}

struct CarWashShortModel: Identifiable, Codable {
    var id: Int
    var name: String
    var location: String
    var isOpen: Bool
}
