//
//  WorkingHoursView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct WorkingHoursView: View {
    var dayOfWeek: DayOfWeek
    var workingHours: WorkingDayModel?

    var body: some View {
        GeometryReader { geometry in
            VStack {
                Text(self.dayOfWeek.rawValue)
                        .font(.system(size: 18, weight: .bold))
                        .frame(width: geometry.size.width, height: nil, alignment: .center)
                        .foregroundColor(ApplicationColor.MiddleGray.toColor())
                VStack {
                    Spacer()
                    if !StringUtils.isEmptyOrNil(self.workingHours?.startTime) && !StringUtils.isEmptyOrNil(self.workingHours?.stopTime) {
                        Text(self.workingHours!.startTime!)
                                .font(.system(size: 14))
                                .frame(width: geometry.size.width, height: nil, alignment: .center)
                        Text(self.workingHours!.stopTime!)
                                .font(.system(size: 14))
                                .frame(width: geometry.size.width, height: nil, alignment: .center)
                    } else {
                        Text("—")
                                .frame(width: geometry.size.width, height: nil, alignment: .center)
                    }
                    Spacer()
                }
            }
        }
    }
}
