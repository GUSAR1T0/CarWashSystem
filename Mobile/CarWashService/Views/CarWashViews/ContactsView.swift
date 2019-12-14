//
//  ContactsView.swift
//  CarWashService
//
//  Created by Anna Boykova on 15.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ContactsView: View {
    var contacts: [(ContactModel)]
    
    var body: some View {
        List(contacts){ contact in
            ContactItemView(contact: contact)
        }
    }
}
