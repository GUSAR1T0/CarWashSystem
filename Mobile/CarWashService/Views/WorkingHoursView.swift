//
//  WorkingHoursView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct WorkingHoursView: View {
    var workingHours: WorkingHoursModel

    @inlinable public init(_ workingHours: WorkingHoursModel) {
        self.workingHours = workingHours
    }
    
    var body: some View {
        GeometryReader { geometry in
            VStack{
                Text(self.workingHours.day)
                    .font(.system(size: 24, weight: .bold))
                    .frame(width: geometry.size.width,
                        height: nil,
                        alignment: .center)
                    .foregroundColor(.white)
                    .background(ApplicationColor.Primary.toRGB())
                Text(self.workingHours.startTime)
                    .frame(width: geometry.size.width,
                        height: nil,
                        alignment: .center)
                Rectangle()
                    .stroke(ApplicationColor.Primary.toRGB(), lineWidth: 1)
                    .frame(width: geometry.size.width,
                        height: 1,
                        alignment: .center)
                Text(self.workingHours.endTime)
                    .frame(width: geometry.size.width,
                        height: nil,
                        alignment: .center)
            }
        }
    }
}
