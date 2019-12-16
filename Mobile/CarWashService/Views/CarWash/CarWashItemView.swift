//
//  CarWashItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 14.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashItemView: View {
    @State private var selection = 0

    var carWash: CarWashModel

    @inlinable public init(_ carWash: CarWashModel) {
        self.carWash = carWash
    }

    var body: some View {
        VStack {
            MapView()
                    .overlay(
                            VStack {
                                Spacer()
                                Rectangle()
                                        .stroke(ApplicationColor.Primary.toRGB(), lineWidth: 4)
                                        .frame(height: 1.0, alignment: .bottom)
                            }
                    )
                    .shadow(radius: 10)
                    .frame(height: 200)
            VStack(alignment: .leading) {
                HStack(alignment: .top) {
                    ScrollView {
                        Text(self.carWash.name)
                                .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                                .font(.system(size: 32, weight: .bold))
                                .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                                .padding(.bottom, 10)
                    }
                            .frame(height: 50)
                    Spacer()
                    HStack {
                        Image(systemName: self.carWash.isOpen ? StatusImage.Open : StatusImage.Close)
                                .foregroundColor(self.carWash.isOpen ? StatusColor.Open : StatusColor.Close)
                        Text(self.carWash.isOpen ? StatusTitle.Open : StatusTitle.Close)
                                .font(.system(size: 14, weight: .bold))
                                .foregroundColor(ApplicationColor.Primary.toRGB())
                    }
                }
                HStack {
                    Spacer()
                    Text(self.carWash.location)
                            .font(.subheadline)
                    Spacer()
                }
            }
                    .padding()
            TabPickerView(selection: self.$selection, views: [
                (
                        name: CarWashesButtonTitle.Info,
                        view: AnyView(
                                ScrollView {
                                    GeneralInfoView(carWash: self.carWash)
                                }
                                        .padding(.top, 10)
                        )
                ),
                (
                        name: CarWashesButtonTitle.Contacts,
                        view: AnyView(ContactsView(email: self.carWash.email, phone: self.carWash.phone))
                ),
                (
                        name: CarWashesButtonTitle.Services,
                        view: AnyView(ServicesView(services: self.carWash.services))
                )
            ])
                    .padding(.leading, 10)
                    .padding(.trailing, 10)
                    .padding(.bottom, 30)
        }
                .navigationBarTitle("Car Wash Info", displayMode: .inline)
                .navigationBarItems(trailing: NavigationLink(destination: EmptyView().navigationBarTitle("", displayMode: .large)) {
                    HStack {
                        Text("Order")
                        Image(systemName: "chevron.right")
                                .font(.system(size: 22, weight: .semibold))
                    }
                })
    }
}
