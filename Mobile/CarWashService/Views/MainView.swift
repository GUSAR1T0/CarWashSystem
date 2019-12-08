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
    var fullName: String {
        get {
            (storage.clientProfile?.firstName ?? "") + " " + (storage.clientProfile?.lastName ?? "")
        }
    }
    @State private var firstName = ""
    @State private var lastName = "test"
    @State private var emailAddress = ""
    @State private var phone = ""
    @State private var birthday = Date()
    @State private var birthdayText = ""
    @State private var flag = false

    var body: some View {
        TabView {
            VStack{
                TitledContainer(ClientProfileFieldTitle.FirstName) {
                    TextField(ClientProfileFieldName.FirstName, text: self.$firstName)
                        .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
                }
                TitledContainer(ClientProfileFieldTitle.LastName) {
                    TextField(ClientProfileFieldName.LastName, text: self.$lastName)
                        .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
                }
                TitledContainer(ClientProfileFieldTitle.Birthday) {
                    DatePicker("Birthday", selection: self.$birthday, displayedComponents: .date).labelsHidden()
                }
                TitledContainer(ClientProfileFieldTitle.Email) {
                    TextField(ClientProfileFieldName.Email, text: self.$emailAddress)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .autocapitalization(.none)
                        .padding(.horizontal)
                }
                TitledContainer(ClientProfileFieldTitle.Phone) {
                    TextField(ClientProfileFieldName.Phone, text: self.$phone)
                        .textFieldStyle(RoundedBorderTextFieldStyle()).padding(.horizontal)
                }
                HStack {
                    Spacer()
                    Button(action: {
                         //TODO: Save
                    }) {
                        Text("Save")
                                .bold()
                                .padding()
                                .background(ApplicationColor.Primary.toRGB())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                }.padding()
            }
                    .tabItem {
                        Image(systemName: "person.fill")
                        Text("Profile")
                    }.tag(0)
            VStack {
                Button(action: {
                    let service = HttpClientService()
                    try! service.delete(endpoint: Requests.SignOut)
                    self.storage.isAuthenticated = false
                    self.storage.clientProfile = nil
                }) {
                    Text("Log out from account \"\(self.fullName)\"")
                            .bold()
                            .padding()
                            .background(ApplicationColor.Primary.toRGB())
                            .cornerRadius(5)
                            .foregroundColor(.white)
                            .padding(10)
                }
            }
                    .tabItem {
                        Image(systemName: "car.fill")
                        Text("Car Wash")
                    }.tag(1)
        }.accentColor(ApplicationColor.Primary.toRGB())
    }
    
    func endEditing() {
        //UIApplication.shared.sendAction(#selector(UIResponder.resignFirstResponder), to: nil, from: nil, for: nil)

        let keyWindow = UIApplication.shared.connectedScenes
                               .filter({$0.activationState == .foregroundActive})
                               .map({$0 as? UIWindowScene})
                               .compactMap({$0})
                               .first?.windows
                               .filter({$0.isKeyWindow}).first
                       keyWindow?.endEditing(true)

    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView().environmentObject(Storage())
    }
}
