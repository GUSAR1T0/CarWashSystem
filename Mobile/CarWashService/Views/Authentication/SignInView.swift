//
//  SignInView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignInView: View {
    @EnvironmentObject var storage: Storage
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var selection: Int? = nil
    let accountController = AccountController()

    var body: some View {
        NavigationView {
            VStack {
                LogoAuthenticationView(subTitle: AuthenticationViewText.SignInTitleText)
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
                        self.storage.isAuthenticated = clientProfile != nil
                        self.storage.clientProfile = clientProfile
                    }) {
                        Text(AuthenticationViewText.SignInButtonText)
                                .bold()
                                .padding()
                                .background(ApplicationColor.Primary.toRGB())
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
                                    .foregroundColor(ApplicationColor.Primary.toRGB())
                        }
                    }
                }.padding()
            }.padding()
        }
    }
}


struct SignInView_Previews: PreviewProvider {
    static var previews: some View {
        SignInView()
    }
}
