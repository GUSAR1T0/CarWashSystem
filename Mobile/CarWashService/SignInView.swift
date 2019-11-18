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
    
    var body: some View {
        NavigationView {
            VStack{
                LogoView(subTitle: SignInTitleStr)
                TextField("johnnyappleseed@apple.com", text: $emailAddress)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding()
                SecureField("Password", text: $password)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding()
                HStack{
                    Spacer()
                    Button(action: {
                        //TODO: Add sign in action
                    }) {
                        Text("Sign In")
                            .bold()
                            .padding()
                            .background(PrimaryColor)
                            .cornerRadius(5)
                            .foregroundColor(.white)
                            .padding(10)
                        }
                }.padding()
                Spacer()
                HStack{
                    Text("Don't have an account?")
                    NavigationLink(destination: SignUpView(), tag: 1, selection: $selection) {
                        Button(action: {
                            self.selection = 1
                        }) {
                            Text("Create").underline().bold().foregroundColor(.black)
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
