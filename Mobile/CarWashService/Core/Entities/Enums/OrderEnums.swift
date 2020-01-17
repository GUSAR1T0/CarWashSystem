//
//  OrderEnums.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

let OrderStatusList = [(Int, String)](
    arrayLiteral: (1, "Opened"),
    (2, "Approved"),
    (3, "Response Is Required"),
    (4, "In Progress"),
    (5, "Processed"),
    (6, "Incident"),
    (7, "Cancelled By Client"),
    (8, "Cancelled By Car Wash"))
