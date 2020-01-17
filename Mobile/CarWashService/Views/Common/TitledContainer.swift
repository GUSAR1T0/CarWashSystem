//
//  TitledContainer.swift
//  CarWashService
//
//  Created by Anna Boykova on 08.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct TitledContainer<Content>: View where Content: View {
    var label: String
    var font: Font
    var viewer: () -> Content

    @inlinable public init(_ label: String, font: Font = .body, @ViewBuilder viewer: @escaping () -> Content) {
        self.label = label
        self.font = font
        self.viewer = viewer
    }

    var body: some View {
        VStack {
            HStack {
                Text(label)
                        .font(font)
                        .bold()
                        .foregroundColor(ApplicationColor.Primary.toColor())
                Spacer()
            }.padding(.horizontal)
            viewer()
        }
    }
}
