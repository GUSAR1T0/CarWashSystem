//
//  AppointmentShortModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct AppointmentShortModel: Codable, Identifiable, AppointmentModel {
    var id: Int
    var carModelId: Int
    var carGovernmentPlate: String = ""
    var carWashId: Int
    var carWashName: String = ""
    var carWashLocation: String = ""
    var requestedStartDate: String = ""
    var requestedStartTime: String = ""
    var approvedStartDate: String?
    var approvedStartTime: String?
    var status: Int
}
