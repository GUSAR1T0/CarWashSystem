//
//  WorkingHoursModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct WorkingHoursModel: Identifiable {
    var id: String = UUID().uuidString
    
    let day: String
    let startTime: String
    let endTime: String
    
    init(day: String, startTime: String, endTime: String) {
        self.day = day
        self.startTime = startTime
        self.endTime = endTime
    }
}
