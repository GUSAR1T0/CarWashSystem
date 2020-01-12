//
// Created by Roman Mashenkin on 12.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct OrderDateStatusView: View {
    var isDateActive: Bool

    var body: some View {
        HStack {
            Text("Order date and time validation:")
            Spacer()
            Image(systemName: self.isDateActive ? StatusImage.Open : StatusImage.Close)
                    .foregroundColor(self.isDateActive ? StatusColor.Open : StatusColor.Close)
            Text(self.isDateActive ? OrderStatusTitle.Passed : OrderStatusTitle.Failed)
                    .font(.system(size: 14, weight: .bold))
                    .foregroundColor(ApplicationColor.Primary.toColor())
        }
    }
}
