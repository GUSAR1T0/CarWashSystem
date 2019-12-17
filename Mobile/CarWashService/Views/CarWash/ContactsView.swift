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
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                    Text(!StringUtils.isEmptyOrNil(self.email) ? self.email! : "—")
                    Spacer()
                }
                        .padding()
                HStack {
                    Image(systemName: "phone.circle.fill")
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                    Text(!StringUtils.isEmptyOrNil(self.phone) ? self.phone! : "—")
                    Spacer()
                }
                        .padding()
            }
        }
    }
}
