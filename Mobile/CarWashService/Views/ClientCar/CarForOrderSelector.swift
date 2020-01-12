//
// Created by Roman Mashenkin on 13.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarForOrderSelector: View {
    @EnvironmentObject private var lookupStorage: LookupStorage

    @Binding var isCarModelChooseModalActive: Bool
    @Binding var selectedClientCarId: Int?
    @Binding var clientCarList: [ClientCarModel]

    var body: some View {
        VStack {
            if !clientCarList.isEmpty {
                List(self.clientCarList) { clientCar in
                    Button(action: {
                        self.selectedClientCarId = clientCar?.id
                        self.isCarModelChooseModalActive.toggle()
                    }) {
                        Text(self.lookupStorage.getCarModelNameWithGovernmentPlate(modelId: clientCar?.modelId, governmentPlate: clientCar?.governmentPlate) ?? "")
                                .padding()
                                .font(.system(size: 18, weight: .bold))
                    }
                            .navigationBarTitle("Choose your car")
                }
                        .frame(minWidth: 0, maxWidth: .infinity)
            } else {
                Text("No cars")
                        .font(.system(size: 24, weight: .regular))
                        .padding()
                        .navigationBarTitle("Choose your car")
            }
        }
    }
}
