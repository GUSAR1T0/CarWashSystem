//
// Created by Roman Mashenkin on 13.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarBrandModelSelector: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State var isActive = false
    private var modelId: Binding<Int?>

    init(modelId: Binding<Int?>) {
        self.modelId = modelId
    }

    var body: some View {
        if let clientLookupModel = lookupStorage.clientLookupModel {
            return AnyView(
                    VStack {
                        List(clientLookupModel.carBrandModelsModels) { carBrandModels in
                            NavigationLink(destination: CarModelSelector(brandId: carBrandModels.id, brandName: carBrandModels.name, modelId: self.modelId)) {
                                Text(carBrandModels.name)
                            }
                                    .padding()
                                    .font(.system(size: 18, weight: .bold))
                                    .navigationBarTitle("Choose a car brand")
                        }
                    }
                                .frame(minWidth: 0, maxWidth: .infinity)
            )
        } else {
            return AnyView(EmptyView())
        }
    }
}
