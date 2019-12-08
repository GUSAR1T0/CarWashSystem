//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct HeaderTitle: View {
    var title: String

    var body: some View {
        HStack {
            Text(title)
                    .padding(.leading, 30)
                    .padding(.trailing, 10)
                    .font(.system(size: 40, weight: .bold))
            Spacer()
        }
    }
}
