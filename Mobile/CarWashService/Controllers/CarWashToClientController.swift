//
// Created by Roman Mashenkin on 17.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class CarWashToClientController {
    let service = HttpClientService()

    func getCarWashList(isLoaded: Binding<Bool>, completeRefresh: @escaping () -> Void = {}, success: @escaping ([CarWashShortModel]) -> Void) {
        isLoaded.wrappedValue = false
        try! service.get(endpoint: Requests.GetCarWashList, success: { (response: [CarWashShortModel]) in
            isLoaded.wrappedValue = true
            success(response)
            completeRefresh()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }

    func getCarWashInfo(isLoaded: Binding<Bool>, carWashId: Int, completeRefresh: @escaping () -> Void = {}, success: @escaping (CarWashFullModel) -> Void) {
        isLoaded.wrappedValue = false
        try! service.get(endpoint: String(format: Requests.GetCarWashInfo, carWashId), success: { (response: CarWashFullModel) in
            isLoaded.wrappedValue = true
            success(response)
            completeRefresh()
        }, error: { (error: ErrorResult) in
            print(error.response ?? error.httpClientError ?? "Unhandled exception")
        })
    }
}
