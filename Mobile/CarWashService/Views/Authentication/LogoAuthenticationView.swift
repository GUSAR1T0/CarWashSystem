//
//  LogoView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct LogoAuthenticationView: View {
    var subTitle: String

    var body: some View {
        VStack(alignment: .center) {
            Image(systemName: "sun.max.fill")
                    .foregroundColor(ApplicationColor.Primary.toRGB())
            Image(systemName: "car.fill")
                    .foregroundColor(ApplicationColor.Primary.toRGB())
            Text(ApplicationText.LogoTitle)
                    .font(.system(size: 32, weight: .bold))
                    .foregroundColor(ApplicationColor.Primary.toRGB())
            Text(subTitle)
                    .font(.subheadline)
        }
                .padding()
                .edgesIgnoringSafeArea(.top)
    }
}

