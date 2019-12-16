//
//  ServiceRow.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServiceRow: View {
    var service: ServiceModel
    
    var body: some View {
        HStack {
            Text(self.service.serviceName)
                .font(.body)
                .padding()
            Spacer()
            Text(String(self.service.price))
                .font(.body)
                .padding()
            Image(systemName: "rublesign.circle.fill").foregroundColor(ApplicationColor.Primary.toRGB())
        }
    }
}
