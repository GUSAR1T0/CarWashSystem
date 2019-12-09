//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientCarsView: View {
    var body: some View {
        NavigationView {
            ScrollView {
                VStack {
                    EmptyView()
                }.padding(.top, 15)
            }
                    .animation(.none)
                    .navigationBarTitle(Text("Cars"))
        }
    }
}
