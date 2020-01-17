//
// Created by Roman Mashenkin on 04.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashAppointmentView: View {
    @Environment(\.presentationMode) var presentationMode: Binding<PresentationMode>
    @State var carWashId: Int
    @State var chooseServicesModalIsActive = false
    @State var selectedClientCarId: Int? = nil
    @State var clientCarList = [ClientCarModel]()
    @State var date = Date()
    @State var hour = 0
    @State var minute = 0
    @State var selectedServiceIds = [Int]()
    @Binding var services: [CarWashServiceModel]
    @Binding var workingHours: WorkingHoursModel?

    @State var clientBlockIsCompleted = false
    @State var servicesBlockIsCompleted = false

    private let appointmentController = AppointmentController()

    private var clientBlockIsCompletedProxy: Binding<Bool> {
        Binding<Bool>(get: { self.clientBlockIsCompleted }, set: {
            self.clientBlockIsCompleted = $0
        })
    }

    private var selectedServiceIdsProxy: Binding<[Int]> {
        Binding<[Int]>(get: { self.selectedServiceIds }, set: {
            self.selectedServiceIds = $0
            self.servicesBlockIsCompleted = !self.selectedServiceIds.isEmpty
        })
    }

    var body: some View {
        ScrollView {
            VStack {
                HStack {
                    Text("Client")
                            .font(.system(size: 24, weight: .bold))
                    Spacer()
                }
                        .padding()
                CarWashAppointmentClientView(selectedClientCarId: self.$selectedClientCarId, clientCarList: self.$clientCarList, date: self.$date, hour: self.$hour, minute: self.$minute, workingHours: self.$workingHours, clientBlockIsCompleted: self.clientBlockIsCompletedProxy)
                HStack {
                    HStack {
                        Text("Services")
                                .font(.system(size: 24, weight: .bold))
                        Spacer()
                    }
                    Spacer()
                    HStack {
                        Spacer()
                        Button(action: {
                            self.chooseServicesModalIsActive.toggle()
                        }) {
                            Text("Choose")
                                    .bold()
                                    .padding()
                                    .background(ApplicationColor.Primary.toColor())
                                    .cornerRadius(5)
                                    .foregroundColor(.white)
                        }.sheet(isPresented: self.$chooseServicesModalIsActive) {
                            NavigationView {
                                ServicesView(selectable: true, selectedServiceIds: self.selectedServiceIdsProxy, services: self.$services)
                                        .navigationBarTitle("Choose services", displayMode: .large)
                            }.accentColor(ApplicationColor.Primary.toColor())
                        }
                    }
                }
                        .padding()
                CarWashAppointmentServicesView(selectedServiceIds: self.$selectedServiceIds, services: self.$services)
                if self.clientBlockIsCompleted && self.servicesBlockIsCompleted {
                    Button(action: {
                        let startTime = "\(DateTimeUtils.formatFrom(CustomDateTimeFormat.dateServerFormat, self.date))T\((self.hour < 10 ? "0" : ""))\(self.hour):\((self.minute == 0 ? "0" : ""))\(self.minute)"
                        let model = AppointmentManageItemModel(carId: self.selectedClientCarId!, carWashId: self.carWashId, startTime: startTime, carWashServiceIds: self.selectedServiceIds)
                        self.appointmentController.addAppointment(carWashId: self.carWashId, model: model) {
                            DispatchQueue.main.async {
                                self.presentationMode.wrappedValue.dismiss()
                            }
                        }
                    }) {
                        Text("Create an appointment")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.Primary.toColor())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                } else {
                    Button(action: {
                    }) {
                        Text("Fill all fields correctly")
                                .bold()
                                .padding()
                                .frame(minWidth: 0, maxWidth: .infinity)
                                .background(ApplicationColor.MiddleGray.toColor())
                                .cornerRadius(5)
                                .foregroundColor(.white)
                                .padding(10)
                    }
                }
            }
        }
                .navigationBarTitle("Checkout", displayMode: .large)
    }
}
