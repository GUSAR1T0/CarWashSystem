//
//  GeneralInfoView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct GeneralInfoView: View {
    var descriptionText: String
    var workingHours: [(WorkingHoursModel)]
    var facilities: [(FacilityModel)]

    @inlinable public init(_ descriptionText: String, workingHours: [(WorkingHoursModel)],
        facilities: [(FacilityModel)]) {
        self.descriptionText = descriptionText
        self.workingHours = workingHours
        self.facilities = facilities
    }
    
    var body: some View {
        VStack {
            TitledContainer(CarWashFieldNames.Description) {
                ScrollView( .vertical, content: {
                    Text(self.descriptionText)
                        .font(.subheadline)
                        .padding()
                })
            }
            Rectangle()
                .stroke(ApplicationColor.Primary.toRGB(), lineWidth: 1)
                .frame(height: 1.0)
            TitledContainer(CarWashFieldNames.WorkingHours) {
                HStack {
                    ForEach(self.workingHours) { wh in
                        WorkingHoursView(wh)
                            .frame(width: 50)
                    }
                }.padding(.horizontal)
            }
            Rectangle()
                .stroke(ApplicationColor.Primary.toRGB(), lineWidth: 1)
                .frame(height: 1.0)
            TitledContainer(CarWashFieldNames.Facilities) {
                ScrollView(.horizontal, content: {
                    HStack {
                        ForEach(self.facilities) { f in
                            Text(f.title)
                                .frame(width: 150)
                                .background(f.isAvaiable
                                    ? StatusColors.Open
                                    : StatusColors.Close)
                                .cornerRadius(5)
                                .foregroundColor(.white)
                        }
                    }.padding()
                })
            }
        }
    }
}
