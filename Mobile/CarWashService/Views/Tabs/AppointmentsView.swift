//
// Created by Roman Mashenkin on 12.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct AppointmentsView: View {
    @State private var appointmentList = [AppointmentShortModel]()

    private let appointmentController = AppointmentController()

    private func fillModels(_ models: [AppointmentShortModel]) {
        self.appointmentList.removeAll()
        for model in models {
            self.appointmentList.append(model)
        }
    }

    var body: some View {
        NavigationView {
            VStack {
                if !self.appointmentList.isEmpty {
                    List(self.appointmentList) { (appointment: AppointmentShortModel) in
                        NavigationLink(destination: AppointmentItemView(carWashId: appointment.carWashId, appointmentId: appointment.id)) {
                            AppointmentRow(model: appointment)
                        }
                    }
                } else {
                    EmptyView()
                }
            }
                    .onAppear {
                        self.appointmentController.getAppointmentList { models in
                            self.fillModels(models)
                        }
                    }
                    .navigationBarTitle(Text("Appointments"))
        }
    }
}
