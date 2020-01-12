//
//  SignInView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignInView: View {
    @EnvironmentObject private var authenticationStorage: AuthenticationStorage
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var selection: Int? = nil

    private let accountController = AccountController()

    var body: some View {
        NavigationView {
            VStack {
                LogoAuthenticationView(subTitle: AuthenticationViewText.SignInTitleText)
                        .padding()
                TextField(AuthenticationFieldName.Email, text: $emailAddress)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .autocapitalization(.none)
                        .padding()
                SecureField(AuthenticationFieldName.Password, text: $password)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .autocapitalization(.none)
                        .padding()
                HStack {
                    Spacer()
                    Button(action: {
                        // TODO: Validate form
                        let model = ClientSignInModel(email: self.emailAddress, password: self.password)
                        let clientProfile = self.accountController.signIn(model)

                        if clientProfile != nil {
                            self.lookupStorage.load()
                        }

                        self.authenticationStorage.isAuthenticated = clientProfile != nil
                        self.authenticationStorage.clientAuthenticationProfile = clientProfile
                    }) {
                        Text(AuthenticationViewText.SignInButtonText)
                                .bold()
                                .padding()
                                .background(ApplicationColor.Primary.toColor())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                }.padding()
                Spacer()
                HStack {
                    Text(AuthenticationViewText.QuestionAboutAccountExist)
                    NavigationLink(destination: SignUpView(), tag: 1, selection: self.$selection) {
                        Button(action: {
                            self.selection = 1
                        }) {
                            Text(AuthenticationViewText.SignUpButtonText)
                                    .underline()
                                    .bold()
                                    .foregroundColor(ApplicationColor.Primary.toColor())
                        }
                    }
                }
                        .padding()
                        .padding(.bottom, 15)
            }.padding()
        }
    }
}


struct SignInView_Previews: PreviewProvider {
    static var previews: some View {
        SignInView()
    }
}
