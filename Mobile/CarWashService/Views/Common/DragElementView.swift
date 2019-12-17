//
// Created by Roman Mashenkin on 18.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct DragElementView: View {
    var body: some View {
        VStack(spacing: 0) {
            Spacer()
            RoundedRectangle(cornerRadius: CGFloat(5.0) / 2.0)
                    .frame(width: 40, height: 5)
                    .foregroundColor(ApplicationColor.DarkGray.toRGB())
            Spacer()
        }
                .frame(height: 30)
    }
}
