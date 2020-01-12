//
// Created by Roman Mashenkin on 05.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashOrderServicesView: View {
    @Binding var selectedServiceIds: [Int]
    @Binding var services: [CarWashServiceModel]

    var body: some View {
        let list = self.services.filter { model in
            self.selectedServiceIds.contains(where: { id in model.id == id })
        }

        return VStack {
            if !list.isEmpty {
                ForEach(list) { service in
                    HStack {
                        Text(service.serviceName)
                                .font(.system(size: 22, weight: .light))
                        Spacer()
                        VStack {
                            HStack {
                                Spacer()
                                Image(systemName: "rublesign.circle.fill")
                                        .foregroundColor(ApplicationColor.Primary.toColor())
                                Text(String(format: "%.2f", service.price))
                                        .font(.system(size: 18, weight: .light))
                            }
                            HStack {
                                Spacer()
                                Image(systemName: "timer")
                                        .foregroundColor(ApplicationColor.Primary.toColor())
                                Text(service.duration)
                                        .font(.system(size: 18, weight: .light))
                            }
                        }
                    }
                            .padding()
                }
                Divider()
                        .padding(.leading, 15)
                HStack {
                    Text("Total")
                            .font(.system(size: 20, weight: .bold))
                    VStack {
                        HStack {
                            Spacer()
                            Image(systemName: "rublesign.circle.fill")
                                    .foregroundColor(ApplicationColor.Primary.toColor())
                            Text(String(format: "%.2f", list.map({ $0.price }).reduce(0, +)))
                                    .font(.system(size: 18, weight: .light))
                        }
                        HStack {
                            Spacer()
                            Image(systemName: "timer")
                                    .foregroundColor(ApplicationColor.Primary.toColor())
                            Text(DateTimeUtils.getTotalTime(times: list.map({ $0.duration })))
                                    .font(.system(size: 18, weight: .light))
                        }
                    }
                }
                        .padding()
            } else {
                Text("No chosen services")
                        .font(.system(size: 24, weight: .regular))
                        .padding()
            }
        }
    }
}
