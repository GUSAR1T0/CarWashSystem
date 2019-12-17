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
    @State var carWashId: Int
    @State private var isLoaded = false
    @State private var name = ""
    @State private var email: String?
    @State private var phone: String?
    @State private var location = ""
    @State private var coordinateX: Decimal?
    @State private var coordinateY: Decimal?
    @State private var description: String?
    @State private var hasCafe = false
    @State private var hasRestZone = false
    @State private var hasParking = false
    @State private var hasWC = false
    @State private var hasCardPayment = false
    @State private var workingHours: WorkingHoursModel?
    @State private var isOpen = false
    @State private var services = [CarWashServiceModel]()

    private let carWashToClientController = CarWashToClientController()

    private func fillFields(_ model: CarWashFullModel) {
        self.$name.wrappedValue = model.name
        self.$email.wrappedValue = model.email
        self.$phone.wrappedValue = model.phone
        self.$location.wrappedValue = model.location
        self.$coordinateX.wrappedValue = model.coordinateX
        self.$coordinateY.wrappedValue = model.coordinateY
        self.$description.wrappedValue = model.description
        self.$hasCafe.wrappedValue = model.hasCafe
        self.$hasRestZone.wrappedValue = model.hasRestZone
        self.$hasParking.wrappedValue = model.hasParking
        self.$hasWC.wrappedValue = model.hasWC
        self.$hasCardPayment.wrappedValue = model.hasCardPayment
        self.$workingHours.wrappedValue = model.workingHours
        self.$isOpen.wrappedValue = model.isOpen
        self.$services.wrappedValue = model.services
    }

    var body: some View {
        VStack {
            MapView(coordinateX: coordinateX, coordinateY: coordinateY, title: name)
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
                    Text(self.name)
                            .frame(minWidth: 0, maxWidth: .infinity, alignment: .leading)
                            .font(.system(size: 28, weight: .bold))
                            .fixedSize(horizontal: false, vertical: true) // Workaround, SwiftUI can't calculate elements heights correctly
                            .padding(.bottom, 10)
                    Spacer()
                    HStack {
                        Image(systemName: self.isOpen ? StatusImage.Open : StatusImage.Close)
                                .foregroundColor(self.isOpen ? StatusColor.Open : StatusColor.Close)
                        Text(self.isOpen ? StatusTitle.Open : StatusTitle.Close)
                                .font(.system(size: 14, weight: .bold))
                                .foregroundColor(ApplicationColor.Primary.toRGB())
                    }
                }
                HStack {
                    Spacer()
                    Text(self.location)
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
                                    GeneralInfoView(
                                            description: self.$description,
                                            workingHours: self.$workingHours,
                                            hasCafe: self.$hasCafe,
                                            hasRestZone: self.$hasRestZone,
                                            hasParking: self.$hasParking,
                                            hasWC: self.$hasWC,
                                            hasCardPayment: self.$hasCardPayment
                                    )
                                }
                        )
                ),
                (
                        name: CarWashesButtonTitle.Contacts,
                        view: AnyView(ContactsView(email: self.$email, phone: self.$phone))
                ),
                (
                        name: CarWashesButtonTitle.Services,
                        view: AnyView(ServicesView(services: self.$services))
                )
            ])
                    .onAppear {
                        self.carWashToClientController.getCarWashInfo(isLoaded: self.$isLoaded, carWashId: self.carWashId) { model in
                            self.fillFields(model)
                        }
                    }
                    .padding(.leading, 10)
                    .padding(.trailing, 10)
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
