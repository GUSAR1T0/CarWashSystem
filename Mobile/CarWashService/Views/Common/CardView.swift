//
// Created by Roman Mashenkin on 10.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CardView<Content>: View where Content: View {
    var viewer: () -> Content

    @inlinable public init(@ViewBuilder viewer: @escaping () -> Content) {
        self.viewer = viewer
    }

    var body: some View {
        VStack {
            viewer()
                    .layoutPriority(100)
                    .padding()
        }
                .cornerRadius(10)
                .overlay(
                        RoundedRectangle(cornerRadius: 10)
                                .stroke(Color(.sRGB, red: 150 / 255, green: 150 / 255, blue: 150 / 255, opacity: 0.5), lineWidth: 1)
                ).padding([.top, .horizontal])
    }
}
