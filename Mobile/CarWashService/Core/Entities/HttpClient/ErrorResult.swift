//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class ErrorResult {
    public var reasonCode: Int? = nil
    public var response: ErrorResponse? = nil
    public var httpClientError: Error? = nil

    init(reasonCode: Int) {
        self.reasonCode = reasonCode
    }

    convenience init(reasonCode: Int, response: ErrorResponse) {
        self.init(reasonCode: reasonCode)
        self.response = response
    }

    init(httpClientError: Error) {
        self.httpClientError = httpClientError
    }
}
