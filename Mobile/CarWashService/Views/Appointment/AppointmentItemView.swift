//
//  AppointmentItemView.swift
//  CarWashService
//
//  Created by Anna Boykova on 17.01.2020.
//  Copyright © 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct AppointmentItemView: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var model: AppointmentFullModel? = nil
    @State private var selectedServiceIds = [Int]()
    var carWashId: Int
    var appointmentId: Int

    private let appointmentController = AppointmentController()

    private var services: Binding<[CarWashServiceModel]> {
        Binding<[CarWashServiceModel]>(get: { self.model!.carWashServices }, set: { value in
        })
    }

    private var history: Binding<[AppointmentHistoryModel]> {
        Binding<[AppointmentHistoryModel]>(get: { self.model!.history }, set: { value in
        })
    }

    var body: some View {
        ScrollView {
            VStack {
                if self.model != nil {
                    HStack {
                        Text("Main description")
                                .font(.system(size: 24, weight: .bold))
                        Spacer()
                    }
                            .padding()
                    AppointmentRow(model: self.model!)
                    Divider()
                    HStack {
                        Text("Chosen services")
                                .font(.system(size: 24, weight: .bold))
                        Spacer()
                    }
                            .padding()
                    if !self.services.wrappedValue.isEmpty {
                        ForEach(self.services.wrappedValue) { service in
                            ServiceItemView(selectable: false, selectedServiceIds: self.$selectedServiceIds, service: service)
                        }
                    } else {
                        Text("No chosen services")
                                .font(.system(size: 24, weight: .regular))
                                .padding()
                    }
                    Divider()
                    HStack {
                        Text("History")
                                .font(.system(size: 24, weight: .bold))
                        Spacer()
                    }
                            .padding()
                    if !self.history.wrappedValue.isEmpty {
                        ForEach(self.history.wrappedValue) { record in
                            VStack {
                                HStack {
                                    Text("• \(record.timestamp)")
                                            .padding(.bottom, 10)
                                    Spacer()
                                }
                                HStack {
                                    Text(record.action)
                                            .bold()
                                    Spacer()
                                }
                            }
                                    .padding()
                        }
                    } else {
                        Text("No history")
                                .font(.system(size: 24, weight: .regular))
                                .padding()
                    }
                    HStack {
                        if self.model!.status == 3 {
                            Button(action: {
                                self.appointmentController.changeStatusToApprove(carWashId: self.carWashId, appointmentId: self.appointmentId) {
                                    self.appointmentController.getAppointment(carWashId: self.carWashId, appointmentId: self.appointmentId) { model in
                                        self.model = model
                                    }
                                }
                            }) {
                                Text("Approve")
                                        .bold()
                                        .padding()
                                        .frame(minWidth: 0, maxWidth: .infinity)
                                        .background(ApplicationColor.Info.toColor())
                                        .cornerRadius(5)
                                        .foregroundColor(.white)
                                        .padding(10)
                            }
                        }
                        if self.model!.status == 1 || self.model!.status == 2 || self.model!.status == 3 {
                            Button(action: {
                                self.appointmentController.changeStatusToCancel(carWashId: self.carWashId, appointmentId: self.appointmentId) {
                                    self.appointmentController.getAppointment(carWashId: self.carWashId, appointmentId: self.appointmentId) { model in
                                        self.model = model
                                    }
                                }
                            }) {
                                Text("Cancel")
                                        .bold()
                                        .padding()
                                        .frame(minWidth: 0, maxWidth: .infinity)
                                        .background(ApplicationColor.Warning.toColor())
                                        .cornerRadius(5)
                                        .foregroundColor(.white)
                                        .padding(10)
                            }
                        }
                    }
                } else {
                    Text("No appointment data")
                }
            }
                    .frame(minWidth: 0, maxWidth: .infinity)
        }
                .onAppear {
                    self.appointmentController.getAppointment(carWashId: self.carWashId, appointmentId: self.appointmentId) { model in
                        self.model = model
                    }
                }
                .navigationBarTitle(Text("Appointment Info"))
    }
        }
