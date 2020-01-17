//
//  OrderModel.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct OrderModel: Identifiable {
    var id: Int
    var carWashName: String
    var car: ClientCarModel
    var startTime: OrderStartTimeModel
    var orderStatus: OrderStatus
}

struct OrderStartTimeModel: Codable {
    var requestedTime: String
    var approvedTime: String?
}

class OrderStatus: NSObject {
    var status: String

    init(statusCode: Int) {
        self.status =
            OrderStatusList.first(where: { $0.0 == statusCode})?.1 ?? "Not defined"
        super.init()
    }
}
