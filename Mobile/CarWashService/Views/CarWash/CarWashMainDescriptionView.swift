//
// Created by Roman Mashenkin on 17.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashMainDescriptionView: View {
    @Binding var name: String
    @Binding var location: String
    @Binding var isOpen: Bool

    var body: some View {
        VStack(alignment: .leading) {
            HStack(alignment: .top) {
                Text(self.name)
                        .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                        .font(.system(size: 28, weight: .bold))
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.bottom, 10)
                Spacer()
                CarWashStatusView(isOpen: self.isOpen)
            }
            HStack {
                Spacer()
                Text(self.location)
                        .font(.subheadline)
                Spacer()
            }
        }
    }
}
