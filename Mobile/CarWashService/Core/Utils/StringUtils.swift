//
// Created by Roman Mashenkin on 16.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

class StringUtils {
    static func isEmptyOrNil(_ value: String?) -> Bool {
        value == nil || (value?.isEmpty ?? false)
    }
}
