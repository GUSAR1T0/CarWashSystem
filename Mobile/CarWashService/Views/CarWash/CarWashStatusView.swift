//
// Created by Roman Mashenkin on 17.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashStatusView: View {
    var isOpen: Bool

    var body: some View {
        HStack {
            Image(systemName: self.isOpen ? StatusImage.Open : StatusImage.Close)
                    .foregroundColor(self.isOpen ? StatusColor.Open : StatusColor.Close)
            Text(self.isOpen ? StatusTitle.Open : StatusTitle.Close)
                    .font(.system(size: 14, weight: .bold))
                    .foregroundColor(ApplicationColor.Primary.toColor())
        }
    }
}
