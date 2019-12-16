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
    var location: String
    var isOpen: Bool

    @inlinable public init(_ name: String, location: String, isOpen: Bool) {
        self.name = name
        self.location = location
        self.isOpen = isOpen
    }

    var body: some View {
        VStack(alignment: .leading) {
            HStack(alignment: .top) {
                Text(self.name)
                        .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                        .font(.system(size: 24, weight: .bold))
                        .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                        .padding(.bottom, 10)
                Spacer()
                HStack {
                    Image(systemName: self.isOpen ? StatusImage.Open : StatusImage.Close)
                            .foregroundColor(self.isOpen ? StatusColor.Open : StatusColor.Close)
                    Text(self.isOpen ? StatusTitle.Open : StatusTitle.Close)
                            .font(.system(size: 14, weight: .bold))
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                }
            }
            Text(self.location)
                    .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                    .font(.subheadline)
                    .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
        }
                .padding()
    }
}
