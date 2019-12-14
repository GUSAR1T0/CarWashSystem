//
// Created by Roman Mashenkin on 10.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct LoadingView: View {
    var body: some View {
        VStack {
            Spacer()
            HStack {
                Spacer()
                Text("Loading...")
                        .bold()
                        .padding()
                        .frame(minWidth: 0, maxWidth: .infinity)
                Spacer()
            }.padding()
            Spacer()
        }.padding(.top, navigationBarPaddingValue)
    }
}
