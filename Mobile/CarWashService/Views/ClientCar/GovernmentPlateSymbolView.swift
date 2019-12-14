//
// Created by Roman Mashenkin on 13.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct GovernmentPlateSymbolView: View {
    var selection: Binding<String>
    @State var symbols: [String] = []
    @State var width: CGFloat = .infinity

    var body: some View {
        Picker(selection: selection, label: Text("GPSV")) {
            ForEach(self.symbols, id: \.self) { symbol in
                Text(symbol)
                        .font(
                                .system(
                                        size: self.selection.wrappedValue == symbol ? 22 : 18,
                                        weight: self.selection.wrappedValue == symbol ? .bold : .regular
                                )
                        )
            }
        }
                .frame(maxWidth: width)
                .clipped()
                .labelsHidden()
    }
}
