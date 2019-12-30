//
// Created by Roman Mashenkin on 16.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct SearchView: View {
    var search: Binding<String>

    var body: some View {
        HStack {
            Image(systemName: "magnifyingglass")
            TextField("Search", text: search)
                    .foregroundColor(.primary)
            Button(action: {
                self.search.wrappedValue = ""
            }) {
                Image(systemName: "xmark.circle.fill")
                        .opacity(search.wrappedValue == "" ? 0 : 1)
            }
        }
                .padding(EdgeInsets(top: 8, leading: 6, bottom: 8, trailing: 6))
                .foregroundColor(.secondary)
                .background(Color(.secondarySystemBackground))
                .cornerRadius(10.0)
                .padding(.horizontal)
    }
}
