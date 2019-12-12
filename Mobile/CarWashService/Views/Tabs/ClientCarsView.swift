//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct ClientCarsView: View {
    @State private var isAddCarModalActive = false
    @State private var isEditCarModalActive = false
    @State private var deleteItem: ClientCarModel? = nil
    @State private var isDeleteActionActive = false
    @State private var isLoaded = false
    @State private var clientCarList = [ClientCarModel]()

    private let clientProfileController = ClientProfileController()

    var body: some View {
        if !isLoaded && !isDeleteActionActive {
            clientProfileController.getAllCars($clientCarList, isLoaded: $isLoaded)
        }
        return NavigationView {
            ScrollView {
                if isLoaded {
                    VStack {
                        HStack {
                            Button(action: {
                                self.isAddCarModalActive.toggle()
                            }) {
                                Text(ClientProfileViewText.AddNewCarButtonText)
                                        .bold()
                                        .padding()
                                        .frame(minWidth: 0, maxWidth: .infinity)
                                        .background(ApplicationColor.Primary.toRGB())
                                        .cornerRadius(5)
                                        .foregroundColor(.white)
                                        .padding(10)
                            }.sheet(isPresented: $isAddCarModalActive) {
                                CarEditView(typeOfAction: ClientCarViewType.AddView)
                            }
                        }.padding()
                        if !self.clientCarList.isEmpty {
                            ForEach(self.clientCarList) { car in
                                CardView {
                                    HStack {
                                        VStack {
                                            Text(car.model)
                                                    .font(.system(size: 28, weight: .bold))
                                                    .frame(minWidth: 0, maxWidth: .infinity)
                                            Text(car.governmentPlate)
                                                    .frame(minWidth: 0, maxWidth: .infinity)
                                                    .padding(.top, 10)
                                            HStack {
                                                Button(action: {
                                                    self.deleteItem = car
                                                    self.isDeleteActionActive = true
                                                }) {
                                                    Text(ClientProfileViewText.DeleteCarButtonText)
                                                            .bold()
                                                            .padding()
                                                            .frame(minWidth: 0, maxWidth: .infinity)
                                                            .background(ApplicationColor.Primary.toRGB())
                                                            .cornerRadius(5)
                                                            .foregroundColor(.white)
                                                            .padding(10)
                                                }.actionSheet(item: self.$deleteItem) { item in
                                                    ActionSheet(title: Text(ClientProfileViewText.QuestionAboutCarDeletion), buttons: [
                                                        .default(Text(ClientProfileViewText.DeleteCarSubmitButtonText)) {
                                                            self.isLoaded = false
                                                            self.clientProfileController.deleteCar(item.id) {
                                                                self.clientProfileController.getAllCars(self.$clientCarList, isLoaded: self.$isLoaded)
                                                                self.isDeleteActionActive = false
                                                            }
                                                        },
                                                        .cancel()
                                                    ])
                                                }
                                                Button(action: {
                                                    self.isEditCarModalActive.toggle()
                                                }) {
                                                    Text(ClientProfileViewText.EditCarButtonText)
                                                            .bold()
                                                            .padding()
                                                            .frame(minWidth: 0, maxWidth: .infinity)
                                                            .background(ApplicationColor.Primary.toRGB())
                                                            .cornerRadius(5)
                                                            .foregroundColor(.white)
                                                            .padding(10)
                                                }.sheet(isPresented: self.$isEditCarModalActive) {
                                                    CarEditView(typeOfAction: ClientCarViewType.EditView)
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
