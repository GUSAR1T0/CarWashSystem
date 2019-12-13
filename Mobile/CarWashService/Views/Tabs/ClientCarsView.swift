//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientCarsView: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var isAddCarViewActive = false
    @State private var isEditCarViewActive: Int? = nil
    @State private var isDeleteCarActionActive: ClientCarModel? = nil
    @State private var isLoaded = false
    @State private var clientCarList = [ClientCarModel]()

    private let clientProfileController = ClientProfileController()

    var body: some View {
        if !isLoaded && (!isAddCarViewActive || isEditCarViewActive == nil || isDeleteCarActionActive == nil) {
            clientProfileController.getAllCars($clientCarList, isLoaded: $isLoaded)
        }
        return NavigationView {
            ScrollView {
                if isLoaded {
                    VStack {
                        HStack {
                            NavigationLink(destination: CarEditView(isCarListLoaded: self.$isLoaded).environmentObject(self.lookupStorage), isActive: $isAddCarViewActive) {
                                Button(action: {
                                    self.isAddCarViewActive.toggle()
                                }) {
                                    Text(ClientProfileViewText.AddNewCarButtonText)
                                            .bold()
                                            .padding()
                                            .frame(minWidth: 0, maxWidth: .infinity)
                                            .background(ApplicationColor.Primary.toRGB())
                                            .cornerRadius(5)
                                            .foregroundColor(.white)
                                            .padding(10)
                                }
                            }
                        }.padding()
                        if !self.clientCarList.isEmpty {
                            ForEach(self.clientCarList) { car in
                                CardView {
                                    HStack {
                                        VStack {
                                            Text(self.lookupStorage.clientLookupModel?.getModelName(id: car.modelId) ?? "Unknown car")
                                                    .font(.system(size: 28, weight: .bold))
                                                    .frame(minWidth: 0, maxWidth: .infinity)
                                            Text(car.governmentPlate)
                                                    .frame(minWidth: 0, maxWidth: .infinity)
                                                    .padding(.top, 10)
                                            HStack {
                                                Button(action: {
                                                    self.isDeleteCarActionActive = car
                                                }) {
                                                    Text(ActionText.DeleteButtonText)
                                                            .bold()
                                                            .padding()
                                                            .frame(minWidth: 0, maxWidth: .infinity)
                                                            .background(ApplicationColor.Primary.toRGB())
                                                            .cornerRadius(5)
                                                            .foregroundColor(.white)
                                                            .padding(10)
                                                }.actionSheet(item: self.$isDeleteCarActionActive) { item in
                                                    ActionSheet(title: Text(ClientProfileViewText.QuestionAboutCarDeletion), buttons: [
                                                        .default(Text(ActionText.SubmitButtonText)) {
                                                            self.clientProfileController.deleteCar(item.id) {
                                                                self.isLoaded.toggle()
                                                            }
                                                        },
                                                        .cancel()
                                                    ])
                                                }
                                                NavigationLink(destination: CarEditView(
                                                        model: car,
                                                        isCarListLoaded: self.$isLoaded
                                                ).environmentObject(self.lookupStorage), tag: car.id, selection: self.$isEditCarViewActive) {
                                                    Button(action: {
                                                        self.isEditCarViewActive = car.id
                                                    }) {
                                                        Text(ActionText.EditButtonText)
                                                                .bold()
                                                                .padding()
                                                                .frame(minWidth: 0, maxWidth: .infinity)
                                                                .background(ApplicationColor.Primary.toRGB())
                                                                .cornerRadius(5)
                                                                .foregroundColor(.white)
                                                                .padding(10)
                                                    }
                                                }
                                            }.padding(.top, 25)
                                        }
                                    }.padding()
                                }
                            }
                        } else {
                            VStack {
                                Spacer()
                                HStack {
                                    Spacer()
                                    Text(ClientProfileViewText.NoCarText)
                                            .bold()
                                            .padding()
                                            .frame(minWidth: 0, maxWidth: .infinity)
                                    Spacer()
                                }.padding()
                                Spacer()
                            }
                        }
                    }.padding(.top, navigationBarPaddingValue).padding(.bottom, 35)
                } else {
                    LoadingView()
                }
            }
                    .animation(.none)
                    .navigationBarTitle(Text("Cars"))
        }
    }
}
