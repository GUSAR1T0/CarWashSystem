//
//  OrderRow.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct OrderRow: View {
     @EnvironmentObject private var lookupStorage: LookupStorage
    var model: OrderModel

    var body: some View {
        VStack(alignment: .leading) {
            HStack(alignment: .top) {
                Text(self.model.carWashName)
                        .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                        .font(.system(size: 28, weight: .bold))
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.bottom, 10)
                Spacer()
                TitledContainer("Status:") {
                    Text(self.model.orderStatus.status)
                }
            }
            Divider().background(ApplicationColor.Primary.toColor())
            Spacer()
            VStack {
                Text(self.lookupStorage.clientLookupModel?.getModelName(id: model.car.modelId) ?? "Unknown car")
                    .font(.system(size: 28, weight: .bold))
                    .frame(minWidth: 0, maxWidth: .infinity)
                Text(model.car.governmentPlate)
                    .frame(minWidth: 0, maxWidth: .infinity)
                    .padding(.top, 10)
            }
            Divider().background(ApplicationColor.Primary.toColor())
            Spacer()
            Text("Start time")
                .bold()
                .frame(minWidth: 0, maxWidth: .infinity, alignment: .center)
            Divider().background(ApplicationColor.Primary.toColor())
            HStack() {
                TitledContainer("Requested:") {
                    Text(self.model.startTime.requestedTime)
                }
                Spacer()
                Divider().background(ApplicationColor.Primary.toColor())
                Spacer()
                TitledContainer("Approved:") {
                    Text(!StringUtils.isEmptyOrNil(self.model.startTime.approvedTime)
                        ? self.model.startTime.approvedTime! : "—")
                }
            }
        }
    }
}
