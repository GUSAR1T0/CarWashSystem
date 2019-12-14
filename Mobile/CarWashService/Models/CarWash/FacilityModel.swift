//
//  FacilityModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct FacilityModel: Identifiable {
    var id: String = UUID().uuidString
    
    let title: String
    let isAvaiable: Bool
    
    init(title: String, isAvaiable: Bool) {
        self.title = title
        self.isAvaiable = isAvaiable
    }
}
