//
//  SignInView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignInView: View {
    @State private var emailAddress = ""
    @State private var password = ""
    @State var selection: Int? = nil

    private func CreateTextFieldForSignInForm(fieldName: String, textField: Binding<String>) -> some View {
        TextField(fieldName, text: textField)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding()
    }

    var body: some View {
        NavigationView {
            VStack {
                LogoAuthenticationView(subTitle: AuthenticationViewText.SignInTitleText)
                CreateTextFieldForSignInForm(fieldName: AuthenticationFieldName.Email, textField: $emailAddress)
                CreateTextFieldForSignInForm(fieldName: AuthenticationFieldName.Password, textField: $password)
                HStack {
                    Spacer()
                    Button(action: {
                        // TODO: Add sign in action
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
                    NavigationLink(destination: SignUpView(), tag: 1, selection: $selection) {
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
