//
//  LabelTextField.swift
//  CarWashService
//
//  Created by Anna Boykova on 08.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct TitledContainer<Content>: View where Content : View {
    var label: String
//    var placeHolder: String
    var viewer: () -> Content
    
    @inlinable public init(_ label: String, @ViewBuilder viewer: @escaping () -> Content) {
        self.label = label
        self.viewer = viewer
    }

    var body: some View {
        VStack {
            HStack {
                Text(label)
                    .font(.body).bold().foregroundColor(ApplicationColor.Primary.toRGB())
                Spacer()
            }.padding(.horizontal)
            viewer()
//            TextField(placeHolder, text: textHolder)
//                .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
        }
    }
}
