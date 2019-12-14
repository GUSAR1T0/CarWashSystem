//
//  CarWashModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarWashModel: Identifiable {
    var id: String = UUID().uuidString
    
    let name: String
    let location: String
    let description: String
    let isOpen: Bool
    let workingHours: [(WorkingHoursModel)]
    let facilities: [(FacilityModel)]
    let services: [(ServiceModel)]
    let contacts: [(ContactModel)]
    
    init(name: String, location: String, description: String, isOpen: Bool,
        workingHours: [(WorkingHoursModel)], facilities: [(FacilityModel)],
        services: [(ServiceModel)], contacts: [(ContactModel)]) {
        self.name = name
        self.location = location
        self.description = description
        self.isOpen = isOpen
        self.workingHours = workingHours
        self.facilities = facilities
        self.services = services
        self.contacts = contacts
    }
}
