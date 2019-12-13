//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class ClientProfileController {
    let service = HttpClientService()

    func getClientProfile(_ entity: ClientProfileEntity, isLoaded: Binding<Bool>) {
        try! service.get(endpoint: Requests.GetClientProfile, success: { (response: ClientProfileModel) in
            entity.toEntity(response)
            isLoaded.wrappedValue = true
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func updateClientProfile(_ entity: ClientProfileEntity) {
        let request = entity.toModel()
        try! service.put(endpoint: Requests.UpdateClientProfile, request: request, success: { (_: EmptyResponse) in
            print("SUCCESS CLIENT UPDATE")
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func getAllCars(_ list: Binding<[ClientCarModel]>, isLoaded: Binding<Bool>) {
        try! service.get(endpoint: Requests.GetClientCars, success: { (response: [ClientCarModel]) in
            list.wrappedValue.removeAll()
            for model in response {
                list.wrappedValue.append(model)
            }
            isLoaded.wrappedValue = true
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
