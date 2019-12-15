//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class CustomDateFormatter {
    static let datePickerFormat = "dd MMMM, yyyy"
    static let serverFormat = "yyyy-MM-dd"

    static func formatTo(_ format: String, _ date: String) -> Date {
        let formatter = DateFormatter()
        formatter.dateFormat = format
        return formatter.date(from: date) ?? Date()
    }

    static func formatFrom(_ format: String, _ date: Date) -> String {
        let formatter = DateFormatter()
        formatter.dateFormat = format
        return formatter.string(from: date)
    }
}
