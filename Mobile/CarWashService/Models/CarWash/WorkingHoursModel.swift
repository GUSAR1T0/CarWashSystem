//
//  WorkingHoursModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct WorkingDayModel {
    let startTime: String?
    let stopTime: String?
}

struct WorkingHoursModel {
    let monday: WorkingDayModel
    let tuesday: WorkingDayModel
    let wednesday: WorkingDayModel
    let thursday: WorkingDayModel
    let friday: WorkingDayModel
    let saturday: WorkingDayModel
    let sunday: WorkingDayModel
}
