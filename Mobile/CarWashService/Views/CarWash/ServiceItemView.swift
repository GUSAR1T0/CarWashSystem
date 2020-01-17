//
//  ServiceItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServiceItemView: View {
    @State var selectable: Bool
    @Binding var selectedServiceIds: [Int]
    var service: CarWashServiceModel

    var body: some View {
        let predicate: (Int) -> Bool = { id in id == self.service.id }
        return VStack {
            HStack {
                Text(self.service.serviceName)
                        .font(.system(size: 24, weight: .bold))
                        .padding()
                Spacer()
                if self.selectable {
                    if self.selectedServiceIds.contains(where: predicate) {
                        Image(systemName: "checkmark.square.fill")
                                .foregroundColor(ApplicationColor.Primary.toColor())
                    } else {
                        Image(systemName: "square")
                                .foregroundColor(ApplicationColor.Primary.toColor())
                    }
                }
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
                            .foregroundColor(ApplicationColor.Primary.toColor())
                    Text(String(format: "%.2f", self.service.price))
                }
                        .frame(height: 30)
                Spacer()
                HStack {
                    Image(systemName: "timer")
                            .foregroundColor(ApplicationColor.Primary.toColor())
                    Text(self.service.duration)
                }
                        .frame(height: 30)
                Spacer()
                if !self.selectable {
                    HStack {
                        Image(systemName: self.service.isAvailable ? ServiceStatusImage.Available : ServiceStatusImage.NotAvailable)
                                .foregroundColor(self.service.isAvailable ? ServiceStatusColor.Available : ServiceStatusColor.NotAvailable)
                        Text(self.service.isAvailable ? ServiceStatusTitle.Available : ServiceStatusTitle.NotAvailable)
                    }
                    Spacer()
                }
            }
                    .padding()
        }
                .contentShape(Rectangle())
                .gesture(
                        TapGesture()
                                .onEnded { _ in
                                    if self.selectedServiceIds.contains(where: predicate) {
                                        self.selectedServiceIds.removeAll(where: predicate)
                                    } else {
                                        self.selectedServiceIds.append(self.service.id)
                                    }
                                }
                )
    }
}
