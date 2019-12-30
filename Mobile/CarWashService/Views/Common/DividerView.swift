//
// Created by Roman Mashenkin on 16.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct DividerView: View {
    var body: some View {
        Rectangle()
                .stroke(ApplicationColor.LightGray.toRGB())
                .frame(height: 0.25)
                .padding(.leading, 15)
                .padding(.trailing, 15)
    }
}
