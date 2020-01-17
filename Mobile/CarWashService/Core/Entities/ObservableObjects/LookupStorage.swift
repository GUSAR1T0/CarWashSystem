//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class LookupStorage: ObservableObject {
    @Published var clientLookupModel: ClientLookupModel? = nil

    func load() {
        let service = HttpClientService()
        let semaphore = DispatchSemaphore(value: 0)
        var clientLookupModel: ClientLookupModel? = nil
        try! service.get(endpoint: Requests.GetLookup, success: { (response: ClientLookupModel) in
            clientLookupModel = response
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
        self.clientLookupModel = clientLookupModel
    }

    func reset() {
        self.clientLookupModel = nil
    }

    func getCarModelNameWithGovernmentPlate(modelId: Int?, governmentPlate: String?) -> String? {
        guard let modelName = self.clientLookupModel?.getModelName(id: modelId) else {
            return nil
        }

        return "\(modelName) (\(governmentPlate!))"
    }
}
