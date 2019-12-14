//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class AuthenticationStorage: ObservableObject {
    @Published var isAuthenticated = false
    @Published var clientAuthenticationProfile: ClientAuthenticationProfileModel? = nil

    init() {
        let service = HttpClientService()
        let semaphore = DispatchSemaphore(value: 0)
        try! service.get(endpoint: Requests.GetClientData, success: { (response: ClientAuthenticationProfileModel) in
            self.isAuthenticated = true
            self.clientAuthenticationProfile = response
            semaphore.signal()
        }, error: { (error: ErrorResult) in
            if error.reasonCode != 401 {
                print(error.response ?? error.httpClientError ?? "Unhandled exception")
            }
            try! service.delete(endpoint: Requests.SignOut)
            semaphore.signal()
        })
        semaphore.wait()
    }
}