//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientProfileView: View {
    @EnvironmentObject private var authenticationStorage: AuthenticationStorage

    @State private var firstName = ""
    @State private var lastName = "test"
    @State private var emailAddress = ""
    @State private var phone = ""
    @State private var birthday = Date()

    @State private var logoutAction = false

    private let containerPaddingValue: CGFloat = 10

    var body: some View {
        ScrollView {
            VStack {
                HeaderTitle(title: "General Info")
                        .padding(.top, 30)
                        .padding(.bottom, 30)
                TitledContainer(ClientProfileFieldTitle.FirstName) {
                    TextField(ClientProfileFieldName.FirstName, text: self.$firstName)
                            .textFieldStyle(RoundedBorderTextFieldStyle())
                            .padding(.horizontal)
                }
                        .padding(.bottom, containerPaddingValue)
                TitledContainer(ClientProfileFieldTitle.LastName) {
                    TextField(ClientProfileFieldName.LastName, text: self.$lastName)
                            .textFieldStyle(RoundedBorderTextFieldStyle())
                            .padding(.horizontal)
                }
                        .padding(.top, containerPaddingValue)
                        .padding(.bottom, containerPaddingValue)
                TitledContainer(ClientProfileFieldTitle.Birthday) {
                    DatePicker(ClientProfileFieldTitle.Birthday, selection: self.$birthday, displayedComponents: .date)
                            .labelsHidden()
                }
                        .padding(.top, containerPaddingValue)
                        .padding(.bottom, containerPaddingValue)
                TitledContainer(ClientProfileFieldTitle.Email) {
                    TextField(ClientProfileFieldName.Email, text: self.$emailAddress)
                            .textFieldStyle(RoundedBorderTextFieldStyle())
                            .autocapitalization(.none)
                            .padding(.horizontal)
                }
                        .padding(.top, containerPaddingValue)
                        .padding(.bottom, containerPaddingValue)
                TitledContainer(ClientProfileFieldTitle.Phone) {
                    TextField(ClientProfileFieldName.Phone, text: self.$phone)
                            .textFieldStyle(RoundedBorderTextFieldStyle())
                            .padding(.horizontal)
                }
                        .padding(.top, containerPaddingValue)
                HStack {
                    Button(action: {
                        self.logoutAction.toggle()
                    }) {
                        Text("Log out")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.Danger.toRGB())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }.actionSheet(isPresented: $logoutAction) {
                        ActionSheet(title: Text("Are you sure that you want to log out?"), buttons: [
                            .default(Text("Submit")) {
                                let service = HttpClientService()
                                try! service.delete(endpoint: Requests.SignOut)
                                self.authenticationStorage.isAuthenticated = false
                                self.authenticationStorage.clientAuthenticationProfile = nil
                            },
                            .cancel()
                        ])
                    }
                    Button(action: {
                        // TODO: Save
                    }) {
                        Text("Save")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.Primary.toRGB())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                }.padding()
            }
        }
    }
}
