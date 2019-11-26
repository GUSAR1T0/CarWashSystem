//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class HttpClientError: Error, CustomStringConvertible {
    let message: String
    let error: Any?

    init(message: String, error: Any? = nil) {
        self.message = message
        self.error = error
    }

    public var description: String {
        message
    }
}
