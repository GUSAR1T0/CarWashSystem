//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class LookupStorage: ObservableObject {
    @Published var carBrandModelsModels = [CarBrandModelsModel]()

    func load() {
        let service = HttpClientService()
        try! service.get(endpoint: Requests.GetLookup, success: { (response: ClientLookupModel) in
            for model in response.carBrandModelsModels {
                self.carBrandModelsModels.append(model)
            }
        }, error: { error in
            if let error = error.response {
                print("ERROR: \(error.message)")
            } else if let error = error.httpClientError {
                print("ERROR: \(error)")
            }
        })
    }

    func reset() {
        self.carBrandModelsModels.removeAll()
    }
}
