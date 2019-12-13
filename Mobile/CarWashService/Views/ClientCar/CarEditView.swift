//
// Created by Roman Mashenkin on 11.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarEditView: View {
    @Environment(\.presentationMode) private var presentationMode: Binding<PresentationMode>
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State var isActive = false

    @State var modelId: Int?
    @State var selection1: String
    @State var selection2: String
    @State var selection3: String
    @State var selection4: String
    @State var selection5: String
    @State var selection6: String
    @State var selection7: String
    @State var selection8: String
    @State var selection9: String

    var carId: Int? = nil

    init(model: ClientCarModel? = nil) {
        self.carId = model?.id
        _modelId = State(initialValue: model?.modelId)
        _selection1 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 0, defaultValue: "А"))
        _selection2 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 1, defaultValue: "0"))
        _selection3 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 2, defaultValue: "0"))
        _selection4 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 3, defaultValue: "1"))
        _selection5 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 4, defaultValue: "А"))
        _selection6 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 5, defaultValue: "А"))
        _selection7 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 6, defaultValue: ""))
        _selection8 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 7, defaultValue: "0"))
        _selection9 = State(initialValue: CarEditView.getSymbol(model?.governmentPlate, index: 8, defaultValue: "1"))
    }

    private static func getSymbol(_ governmentPlate: String?, index: Int, defaultValue: String) -> String {
        guard let governmentPlate = governmentPlate else {
            return defaultValue
        }
        var index = index
        if governmentPlate.count == 8 {
            if index == 6 {
                return defaultValue
            } else if index > 6 {
                index -= 1
            }
        }
        return String(governmentPlate[governmentPlate.index(governmentPlate.startIndex, offsetBy: index)])
    }

    private func getGovernmentPlate() -> String {
        "\(selection1)\(selection2)\(selection3)\(selection4)\(selection5)\(selection6)\(selection7)\(selection8)\(selection9)"
    }

    var body: some View {
        GeometryReader { geometry in
            ScrollView {
                VStack {
                    TitledContainer(ClientCarFieldTitle.Model) {
                        NavigationLink(destination: CarBrandModelSelector(modelId: self.$modelId), isActive: self.$isActive) {
                            HStack {
                                Text(self.lookupStorage.clientLookupModel?.getModelName(id: self.modelId) ?? ClientCarFieldName.Model)
                                        .foregroundColor(.black)
                                Spacer()
                                Image(systemName: "chevron.right")
                                        .font(.system(size: 18, weight: .bold))
                                        .foregroundColor(.black)
                            }
                                    .padding(.horizontal)
                                    .padding(.top, 5)
                                    .padding(.bottom, 5)
                        }
                    }
                            .padding(.bottom, 10)
                    TitledContainer(ClientCarFieldTitle.GovernmentPlate) {
                        HStack(spacing: 0) {
                            GovernmentPlateSymbolView(selection: self.$selection1, symbols: GovernmentPlateSymbols.letters, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection2, symbols: GovernmentPlateSymbols.mainIntegers, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection3, symbols: GovernmentPlateSymbols.mainIntegers, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection4, symbols: GovernmentPlateSymbols.mainIntegers, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection5, symbols: GovernmentPlateSymbols.letters, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection6, symbols: GovernmentPlateSymbols.letters, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection7, symbols: GovernmentPlateSymbols.specialIntegers, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection8, symbols: GovernmentPlateSymbols.mainIntegers, width: geometry.size.width / 10)
                            GovernmentPlateSymbolView(selection: self.$selection9, symbols: GovernmentPlateSymbols.mainIntegers, width: geometry.size.width / 10)
                        }
                    }
                            .padding(.top, 10)
                    HStack {
//                        Button(action: {
//                            self.presentationMode.wrappedValue.dismiss()
//                        }) {
//                            Text("Cancel")
//                                    .bold()
//                                    .padding()
//                                    .frame(minWidth: 0, maxWidth: .infinity)
//                                    .background(ApplicationColor.Primary.toRGB())
//                                    .cornerRadius(5)
//                                    .foregroundColor(.white)
//                                    .padding(10)
//                        }
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
                    .navigationBarTitle(Text(self.carId != nil ? ClientCarViewType.EditView : ClientCarViewType.AddView))
        }
    }
}
