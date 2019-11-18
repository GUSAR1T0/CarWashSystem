//
//  LogoView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct LogoView: View {
    var subTitle : String
    
    var body: some View {
        VStack(alignment: .center){
            Image(systemName: "sun.max.fill").foregroundColor(PrimaryColor)
            Image(systemName: "car.fill").foregroundColor(PrimaryColor)
            Text(AppLogoStr)
                .font(.largeTitle).bold().foregroundColor(PrimaryColor)
            Text(subTitle)
                .font(.subheadline)
        }
            .padding()
            .edgesIgnoringSafeArea(.top)
    }
}

