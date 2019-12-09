//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class ClientProfileController {
    let service = HttpClientService()

    func getClientProfile(_ entity: ClientProfileEntity, isLoaded: Binding<Bool>) {
        try! service.get(endpoint: Requests.GetClientProfileData, success: { (response: ClientProfileModel) in
            entity.toEntity(response)
            isLoaded.wrappedValue = true
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func updateClientProfile(_ entity: ClientProfileEntity) {
        let request = entity.toModel()
        try! service.put(endpoint: Requests.GetClientProfileData, request: request, success: { (response: EmptyResponse) in
            print("SUCCESS CLIENT UPDATE")
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }
}
