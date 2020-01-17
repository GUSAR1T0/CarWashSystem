//
// Created by Roman Mashenkin on 17.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class AppointmentController {
    let service = HttpClientService()

    func getAppointmentList(success: @escaping ([AppointmentShortModel]) -> Void) {
        try! service.get(endpoint: Requests.GetClientAppointmentList, success: { (response: [AppointmentShortModel]) in
            success(response)
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func getAppointment(carWashId: Int, appointmentId: Int, success: @escaping (AppointmentFullModel) -> Void) {
        try! service.get(endpoint: String(format: Requests.GetClientAppointment, carWashId, appointmentId), success: { (response: AppointmentFullModel) in
            success(response)
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func addAppointment(carWashId: Int, model: AppointmentManageItemModel, success: @escaping () -> Void) {
        try! service.post(endpoint: String(format: Requests.AddClientAppointment, carWashId), request: model, success: { (_: EmptyResponse) in
            success()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func changeStatusToApprove(carWashId: Int, appointmentId: Int, success: @escaping () -> Void) {
        try! service.put(endpoint: String(format: Requests.ApproveClientAppointment, carWashId, appointmentId), success: { (_: EmptyResponse) in
            success()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func changeStatusToCancel(carWashId: Int, appointmentId: Int, success: @escaping () -> Void) {
        try! service.put(endpoint: String(format: Requests.CancelClientAppointment, carWashId, appointmentId), success: { (_: EmptyResponse) in
            success()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }
}
