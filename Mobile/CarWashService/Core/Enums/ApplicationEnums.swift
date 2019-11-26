//
// Created by Roman Mashenkin on 23.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

struct ApplicationText {
    static let LogoTitle = "CAR WASH: CLIENT"
}

enum ApplicationColor: String {
    case Primary = "#631D76"
    case Success = "#60992D"
    case Warning = "#E56D44"
    case Danger = "#B22345"
    case Info = "#4A58B5"

    func toRGB() -> Color {
        var cString: String = self.rawValue.trimmingCharacters(in: .whitespacesAndNewlines).uppercased()

        if (cString.hasPrefix("#")) {
            cString.remove(at: cString.startIndex)
        }

        if ((cString.count) != 6) {
            return Color.gray
        }

        var rgbValue: UInt64 = 0
        Scanner(string: cString).scanHexInt64(&rgbValue)

        let red = Double(CGFloat((rgbValue & 0xFF0000) >> 16) / 255.0)
        let green = Double(CGFloat((rgbValue & 0x00FF00) >> 8) / 255.0)
        let blue = Double(CGFloat(rgbValue & 0x0000FF) / 255.0)
        return Color(red: red, green: green, blue: blue)
    }
}
