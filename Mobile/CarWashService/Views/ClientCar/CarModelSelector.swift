//
// Created by Roman Mashenkin on 13.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarModelSelector: View {
    @EnvironmentObject private var lookupStorage: LookupStorage

    private var brandId: Int
    private var brandName: String
    private var modelId: Binding<Int?>
    private var isCarModelChooseModalActive: Binding<Bool>

    init(brandId: Int, brandName: String, modelId: Binding<Int?>, isCarModelChooseModalActive: Binding<Bool>) {
        self.brandId = brandId
        self.brandName = brandName
        self.modelId = modelId
        self.isCarModelChooseModalActive = isCarModelChooseModalActive
    }

    var body: some View {
        if let clientLookupModel = lookupStorage.clientLookupModel {
            return AnyView(
                    VStack {
                        List(clientLookupModel.carBrandModelsModels.first { item in
                            item.id == brandId
                        }?.models ?? []) { carModel in
                            Button(action: {
                                self.modelId.wrappedValue = carModel.id
                                self.isCarModelChooseModalActive.wrappedValue.toggle()
                            }) {
                                Text("\(self.brandName) \(carModel.name)")
                                        .padding()
                                        .font(.system(size: 18, weight: .bold))
                            }
                                    .navigationBarTitle("Choose a car model")
                        }
                    }
                            .frame(minWidth: 0, maxWidth: .infinity)
            )
        } else {
            return AnyView(EmptyView())
        }
    }
}
