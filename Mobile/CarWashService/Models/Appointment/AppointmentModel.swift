//
// Created by Roman Mashenkin on 17.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

protocol AppointmentModel {
    var id: Int { get set }
    var carModelId: Int { get set }
    var carGovernmentPlate: String { get set }
    var carWashId: Int { get set }
    var carWashName: String { get set }
    var carWashLocation: String { get set }
    var requestedStartDate: String { get set }
    var requestedStartTime: String { get set }
    var approvedStartDate: String? { get set }
    var approvedStartTime: String? { get set }
    var status: Int { get set }
}
