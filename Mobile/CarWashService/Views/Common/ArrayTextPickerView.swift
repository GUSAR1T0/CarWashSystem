//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit

class CustomPickerView: UIPickerView {
    var selection: Binding<String>? = nil
    var items: [String] = []
}

extension CustomPickerView: UIPickerViewDelegate {
    public func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        selection?.wrappedValue = items[row]
    }

    public func pickerView(_ pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String? {
        items[row]
    }
}

extension CustomPickerView: UIPickerViewDataSource {
    public func numberOfComponents(in pickerView: UIPickerView) -> Int {
        1
    }

    public func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        items.count
    }
}

final class ArrayTextPickerView: CustomTextPickerView {
    private(set) var textField: UITextField
    private(set) var placeholder: String
    private(set) var selection: Binding<String>

    private var picker = CustomPickerView(frame: .zero)
    private var items: [String]

    init(_ placeholder: String, selection: Binding<String>, items: [String]) {
        self.textField = UITextField()
        self.placeholder = placeholder
        self.selection = selection
        self.items = items
        self.picker.selection = self.selection
        self.picker.items = self.items
        self.picker.dataSource = self.picker
        self.picker.delegate = self.picker
    }

    func makeUIView(context: Context) -> UITextField {
        textField.placeholder = placeholder
        textField.inputView = picker

        setToolbar(selector: #selector(complete))

        return textField
    }

    func updateUIView(_ uiView: UITextField, context: Context) {
        let value: String = self.selection.wrappedValue
        uiView.text = value
        for (index, element) in self.items.enumerated() {
            if element == value {
                self.picker.selectRow(index, inComponent: 0, animated: true)
            }
        }
    }

    @objc func complete() {
        textField.resignFirstResponder()
    }
}
