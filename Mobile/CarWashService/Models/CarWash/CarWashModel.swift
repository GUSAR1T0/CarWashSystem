//
//  CarWashModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarWashModel: Identifiable {
    var id: Int
    let name: String
    let location: String
    let description: String
    let email: String
    let phone: String
    let isOpen: Bool // Define by working hours
    let workingHours: WorkingHoursModel
    let hasCafe: Bool
    let hasRestZone: Bool
    let hasParking: Bool
    let hasWC: Bool
    let hasCardPayment: Bool

    let services: [ServiceModel]
}
