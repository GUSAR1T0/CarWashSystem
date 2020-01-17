//
//  ContactsView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ContactsView: View {
    @Binding var email: String?
    @Binding var phone: String?

    var body: some View {
        ScrollView {
            VStack {
                HStack {
                    Image(systemName: "envelope.circle.fill")
                            .foregroundColor(ApplicationColor.Primary.toColor())
                    Button(action: {
                        if !StringUtils.isEmptyOrNil(self.email) {
                            UIApplication.shared.open(URL(string: "mailto:\(self.email!)")!)
                        }
                    }) {
                        Text(!StringUtils.isEmptyOrNil(self.email) ? self.email! : "—")
                                .underline()
                                .bold()
                                .foregroundColor(ApplicationColor.Primary.toColor())
                    }
                    Spacer()
                }
                        .padding()
                HStack {
                    Image(systemName: "phone.circle.fill")
                            .foregroundColor(ApplicationColor.Primary.toColor())
                    Button(action: {
                        if !StringUtils.isEmptyOrNil(self.phone) {
                            var phone = self.phone!
                            phone.removeAll(where: { char in char == " " || char == "(" || char == ")" })
                            UIApplication.shared.open(URL(string: "tel://\(phone)")!)
                        }
                    }) {
                        Text(!StringUtils.isEmptyOrNil(self.phone) ? self.phone! : "—")
                                .underline()
                                .bold()
                                .foregroundColor(ApplicationColor.Primary.toColor())
                    }
                    Spacer()
                }
                        .padding()
            }
        }
    }
}