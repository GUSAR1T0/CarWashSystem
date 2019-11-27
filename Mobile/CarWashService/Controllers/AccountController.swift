//
// Created by Roman Mashenkin on 27.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation
import SwiftUI

class AccountController {
    let service = HttpClientService()
    let externalSignInService = ExternalSignInService()

    func signIn(_ model: ClientSignInModel) -> ClientProfileModel? {
        var clientProfile: ClientProfileModel? = nil
        let semaphore = DispatchSemaphore(value: 0)
        try! service.post(endpoint: Requests.SignIn, request: model, success: { (response: ClientProfileModel) in
            clientProfile = response
            semaphore.signal()
        }, error: { (error: ErrorResult) in
            if let error = error.response {
                print("ERROR: \(error.message)")
            } else if let error = error.httpClientError {
                print("ERROR: \(error)")
            }
            semaphore.signal()
        })
        semaphore.wait()
        return clientProfile
    }

    func signUp(_ model: ClientSignUpModel) -> ClientProfileModel? {
        var clientProfile: ClientProfileModel? = nil
        let semaphore = DispatchSemaphore(value: 0)
        try! service.post(endpoint: Requests.SignUp, request: model, success: { (response: ClientProfileModel) in
            clientProfile = response
            semaphore.signal()
        }, error: { error in
            if let error = error.response {
                print("ERROR: \(error.message)")
            } else if let error = error.httpClientError {
                print("ERROR: \(error)")
            }
            semaphore.signal()
        })
        semaphore.wait()
        return clientProfile
    }

    func externalSignIn(_ token: String) -> ClientProfileModel? {
        var clientProfile: ClientProfileModel? = nil
        let semaphore = DispatchSemaphore(value: 0)
        try! self.service.get(endpoint: "\(Requests.CompleteExternalSignIn)?token=\(token)", success: { (response: ClientProfileModel) in
            clientProfile = response
            semaphore.signal()
        }, error: { error in
            if let error = error.response {
                print("ERROR: \(error.message)")
            } else if let error = error.httpClientError {
                print("ERROR: \(error)")
            }
            semaphore.signal()
        })
        semaphore.wait()
        return clientProfile
    }

    private func externalSignIn(_ window: UIWindow?, schema: Int, handler: @escaping (String) -> Void) {
        if let window = window {
            externalSignInService.contextProvider = ExternalSignInContextProvider(window)
        }
        externalSignInService.signIn(schema: schema, success: { handler($0) }, error: { error in
            print("ERROR: \(error)")
        })
    }

    func externalSignInThroughGoogle(_ window: UIWindow?, handler: @escaping (String) -> Void) {
        externalSignIn(window, schema: 1, handler: handler)
    }

    func externalSignInThroughVk(_ window: UIWindow?, handler: @escaping (String) -> Void) {
        externalSignIn(window, schema: 2, handler: handler)
    }
}
