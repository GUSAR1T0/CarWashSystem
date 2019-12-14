//
//  ServicesView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServicesView: View {
    var services: [(ServiceModel)]
    
    var body: some View {
        NavigationView {
            List(services) { service in
                NavigationLink(destination: ServiceItemView(service: service)) {
                    ServiceRow(service: service)
                }
            }
        }
    }
}
