//
//  SignUpView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignUpView: View {
    @EnvironmentObject private var authenticationStorage: AuthenticationStorage
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var firstName = ""
    @State private var lastName = ""
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var confirmPassword = ""

    private let accountController = AccountController()

    var body: some View {
        VStack {
            LogoAuthenticationView(subTitle: AuthenticationViewText.SignUpTitleText).edgesIgnoringSafeArea(.top)
            TextField(AuthenticationFieldName.FirstName, text: $firstName)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding(5)
            TextField(AuthenticationFieldName.LastName, text: $lastName)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding(5)
            TextField(AuthenticationFieldName.Email, text: $emailAddress)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .autocapitalization(.none)
                    .padding(5)
            SecureField(AuthenticationFieldName.Password, text: $password)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .autocapitalization(.none)
                    .padding(5)
            SecureField(AuthenticationFieldName.PasswordConfirmation, text: $confirmPassword)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .autocapitalization(.none)
                    .padding(5)
            HStack {
                Spacer()
                Button(action: {
                    // TODO: Validate form
                    guard self.password == self.confirmPassword else {
                        return
                    }
                    let model = ClientSignUpModel(email: self.emailAddress, password: self.password, firstName: self.firstName, lastName: self.lastName)
                    let clientProfile = self.accountController.signUp(model)

                    if clientProfile != nil {
                        self.lookupStorage.load()
                    }

                    self.authenticationStorage.isAuthenticated = clientProfile != nil
                    self.authenticationStorage.clientAuthenticationProfile = clientProfile
                }) {
                    Text(AuthenticationViewText.SignUpButtonText)
                            .bold()
                            .padding()
                            .background(ApplicationColor.Primary.toRGB())
                            .cornerRadius(5)
                            .foregroundColor(.white)
                            .padding(10)
                }
            }.padding()
            Spacer()
            VStack {
                Text(AuthenticationViewText.QuestionAboutSocialAuthorization).padding(.bottom, 10)
                HStack {
                    Button(action: {
                        self.accountController.externalSignInThroughGoogle(UIApplication.shared.keyWindow, handler: { token in
                            let clientProfile = self.accountController.externalSignIn(token)

                            if clientProfile != nil {
                                self.lookupStorage.load()
                            }

                            self.authenticationStorage.isAuthenticated = clientProfile != nil
                            self.authenticationStorage.clientAuthenticationProfile = clientProfile
                        })
                    }) {
                        Text("Google")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.Primary.toRGB())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                    Button(action: {
                        self.accountController.externalSignInThroughVk(UIApplication.shared.keyWindow, handler: { token in
                            let clientProfile = self.accountController.externalSignIn(token)

                            if clientProfile != nil {
                                self.lookupStorage.load()
                            }

                            self.authenticationStorage.isAuthenticated = clientProfile != nil
                            self.authenticationStorage.clientAuthenticationProfile = clientProfile
                        })
                    }) {
                        Text("VK")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.Primary.toRGB())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                }
            }.padding()
        }.padding()
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
