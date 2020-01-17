//
// Created by Roman Mashenkin on 12.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct OrdersView: View {
    @State private var orderList = [OrderModel](arrayLiteral: OrderModel(id: 0, carWashName: "test", car: ClientCarModel(id: 1, modelId: 1, governmentPlate: "test"), startTime: OrderStartTimeModel(requestedTime: "test", approvedTime: "test"), orderStatus: OrderStatus(statusCode: 1)), OrderModel(id: 1, carWashName: "test2", car: ClientCarModel(id: 1, modelId: 1, governmentPlate: "test"), startTime: OrderStartTimeModel(requestedTime: "test", approvedTime: "test"), orderStatus: OrderStatus(statusCode: 2)))

    var body: some View {
        NavigationView {
            VStack {
                if !self.orderList.isEmpty {
                    List(self.orderList) {
                        (order: OrderModel) in
                            NavigationLink(destination: OrderItemView()) {
                                OrderRow(model: order)
                            }
                    }
                }
                else {
                    EmptyView()
                }
            }
                .onAppear {
                        //self.orderList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
                    .navigationBarTitle(Text("Orders"))
        }
    }

}
