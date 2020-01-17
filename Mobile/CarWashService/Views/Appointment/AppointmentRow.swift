//
//  AppointmentRow.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct AppointmentRow: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    var model: AppointmentModel

    private func getStatusName(_ status: Int) -> String {
        AppointmentStatusList.first(where: { $0.0 == status })?.1 ?? "Not defined"
    }

    var body: some View {
        VStack(alignment: .leading) {
            TitledContainer("Car Wash:") {
                VStack {
                    HStack {
                        Text(self.model.carWashName)
                                .font(.system(size: 20, weight: .bold))
                        Spacer()
                    }
                            .padding(.bottom, 10)
                    HStack {
                        Text(self.model.carWashLocation)
                        Spacer()
                    }
                }
                        .padding()
            }
            TitledContainer("Car Info:") {
                HStack {
                    Text(self.lookupStorage.getCarModelNameWithGovernmentPlate(modelId: self.model.carModelId, governmentPlate: self.model.carGovernmentPlate) ?? "Unknown car")
                            .bold()
                    Spacer()
                }
                        .padding()
            }
            TitledContainer("Appointment Status:") {
                HStack {
                    Text(self.getStatusName(self.model.status))
                    Spacer()
                }
                        .padding()
            }
            TitledContainer("Start Time:") {
                VStack {
                    if !StringUtils.isEmptyOrNil(self.model.approvedStartDate) && !StringUtils.isEmptyOrNil(self.model.approvedStartTime) {
                        HStack {
                            Text(self.model.approvedStartDate!)
                            Spacer()
                        }
                        HStack {
                            Text(self.model.approvedStartTime!)
                            Spacer()
                        }
                    } else {
                        HStack {
                            Text(self.model.requestedStartDate)
                            Spacer()
                        }
                        HStack {
                            Text(self.model.requestedStartTime)
                            Spacer()
                        }
                    }
                }
                        .padding()
            }
        }
                .padding(.top, 10)
    }
}
