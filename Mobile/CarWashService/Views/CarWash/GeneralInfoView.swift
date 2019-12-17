//
//  GeneralInfoView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct GeneralInfoView: View {
    @Binding var description: String?
    @Binding var workingHours: WorkingHoursModel?
    @Binding var hasCafe: Bool
    @Binding var hasRestZone: Bool
    @Binding var hasParking: Bool
    @Binding var hasWC: Bool
    @Binding var hasCardPayment: Bool

    var body: some View {
        VStack {
            TitledContainer(CarWashFieldName.Description) {
                Text(!StringUtils.isEmptyOrNil(self.description) ? self.description! : "—")
                        .font(.subheadline)
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.top, 10)
                        .padding(.leading, 15)
                        .padding(.trailing, 15)
            }
            DividerView()
            TitledContainer(CarWashFieldName.WorkingHours) {
                VStack {
                    HStack {
                        WorkingHoursView(dayOfWeek: .monday, workingHours: self.workingHours?.monday)
                        WorkingHoursView(dayOfWeek: .tuesday, workingHours: self.workingHours?.tuesday)
                        WorkingHoursView(dayOfWeek: .wednesday, workingHours: self.workingHours?.wednesday)
                        WorkingHoursView(dayOfWeek: .thursday, workingHours: self.workingHours?.thursday)
                        WorkingHoursView(dayOfWeek: .friday, workingHours: self.workingHours?.friday)
                        WorkingHoursView(dayOfWeek: .saturday, workingHours: self.workingHours?.saturday)
                        WorkingHoursView(dayOfWeek: .sunday, workingHours: self.workingHours?.sunday)
                    }
                            .frame(height: 75) // Workaround, SwiftUI can't calculate elements heights correctly
                            .padding(.horizontal)
                }
            }
            DividerView()
            TitledContainer(CarWashFieldName.Facilities) {
                VStack {
                    FacilityTagView(title: "Cafe", isAvailable: self.hasCafe)
                    FacilityTagView(title: "Rest Zone", isAvailable: self.hasRestZone)
                    FacilityTagView(title: "Parking", isAvailable: self.hasParking)
                    FacilityTagView(title: "WC", isAvailable: self.hasWC)
                    FacilityTagView(title: "Card Payment", isAvailable: self.hasCardPayment)
                }
                        .frame(height: 200) // Workaround, SwiftUI can't calculate elements heights correctly
            }
        }
                .padding(.top, 10)
                .padding(.bottom, 30)
    }
}
