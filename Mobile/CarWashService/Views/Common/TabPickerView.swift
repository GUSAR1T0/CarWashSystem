//
//  TabPickerView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct TabPickerView<Content: View>: View {
    var selection: Binding<Int>
    var views: [(name: String, view: Content)]

    var body: some View {
        VStack {
            Picker(selection: self.selection, label: Text("TabPicker")) {
                ForEach(0..<self.views.count) {
                    Text(Array(self.views)[$0].name)
                }
            }
            .pickerStyle(SegmentedPickerStyle())
            Array(self.views)[self.selection.wrappedValue].view
        }
    }
}
