//
//  MainView.swift
//  CarWashService
//
//  Created by Anna Boykova on 23/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct MainView: View {
    @State private var selection = 1

    var body: some View {
        TabView(selection: $selection) {
            ClientProfileView()
                    .tabItem {
                        Image(systemName: "person.fill")
                        Text("Profile")
                    }.tag(0)
            CarWashesView()
                    .tabItem {
                        Image(systemName: "wand.and.rays")
                        Text("Car Washes")
                    }.tag(1)
            AppointmentsView()
                    .tabItem {
                        Image(systemName: "rectangle.stack.badge.person.crop")
                        Text("Appointments")
                    }.tag(2)
        }.accentColor(ApplicationColor.Primary.toColor())
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
