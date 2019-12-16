//
//  GeneralInfoView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct GeneralInfoView: View {
    let carWash: CarWashModel

    var body: some View {
        VStack {
            TitledContainer(CarWashFieldName.Description) {
                Text(self.carWash.description)
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
                        WorkingHoursView(dayOfWeek: .monday, workingHours: self.carWash.workingHours.monday)
                        WorkingHoursView(dayOfWeek: .tuesday, workingHours: self.carWash.workingHours.tuesday)
                        WorkingHoursView(dayOfWeek: .wednesday, workingHours: self.carWash.workingHours.wednesday)
                        WorkingHoursView(dayOfWeek: .thursday, workingHours: self.carWash.workingHours.thursday)
                        WorkingHoursView(dayOfWeek: .friday, workingHours: self.carWash.workingHours.friday)
                        WorkingHoursView(dayOfWeek: .saturday, workingHours: self.carWash.workingHours.saturday)
                        WorkingHoursView(dayOfWeek: .sunday, workingHours: self.carWash.workingHours.sunday)
                    }
                            .frame(height: 75) // Workaround, SwiftUI can't calculate elements heights correctly
                            .padding(.horizontal)
                }
            }
            DividerView()
            TitledContainer(CarWashFieldName.Facilities) {
                VStack {
                    FacilityTagView(title: "Cafe", isAvailable: self.carWash.hasCafe)
                    FacilityTagView(title: "Rest Zone", isAvailable: self.carWash.hasRestZone)
                    FacilityTagView(title: "Parking", isAvailable: self.carWash.hasParking)
                    FacilityTagView(title: "WC", isAvailable: self.carWash.hasWC)
                    FacilityTagView(title: "Card Payment", isAvailable: self.carWash.hasCardPayment)
                }
                        .frame(height: 200) // Workaround, SwiftUI can't calculate elements heights correctly
            }
        }
    }
}
