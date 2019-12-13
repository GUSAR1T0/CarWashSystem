//
// Created by Roman Mashenkin on 13.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarModelSelector: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    @Environment(\.presentationMode) var presentationMode: Binding<PresentationMode>

    private var brandId: Int
    private var brandName: String
    private var modelId: Binding<Int?>

    init(brandId: Int, brandName: String, modelId: Binding<Int?>) {
        self.brandId = brandId
        self.brandName = brandName
        self.modelId = modelId
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
                                self.presentationMode.wrappedValue.dismiss()
                                self.presentationMode.wrappedValue.dismiss()
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
