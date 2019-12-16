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

struct CarWashFieldName {
    static let Description = "Description"
    static let WorkingHours = "Working Hours"
    static let Facilities = "Facilities"
}

struct StatusTitle
{
    static let Open = "Open"
    static let Close = "Close"
}

struct StatusColor {
    static let Open = ApplicationColor.Success.toRGB()
    static let Close = ApplicationColor.Danger.toRGB()
}

struct StatusImage {
    static let Open = "checkmark.circle.fill"
    static let Close = "xmark.circle.fill"
}

struct Facility {
    static let Cafe = "Cafe"
    static let RestZone = "Rest zone"
    static let Parking = "Parking"
    static let WC = "WC"
    static let CardPayment = "Card payment"
}

struct ServiceTitle {
    static let Description = "Description"
    static let Price = "Price"
    static let Duration = "Duration"
}
