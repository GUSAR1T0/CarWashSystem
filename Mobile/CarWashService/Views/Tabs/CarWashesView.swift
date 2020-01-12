//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashesView: View {
    @State private var searchText = ""
    @State private var carWashList = [CarWashShortModel]()
    @State private var isLoaded = false

    private let carWashToClientController = CarWashToClientController()

    private func fillModels(_ models: [CarWashShortModel]) {
        self.carWashList.removeAll()
        for model in models {
            self.carWashList.append(model)
        }
    }

    // Customization of segment controller
    init() {
        UISegmentedControl.appearance().selectedSegmentTintColor = ApplicationColor.Primary.toUIColor(opacity: 1)
        UISegmentedControl.appearance().setTitleTextAttributes([
            .foregroundColor: UIColor.white
        ], for: .selected)
    }

    var body: some View {
        NavigationView {
            VStack {
                SearchView(search: self.$searchText)
                        .padding(.top, 10)
                if !self.carWashList.isEmpty {
                    List {
                        // Filtered list of carWashes
                        ForEach(self.carWashList.filter {
                            if searchText != "" {
                                let name = $0.name.lowercased()
                                let location = $0.location.lowercased()
                                let search = searchText.lowercased()
                                return name.contains(search) || location.contains(search)
                            }

                            return true
                        }) { (carWash: CarWashShortModel) in
                            NavigationLink(destination: CarWashItemView(carWashId: carWash.id)) {
                                CarWashRow(model: carWash)
                            }
                        }
                    }
                } else {
                    Spacer()
                }
            }
                    .onAppear {
                        self.carWashToClientController.getCarWashList(isLoaded: self.$isLoaded) { models in
                            self.fillModels(models)
                        }
                    }
                    .navigationBarTitle(Text("Car Washes"))
        }
    }
}
