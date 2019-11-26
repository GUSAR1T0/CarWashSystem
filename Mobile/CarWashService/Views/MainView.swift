//
//  SignUpView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct MainView: View {
    @EnvironmentObject private var storage: Storage

    var body: some View {
        VStack {
            Button(action: {
                let service = HttpClientService()
                try! service.delete(endpoint: Requests.SignOut)
                self.storage.isAuthenticated = false
            }) {
                Text("Log out")
                        .bold()
                        .padding()
                        .background(ApplicationColor.Primary.toRGB())
                        .cornerRadius(5)
                        .foregroundColor(.white)
                        .padding(10)
            }
        }
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
