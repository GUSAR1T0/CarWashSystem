//
// Created by Roman Mashenkin on 11.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarEditView: View {
    var typeOfAction: String

    var body: some View {
        NavigationView {
            ScrollView {
                VStack {
                    EmptyView()
                }.padding(.top, 15)
            }
                    .animation(.none)
                    .navigationBarTitle(Text(typeOfAction))
        }
    }
}
