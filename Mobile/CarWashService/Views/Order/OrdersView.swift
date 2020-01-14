//
// Created by Roman Mashenkin on 12.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct OrdersView: View {
    @State private var orderList = [Int]()

    var body: some View {
        NavigationView {
            VStack {
                if !self.orderList.isEmpty {
                    List {
                        ForEach(self.orderList, id: \.self) { index in
                            Text("\(index)")
                        }
                    }
                } else {
                    EmptyView()
                }
            }
                    .onAppear {
//                        self.orderList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                    }
                    .navigationBarTitle(Text("Orders"))
        }
    }
}
