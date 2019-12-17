//
//  ServiceItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServiceItemView: View {
    let service: CarWashServiceModel

    var body: some View {
        VStack {
            HStack {
                Text(self.service.serviceName)
                        .font(.system(size: 24, weight: .bold))
                        .padding()
                Spacer()
            }
            TitledContainer(ServiceTitle.Description) {
                Text(!StringUtils.isEmptyOrNil(self.service.description) ? self.service.description! : "—")
                        .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                        .font(.subheadline)
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.top, 10)
                        .padding(.leading, 15)
                        .padding(.trailing, 15)
            }
            HStack {
                Spacer()
                HStack {
                    Image(systemName: "rublesign.circle.fill")
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                    Text(String(format: "%.2f", self.service.price))
                }
                        .frame(height: 30)
                Spacer()
                HStack {
                    Image(systemName: "timer")
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                    Text(self.service.duration)
                }
                        .frame(height: 30)
                Spacer()
                HStack {
                    Image(systemName: self.service.isAvailable ? ServiceStatusImage.Active : ServiceStatusImage.NotActive)
                            .foregroundColor(self.service.isAvailable ? ServiceStatusColor.Active : ServiceStatusColor.NotActive)
                    Text(self.service.isAvailable ? ServiceStatusTitle.Active : ServiceStatusTitle.NotActive)
                }
                Spacer()
            }
                    .padding()
            DividerView()
        }
    }
}
