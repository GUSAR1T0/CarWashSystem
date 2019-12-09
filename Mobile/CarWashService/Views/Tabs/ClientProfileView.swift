//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientProfileView: View {
    private let containerPaddingValue: CGFloat = 10
    private let navigationBarPaddingValue: CGFloat = 15

    @EnvironmentObject private var authenticationStorage: AuthenticationStorage
    @State private var logoutAction = false
    @State private var isLoaded = false

    private var clientProfileController = ClientProfileController()
    @ObservedObject private var clientProfile = ClientProfileEntity()

    var body: some View {
        if !isLoaded {
            clientProfileController.getClientProfile(clientProfile, isLoaded: $isLoaded)
        }
        return NavigationView {
            ScrollView {
                if isLoaded {
                    VStack {
                        TitledContainer(ClientProfileFieldTitle.FirstName) {
                            TextField(ClientProfileFieldName.FirstName, text: self.$clientProfile.firstName)
                                    .textFieldStyle(RoundedBorderTextFieldStyle())
                                    .padding(.horizontal)
                        }
                                .padding(.bottom, containerPaddingValue)
                        TitledContainer(ClientProfileFieldTitle.LastName) {
                            TextField(ClientProfileFieldName.LastName, text: self.$clientProfile.lastName)
                                    .textFieldStyle(RoundedBorderTextFieldStyle())
                                    .padding(.horizontal)
                        }
                                .padding(.top, containerPaddingValue)
                                .padding(.bottom, containerPaddingValue)
                        TitledContainer(ClientProfileFieldTitle.Birthday) {
                            DateTextPickerView(ClientProfileFieldName.Birthday, selection: self.$clientProfile.birthday)
                                    .setRoundedBorderTextFieldStyle()
                                    .padding(.horizontal)
                        }
                                .padding(.top, containerPaddingValue)
                                .padding(.bottom, containerPaddingValue)
                        TitledContainer(ClientProfileFieldTitle.Email) {
                            TextField(ClientProfileFieldName.Email, text: self.$clientProfile.email)
                                    .textFieldStyle(RoundedBorderTextFieldStyle())
                                    .autocapitalization(.none)
                                    .padding(.horizontal)
                        }
                                .padding(.top, containerPaddingValue)
                                .padding(.bottom, containerPaddingValue)
                        TitledContainer(ClientProfileFieldTitle.Phone) {
                            TextField(ClientProfileFieldName.Phone, text: self.$clientProfile.phone)
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
                                // TODO: Validate form
                                self.clientProfileController.updateClientProfile(self.clientProfile)
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
                    }.padding(.top, navigationBarPaddingValue)
                } else {
                    VStack {
                        Spacer()
                        HStack {
                            Spacer()
                            Text("Loading...")
                                    .padding()
                                    .frame(minWidth: 0, maxWidth: .infinity)
                            Spacer()
                        }.padding()
                        Spacer()
                    }.padding(.top, navigationBarPaddingValue)
                }
            }
                    .animation(.none)
                    .navigationBarTitle(Text("General Info"))
        }
    }
}
