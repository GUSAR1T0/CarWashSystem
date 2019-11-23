//
//  SignUpView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignUpView: View {
    @State private var firstName = ""
    @State private var lastName = ""
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var confirmPassword = ""

    private func CreateTextFieldForSignUpForm(fieldName: String, textField: Binding<String>) -> some View {
        TextField(fieldName, text: textField)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
    }

    var body: some View {
        VStack {
            LogoView(subTitle: AuthenticationViewText.SignUpTitleText).edgesIgnoringSafeArea(.top)
            CreateTextFieldForSignUpForm(fieldName: AuthenticationFieldName.FirstName, textField: $firstName)
            CreateTextFieldForSignUpForm(fieldName: AuthenticationFieldName.LastName, textField: $lastName)
            CreateTextFieldForSignUpForm(fieldName: AuthenticationFieldName.Email, textField: $emailAddress)
            CreateTextFieldForSignUpForm(fieldName: AuthenticationFieldName.Password, textField: $password)
            CreateTextFieldForSignUpForm(fieldName: AuthenticationFieldName.PasswordConfirmation, textField: $confirmPassword)
            HStack {
                Spacer()
                Button(action: {
                    // TODO: Add sign up action
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
            Text(AuthenticationViewText.QuestionAboutSocialAuthorization)
        }.padding()
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
