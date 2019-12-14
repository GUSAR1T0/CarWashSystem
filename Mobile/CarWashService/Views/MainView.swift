//
//  MainView.swift
//  CarWashService
//
//  Created by Anna Boykova on 23/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct MainView: View {
    @State private var selection = 2

    var body: some View {
        TabView(selection: $selection) {
            ClientProfileView()
                    .tabItem {
                        Image(systemName: "person.fill")
                        Text("Profile")
                    }.tag(0)
            ClientCarsView()
                    .tabItem {
                        Image(systemName: "car.fill")
                        Text("Cars")
                    }.tag(1)
            CarWashesView()
                    .tabItem {
                        Image(systemName: "wand.and.rays")
                        Text("Car Washes")
                    }.tag(2)
        }.accentColor(ApplicationColor.Primary.toRGB())
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
