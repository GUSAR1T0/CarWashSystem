//
//  ServicesView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ServicesView: View {
    @State var selectable: Bool
    @Binding var selectedServiceIds: [Int]
    @Binding var services: [CarWashServiceModel]

    var body: some View {
        List(self.services.filter { service in !self.selectable || service.isAvailable == true }) { service in
            ServiceItemView(selectable: self.selectable, selectedServiceIds: self.$selectedServiceIds, service: service)
        }
    }
}
