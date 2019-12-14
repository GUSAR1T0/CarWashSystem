//
//  CarWashesEnums.swift
//  CarWashService
//
//  Created by Anna Boykova on 14.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct CarWashesButtonTitle {
    static let Info = "General Info"
    static let Services = "Services"
    static let Contacts = "Contacts"
    static let Order = "Make a order"
}

struct CarWashFieldNames {
    static let Description = "Description"
    static let WorkingHours = "Working Hours"
    static let Facilities = "Facilities"
}

struct StatusTitels
{
    static let Open = "Open"
    static let Close = "Close"
}

struct StatusColors {
    static let Open = ApplicationColor.Success.toRGB()
    static let Close = ApplicationColor.Danger.toRGB()
}

struct StatusImages {
    static let Open = "checkmark.circle.fill"
    static let Close = "xmark.circle.fill"
}

struct Facilities {
    static let Cafe = "Cafe"
    static let RestZone = "Rest zone"
    static let Parking = "Parking"
    static let WC = "WC"
    static let CardPayment = "Card payment"
}

struct ServiceTitles {
    static let Name = "Service name"
    static let Description = "Description"
    static let Price = "Price"
    static let Duration = "Duration"
}
