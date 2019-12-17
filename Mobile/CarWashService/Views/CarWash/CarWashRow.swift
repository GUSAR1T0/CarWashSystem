//
//  CarWashRow.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashRow: View {
    var model: CarWashShortModel

    var body: some View {
        VStack(alignment: .leading) {
            HStack(alignment: .top) {
                Text(self.model.name)
                        .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                        .font(.system(size: 24, weight: .bold))
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.bottom, 10)
                Spacer()
                CarWashStatusView(isOpen: self.model.isOpen)
            }
            Text(self.model.location)
                    .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                    .font(.subheadline)
                    .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
        }
                .padding()
    }
}
