//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import Combine

struct ClientProfileView: View {
    private let containerPaddingValue: CGFloat = 10

    @EnvironmentObject private var authenticationStorage: AuthenticationStorage
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var logoutAction = false
    @State private var isLoaded = false
    @State private var firstName: String = ""
    @State private var lastName: String = ""
    @State private var email: String = ""
    @State private var phone: String = ""
    @State private var birthday: String = ""

    private let accountController = AccountController()
    private let clientProfileController = ClientProfileController()

    private func prepareBirthday(_ birthday: String) -> String {
        let modified = birthday[..<birthday.index(birthday.startIndex, offsetBy: 10)]
        let date = CustomDateFormatter.formatTo(CustomDateFormatter.serverFormat, String(modified))
        return CustomDateFormatter.formatFrom(CustomDateFormatter.datePickerFormat, date)
    }

    private func prepareBirthday() -> String {
        let date = CustomDateFormatter.formatTo(CustomDateFormatter.datePickerFormat, birthday)
        return CustomDateFormatter.formatFrom(CustomDateFormatter.serverFormat, date)
    }

    private func fillFields(_ model: ClientProfileModel) {
        DispatchQueue.main.async {
            self.firstName = model.firstName
            self.lastName = model.lastName
            self.email = model.email ?? ""
            self.phone = model.phone ?? ""
            self.birthday = model.birthday == nil || model.birthday == "" ? "" : self.prepareBirthday(model.birthday!)
        }
    }

    private func fillModel() -> ClientProfileModel {
        ClientProfileModel(
                firstName: self.firstName,
                lastName: self.lastName,
                email: self.email == "" ? nil : self.email,
                phone: self.phone == "" ? nil : self.phone,
                birthday: self.birthday == "" ? nil : self.prepareBirthday()
        )
    }

    var body: some View {
        NavigationView {
            RefreshableScrollView(action: { completeRefresh in
                self.clientProfileController.getClientProfile(isLoaded: self.$isLoaded, completeRefresh: completeRefresh) { model in
                    self.fillFields(model)
                }
            }) {
                VStack {
                    TitledContainer(ClientProfileFieldTitle.FirstName) {
                        TextField(ClientProfileFieldName.FirstName, text: self.$firstName)
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .padding(.horizontal)
                    }
                            .padding(.bottom, self.containerPaddingValue)
                    TitledContainer(ClientProfileFieldTitle.LastName) {
                        TextField(ClientProfileFieldName.LastName, text: self.$lastName)
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .padding(.horizontal)
                    }
                            .padding(.top, self.containerPaddingValue)
                            .padding(.bottom, self.containerPaddingValue)
                    TitledContainer(ClientProfileFieldTitle.Birthday) {
                        if self.isLoaded { // Workaround to render custom view
                            DateTextPickerView(ClientProfileFieldName.Birthday, selection: self.$birthday)
                                    .setRoundedBorderTextFieldStyle()
                                    .padding(.horizontal)
                        } else {
                            DateTextPickerView(ClientProfileFieldName.Birthday, selection: self.$birthday)
                                    .setRoundedBorderTextFieldStyle()
                                    .padding(.horizontal)
                        }
                    }
                            .padding(.top, self.containerPaddingValue)
                            .padding(.bottom, self.containerPaddingValue)
                    TitledContainer(ClientProfileFieldTitle.Email) {
                        TextField(ClientProfileFieldName.Email, text: self.$email)
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .autocapitalization(.none)
                                .padding(.horizontal)
                    }
                            .padding(.top, self.containerPaddingValue)
                            .padding(.bottom, self.containerPaddingValue)
                    TitledContainer(ClientProfileFieldTitle.Phone) {
                        TextField(ClientProfileFieldName.Phone, text: self.$phone)
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .padding(.horizontal)
                    }
                            .padding(.top, self.containerPaddingValue)
                    HStack {
                        Button(action: {
                            self.logoutAction.toggle()
                        }) {
                            Text(ActionText.LogOutButtonText)
                                    .bold()
                                    .padding()
                                    .frame(minWidth: 0, maxWidth: .infinity)
                                    .background(ApplicationColor.Danger.toRGB())
                                    .cornerRadius(5)
                                    .foregroundColor(.white)
                                    .padding(10)
                        }.actionSheet(isPresented: self.$logoutAction) {
                            ActionSheet(title: Text(ClientProfileViewText.QuestionAboutLogOut), buttons: [
                                .default(Text(ActionText.SubmitButtonText)) {
                                    self.accountController.signOut()

                                    self.lookupStorage.reset()

                                    self.authenticationStorage.isAuthenticated = false
                                    self.authenticationStorage.clientAuthenticationProfile = nil
                                },
                                .cancel()
                            ])
                        }
                        Button(action: {
                            // TODO: Validate form
                            self.clientProfileController.updateClientProfile(self.fillModel())
                        }) {
                            Text(ActionText.SaveButtonText)
                                    .bold()
                                    .padding()
                                    .frame(minWidth: 0, maxWidth: .infinity)
                                    .background(ApplicationColor.Primary.toRGB())
                                    .cornerRadius(5)
                                    .foregroundColor(.white)
                                    .padding(10)
                        }
                    }.padding()
                }.padding(.top, self.navigationBarPaddingValue)
            }
                    .onAppear {
                        self.clientProfileController.getClientProfile(isLoaded: self.$isLoaded) { model in
                            self.fillFields(model)
                        }
                    }
                    .animation(.none)
                    .navigationBarTitle(Text("General Info"))
        }
    }
}
