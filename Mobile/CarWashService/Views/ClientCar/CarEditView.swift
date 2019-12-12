//
// Created by Roman Mashenkin on 11.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarEditView: View {
    var typeOfAction: String

    @Environment(\.presentationMode) private var presentationMode: Binding<PresentationMode>
//    @EnvironmentObject private var lookupStorage: LookupStorage
//    @State private var items = ["Audi", "BMW", "Mercedes", "Volkswagen", "Renault"]
//    @State private var picker: String = "Renault Sandero"
    @State private var governmentPlate = ""

    var body: some View {
        NavigationView {
            ScrollView {
                VStack {
                    TitledContainer(ClientCarFieldTitle.Model) {
//                        ArrayTextPickerView(ClientCarFieldName.Model, selection: self.$picker, items: self.items)
//                                .setRoundedBorderTextFieldStyle()
//                                .padding(.horizontal)
                        Text("Renault Sandero")
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .padding(.horizontal)
                    }
                            .padding(.bottom, 10)
                    TitledContainer(ClientCarFieldTitle.GovernmentPlate) {
                        TextField(ClientCarFieldName.GovernmentPlate, text: self.$governmentPlate)
                                .textFieldStyle(RoundedBorderTextFieldStyle())
                                .padding(.horizontal)
                    }
                            .padding(.top, 10)
                    HStack {
                        Button(action: {
                            self.presentationMode.wrappedValue.dismiss()
                        }) {
                            Text("Cancel")
                                    .bold()
                                    .padding()
                                    .frame(minWidth: 0, maxWidth: .infinity)
                                    .background(ApplicationColor.Primary.toRGB())
                                    .cornerRadius(5)
                                    .foregroundColor(.white)
                                    .padding(10)
                        }
                        Button(action: {
                        }) {
                            Text("Save")
                                    .bold()
                                    .padding()
                                    .frame(minWidth: 0, maxWidth: .infinity)
                                    .background(ApplicationColor.Primary.toRGB())
                                    .cornerRadius(5)
                                    .foregroundColor(.white)
                                    .padding(10)
                        }
                    }.padding()
                }.padding(.top, 15)
            }
                    .animation(.none)
                    .navigationBarTitle(Text(typeOfAction))
        }
    }
}
