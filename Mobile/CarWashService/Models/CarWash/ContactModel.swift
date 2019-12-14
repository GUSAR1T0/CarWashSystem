//
//  ContactModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ContactModel: Identifiable {
    var id: String = UUID().uuidString
    
    let location: String
    let email: String
    let phone: String
    
    init(location: String, email: String, phone: String) {
        self.location = location
        self.email = email
        self.phone = phone
    }
}
