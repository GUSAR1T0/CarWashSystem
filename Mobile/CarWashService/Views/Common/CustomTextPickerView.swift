//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit

protocol CustomTextPickerView: UIViewRepresentable where UIViewType == UITextField {
    var textField: UITextField { get }
    var placeholder: String { get }
    var selection: String { get }
}

extension CustomTextPickerView {
    func setRoundedBorderTextFieldStyle() -> Self {
        textField.borderStyle = .roundedRect
        return self
    }
}