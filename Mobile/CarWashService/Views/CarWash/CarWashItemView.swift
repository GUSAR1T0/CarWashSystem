//
//  CarWashItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 14.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import MapKit

struct CarWashItemView: View {
    @State private var selection = 0
    @State var carWashId: Int
    @State private var isLoaded = false
    @State private var modalPresented: Bool = false
    @State private var isActionSheetActive: Bool = false
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
    @State private var selectedServiceIds = [Int]()

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
            MapView(coordinateX: coordinateX, coordinateY: coordinateY, title: name, isActionSheetActive: $isActionSheetActive)
                    .overlay(
                            VStack {
                                Spacer()
                                Rectangle()
                                        .stroke(ApplicationColor.Primary.toColor(), lineWidth: 4)
                                        .frame(height: 1.0, alignment: .bottom)
                            }
                    )
                    .shadow(radius: 10)
                    .frame(minHeight: 0, maxHeight: .infinity)
                    .actionSheet(isPresented: $isActionSheetActive) {
                        var buttons = [Alert.Button]()
                        if let coordinateX = coordinateX, let coordinateY = coordinateY {
                            let coordinateX = CLLocationDegrees(truncating: NSDecimalNumber(decimal: coordinateX))
                            let coordinateY = CLLocationDegrees(truncating: NSDecimalNumber(decimal: coordinateY))

                            // Apple Maps
                            buttons.append(.default(Text("Apple Maps")) {
                                let coords = CLLocationCoordinate2D(latitude: coordinateX, longitude: coordinateY)
                                let destination = MKMapItem(placemark: MKPlacemark(coordinate: coords))
                                destination.name = self.name
                                MKMapItem.openMaps(with: [destination], launchOptions: [MKLaunchOptionsDirectionsModeKey: MKLaunchOptionsDirectionsModeDriving])
                            });

                            // Google Maps
                            if (UIApplication.shared.canOpenURL(URL(string: "comgooglemaps://")!)) {
                                buttons.append(.default(Text("Google Maps")) {
                                    UIApplication.shared.open(URL(string: "comgooglemaps://?saddr=&daddr=\(String(format: "%.6f", coordinateX)),\(String(format: "%.6f", coordinateY))&directionsmode=driving")!)
                                })
                            }

                            // Yandex Maps
                            if (UIApplication.shared.canOpenURL(URL(string: "yandexmaps://")!)) {
                                buttons.append(.default(Text("Yandex.Maps")) {
                                    UIApplication.shared.open(URL(string: "yandexmaps://maps.yandex.ru/?pt=\(String(format: "%.6f", coordinateX)),\(String(format: "%.6f", coordinateY))&z=15&l=map")!)
                                })
                            }
                        }

                        buttons.append(.cancel());
                        return ActionSheet(title: Text("Would you like to open it into the one of maps app?"), buttons: buttons)
                    }
            VStack {
                DragElementView()
                CarWashMainDescriptionView(
                        name: self.$name,
                        location: self.$location,
                        isOpen: self.$isOpen
                )
                        .padding()
                        .padding(.bottom, 20)
            }
                    .gesture(
                            TapGesture()
                                    .onEnded { _ in
                                        self.modalPresented = true
                                    }
                    )
                    .gesture(
                            DragGesture()
                                    .onChanged { value in
                                        if value.translation.height <= -50 {
                                            self.modalPresented = true
                                        }
                                    }
                    )
        }
                .sheet(isPresented: $modalPresented) {
                    VStack {
                        DragElementView()
                        CarWashMainDescriptionView(
                                name: self.$name,
                                location: self.$location,
                                isOpen: self.$isOpen
                        )
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
                                    view: AnyView(ServicesView(selectable: false, selectedServiceIds: self.$selectedServiceIds, services: self.$services))
                            )
                        ])
                                .padding(.leading, 10)
                                .padding(.trailing, 10)
                    }
                }
                .onAppear {
                    self.carWashToClientController.getCarWashInfo(isLoaded: self.$isLoaded, carWashId: self.carWashId) { model in
                        self.fillFields(model)
                    }
                }
                .navigationBarTitle("Car Wash Info", displayMode: .inline)
                .navigationBarItems(trailing: NavigationLink(destination: CarWashOrderView(services: self.$services, workingHours: self.$workingHours)) {
                    HStack {
                        Text("Checkout")
                        Image(systemName: "chevron.right")
                                .font(.system(size: 22, weight: .semibold))
                    }
                })
    }
}
