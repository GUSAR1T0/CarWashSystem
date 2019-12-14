//
//  CarWashRow.swift
//  CarWashService
//
//  Created by Anna Boykova on 16.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashRow: View {
    var name: String
    var address: String
    var isOpen: Bool
    
    @inlinable public init(_ name: String, address: String, isOpen: Bool) {
        self.name = name
        self.address = address
        self.isOpen = isOpen
    }
    
    var body: some View {
        VStack(alignment: .leading) {
            Text(name)
                .font(.system(size: 24, weight: .bold))
                .padding(.bottom, 5)
            HStack(alignment: .top) {
                Text(address)
                    .font(.subheadline)
                Spacer()
                HStack {
                    Image(systemName: self.isOpen ? StatusImages.Open : StatusImages.Close)
                        .foregroundColor(self.isOpen ? StatusColors.Open : StatusColors.Close)
                    Text(self.isOpen ? StatusTitels.Open : StatusTitels.Close)
                        .font(.system(size: 24, weight: .bold))
                        .foregroundColor(ApplicationColor.Primary.toRGB())
                }
            }
        }
        .padding()
    }
}
