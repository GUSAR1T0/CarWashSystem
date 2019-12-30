//
// Created by Roman Mashenkin on 16.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct FacilityTagView: View {
    let title: String
    let isAvailable: Bool

    var body: some View {
        GeometryReader { geometry in
            HStack {
                Spacer()
                Text(self.title.uppercased())
                        .font(.system(size: 18, weight: .bold))
                        .frame(width: geometry.size.width / 2, height: 30)
                        .foregroundColor(ApplicationColor.MiddleGray.toRGB())
                Spacer()
                Image(systemName: self.isAvailable ? "checkmark.circle.fill" : "xmark.circle.fill")
                        .padding()
                        .frame(width: geometry.size.width / 4, height: 30)
                        .background(self.isAvailable ? StatusColor.Open : StatusColor.Close)
                        .foregroundColor(.white)
                        .cornerRadius(5)
                Spacer()
            }
        }
    }
}
