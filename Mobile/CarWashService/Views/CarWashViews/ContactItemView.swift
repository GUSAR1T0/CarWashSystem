//
//  ContactItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ContactItemView: View {
    var contact: ContactModel
    
    var body: some View {
        VStack(alignment: .leading) {
            HStack{
                Image(systemName: "mappin.circle.fill")
                    .foregroundColor(ApplicationColor.Primary.toRGB())
                Text(contact.location).frame(width: 350)
                Spacer()
            }.padding()
            HStack{
                Image(systemName: "envelope.circle.fill")
                    .foregroundColor(ApplicationColor.Primary.toRGB())
                Text(contact.email)
                Spacer()
            }.padding()
            HStack{
                Image(systemName: "phone.circle.fill")
                    .foregroundColor(ApplicationColor.Primary.toRGB())
                Text(contact.phone)
                Spacer()
            }.padding()
        }
    }
}
