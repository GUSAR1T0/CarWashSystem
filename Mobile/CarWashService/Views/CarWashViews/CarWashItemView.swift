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
                .edgesIgnoringSafeArea(.top)
                .shadow(radius: 10)
                .frame(height: 100)
            VStack(alignment: .leading) {
                Text(self.carWash.name)
                    .font(.system(size: 32, weight: .bold))
                    .padding(.bottom, 10)
                HStack(alignment: .top) {
                    Text(self.carWash.location)
                        .font(.subheadline)
                    Spacer()
                    HStack {
                        Image(systemName: self.carWash.isOpen ? StatusImages.Open : StatusImages.Close)
                            .foregroundColor(self.carWash.isOpen ? StatusColors.Open : StatusColors.Close)
                        Text(self.carWash.isOpen ? StatusTitels.Open : StatusTitels.Close)
                            .font(.system(size: 24, weight: .bold))
                            .foregroundColor(ApplicationColor.Primary.toRGB())
                    }
                }
            }
            .padding()
            TabPickerView(selection: self.$selection, views: [
                (name: CarWashesButtonTitle.Info, view:
                    AnyView(
                        ScrollView {
                            GeneralInfoView(self.carWash.description,
                                workingHours: self.carWash.workingHours,
                                facilities: self.carWash.facilities)
                        })
                    ),
                (name: CarWashesButtonTitle.Services, view:
                    AnyView(
                        ServicesView(services: self.carWash.services))
                    ),
                (name: CarWashesButtonTitle.Contacts, view:
                    AnyView(
                        ContactsView(contacts: self.carWash.contacts))
                    )
            ])
            Button(action: {
                    //TODO: Add make order action
                }) {
                    Text(CarWashesButtonTitle.Order)
                            .bold()
                            .padding()
                            .frame(minWidth: 0, maxWidth: .infinity)
                            .background(ApplicationColor.Primary.toRGB())
                            .cornerRadius(5)
                            .foregroundColor(.white)
                            .padding(10)
                }
        }
    }
}
