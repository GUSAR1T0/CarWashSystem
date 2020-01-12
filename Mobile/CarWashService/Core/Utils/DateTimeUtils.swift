//
// Created by Roman Mashenkin on 05.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class DateTimeUtils {
    static func getTotalTime(times: [String]) -> String {
        var result: Int = 0
        for time in times {
            let components: Array = time.components(separatedBy: ":")
            let hours = Int(components[0]) ?? 0
            let minutes = Int(components[1]) ?? 0
            result += Int(hours * 60 + minutes)
        }
        return String(format: "%02d:%02d", result / 60, result % 60);
    }

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

    static func getDayOfWeek(_ date: Date) -> DayOfWeek? {
        let formatter = DateFormatter()
        formatter.dateFormat = "EEE"
        let weekday = formatter.string(from: date)
        switch (weekday) {
        case "Mon":
            return .monday
        case "Tue":
            return .tuesday
        case "Wed":
            return .wednesday
        case "Thu":
            return .thursday
        case "Fri":
            return .friday
        case "Sat":
            return .saturday
        case "Sun":
            return .sunday
        default:
            return nil
        }
    }
}
