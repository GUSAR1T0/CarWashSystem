//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class ErrorResponse: Codable {
    public var source: String?
    public var target: String?
    public var message: String
    public var stackTrace: String?
}
