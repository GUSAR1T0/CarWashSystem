//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class ClientProfileController {
    let service = HttpClientService()

    func getClientProfile(isLoaded: Binding<Bool>, completeRefresh: @escaping () -> Void = {}, success: @escaping (ClientProfileModel) -> Void) {
        isLoaded.wrappedValue = false
        try! service.get(endpoint: Requests.GetClientProfile, success: { (response: ClientProfileModel) in
            isLoaded.wrappedValue = true
            success(response)
            completeRefresh()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func updateClientProfile(_ model: ClientProfileModel) {
        try! service.put(endpoint: Requests.UpdateClientProfile, request: model, success: { (_: EmptyResponse) in
            print("SUCCESS CLIENT UPDATE")
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func getAllCars(isLoaded: Binding<Bool>, completeRefresh: @escaping () -> Void = {}, success: @escaping ([ClientCarModel]) -> Void) {
        isLoaded.wrappedValue = false
        try! service.get(endpoint: Requests.GetClientCars, success: { (response: [ClientCarModel]) in
            isLoaded.wrappedValue = true
            success(response)
            completeRefresh()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func addCar(_ model: ClientCarModel, callback: @escaping () -> Void) {
        try! service.post(endpoint: Requests.AddClientCar, request: model, success: { (_: EmptyResponse) in callback() }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func updateCar(_ model: ClientCarModel, callback: @escaping () -> Void) {
        try! service.put(endpoint: String(format: Requests.UpdateClientCar, model.id), request: model, success: { (_: EmptyResponse) in callback() }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func deleteCar(_ carId: Int, callback: @escaping () -> Void) {
        try! service.delete(endpoint: String(format: Requests.DeleteClientCar, carId), success: { (_: EmptyResponse) in callback() })
    }
}
