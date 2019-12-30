//
//  WorkingHoursModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct WorkingDayModel: Codable {
    var startTime: String?
    var stopTime: String?
}

struct WorkingHoursModel: Codable {
    var monday: WorkingDayModel
    var tuesday: WorkingDayModel
    var wednesday: WorkingDayModel
    var thursday: WorkingDayModel
    var friday: WorkingDayModel
    var saturday: WorkingDayModel
    var sunday: WorkingDayModel
}
