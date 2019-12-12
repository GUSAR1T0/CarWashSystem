//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit

protocol CustomTextPickerView: UIViewRepresentable where UIViewType == UITextField {
    var textField: UITextField { get }
    var placeholder: String { get }
    var selection: Binding<String> { get }
}

extension CustomTextPickerView {
    func setRoundedBorderTextFieldStyle() -> Self {
        textField.borderStyle = .roundedRect
        return self
    }

    func setToolbar(selector: Selector?) {
        let toolbar = UIToolbar()
        toolbar.sizeToFit()
        let flexibleSpaceButtonItem = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButtonItem = UIBarButtonItem(barButtonSystemItem: .done, target: self, action: selector)
        toolbar.setItems([flexibleSpaceButtonItem, doneButtonItem], animated: true)
        toolbar.isUserInteractionEnabled = true
        textField.inputAccessoryView = toolbar
    }
}