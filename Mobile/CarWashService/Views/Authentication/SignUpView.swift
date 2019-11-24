//
//  SignUpView.swift
//  CarWashService
//
//  Created by Anna Boykova on 18/11/2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SignUpView: View {
    @EnvironmentObject var storage: Storage
    @State private var firstName = ""
    @State private var lastName = ""
    @State private var emailAddress = ""
    @State private var password = ""
    @State private var confirmPassword = ""
//    @State private var googleAuthenticationModal: Bool = false
    @State private var vkAuthenticationModal: Bool = false

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

                    let service = HttpClientService()
                    let model = ClientSignUpModel(email: self.emailAddress, password: self.password, firstName: self.firstName, lastName: self.lastName)

                    var flag = false
                    let semaphore = DispatchSemaphore(value: 0)
                    try! service.post(endpoint: Requests.SignUp, request: model, success: { (response: ClientProfileModel) in
                        flag = true
                        semaphore.signal()
                    }, error: { error in
                        if let error = error.response {
                            print("ERROR: \(error.message)")
                        } else if let error = error.httpClientError {
                            print("ERROR: \(error)")
                        }
                        semaphore.signal()
                    })
                    semaphore.wait()
                    self.storage.isAuthenticated = flag
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
//            Spacer()
//            VStack {
//                Text(AuthenticationViewText.QuestionAboutSocialAuthorization).padding(.bottom, 10)
//                HStack {
//                    Button(action: {
//                        self.googleAuthenticationModal = true
//                    }) {
//                        Text("Google")
//                                .bold()
//                                .padding()
//                                .frame(minWidth: 0, maxWidth: .infinity)
//                                .background(ApplicationColor.Primary.toRGB())
//                                .cornerRadius(5)
//                                .foregroundColor(.white)
//                                .padding(10)
//                    }.sheet(isPresented: $googleAuthenticationModal) {
//                        SafariView(url: "\(ServerConfiguration.host)\(Requests.InitializeExternalSignIn)", schema: 1)
//                    }
//                    Button(action: {
//                        self.vkAuthenticationModal = true
//                    }) {
//                        Text("VK")
//                                .bold()
//                                .padding()
//                                .frame(minWidth: 0, maxWidth: .infinity)
//                                .background(ApplicationColor.Primary.toRGB())
//                                .cornerRadius(5)
//                                .foregroundColor(.white)
//                                .padding(10)
//                    }.sheet(isPresented: $vkAuthenticationModal) {
//                        ExternalSignInWebView(url: "\(ServerConfiguration.host)\(Requests.InitializeExternalSignIn)", schema: 2)
//                    }
//                }
//            }.padding()
        }.padding()
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
