//
//  Utils.swift
//  CarWashService
//
//  Created by Anna Boykova on 17/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

//Constants region
    let PrimaryColor = hexStringToUIColor(hex:"#631D76")
    let SuccessColor = hexStringToUIColor(hex:"#60992D")
    let WarningColor = hexStringToUIColor(hex:"#E56D44")
    let DangerColor = hexStringToUIColor(hex:"#B22345")
    let InfoColor = hexStringToUIColor(hex:"#4A58B5")
    
    let AppLogoStr = "Car Wash"
    let SignInTitleStr = "Sign in to your account"
    let SignUpTitleStr = "Create account"

// Helper methods
func hexStringToUIColor (hex:String) -> Color {
    var cString:String = hex.trimmingCharacters(in: .whitespacesAndNewlines).uppercased()

    if (cString.hasPrefix("#")) {
        cString.remove(at: cString.startIndex)
    }

    if ((cString.count) != 6) {
        return Color.gray
    }

    var rgbValue:UInt64 = 0
    Scanner(string: cString).scanHexInt64(&rgbValue)

    return Color(
        red: Double(CGFloat((rgbValue & 0xFF0000) >> 16) / 255.0),
        green: Double(CGFloat((rgbValue & 0x00FF00) >> 8) / 255.0),
        blue: Double(CGFloat(rgbValue & 0x0000FF) / 255.0)
    )
}
