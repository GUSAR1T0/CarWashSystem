//
//  ServiceItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServiceItemView: View {
    var service: ServiceModel
    
    var body: some View {
        ScrollView {
            VStack {
            TitledContainer(ServiceTitles.Name) {
                Text(self.service.name)
                    .font(.subheadline)
                    .padding()
            }
            TitledContainer(ServiceTitles.Description) {
                Text(self.service.description)
                    .font(.subheadline)
                    .padding()
            }
            TitledContainer(ServiceTitles.Price) {
                HStack {
                    Text(String(self.service.price))
                        .font(.subheadline)
                        .padding()
                    Image(systemName: "rublesign.circle.fill").foregroundColor(ApplicationColor.Primary.toRGB())
                }
            }
            TitledContainer(ServiceTitles.Duration) {
                Text(self.service.duration)
                    .font(.subheadline)
                    .padding()
            }
        }
        }}
}
