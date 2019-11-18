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
    @State private var secondName = ""
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var confirmPassword = ""
    
    var body: some View {
        VStack{
            LogoView(subTitle: SignUpTitleStr).edgesIgnoringSafeArea(.top)
            TextField("John", text: $firstName)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
            TextField("Smith", text: $secondName)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
            TextField("johnnyappleseed@apple.com", text: $emailAddress)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
            SecureField("Password", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
            SecureField("Confirm password", text: $confirmPassword)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(5)
            HStack{
                Spacer()
                Button(action: {
                        //TODO: Add sign up action
                }) {
                    Text("Create")
                        .bold()
                        .padding()
                        .background(PrimaryColor)
                        .cornerRadius(5)
                        .foregroundColor(.white)
                        .padding(10)
                    }
            }.padding()
            Spacer()
            Text("Or create account using social media")
        }.padding()
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
