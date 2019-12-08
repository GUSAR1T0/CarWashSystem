//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientCarsView: View {
    var body: some View {
        ScrollView {
            VStack {
                HeaderTitle(title: "Cars")
                        .padding(.top, 10)
                        .padding(.bottom, 30)
                Spacer()
                Text("Cars...")
                Spacer()
            }
        }
    }
}
