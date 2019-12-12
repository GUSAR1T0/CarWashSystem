//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit
import Combine

final class DateTextPickerView: CustomTextPickerView {
    private(set) var textField: UITextField
    private(set) var placeholder: String
    private(set) var selection: Binding<String>

    private var datePicker = UIDatePicker()
    private var format = CustomDateFormatter.datePickerFormat

    init(_ placeholder: String, selection: Binding<String>, format: String? = nil) {
        self.textField = UITextField()
        self.placeholder = placeholder
        self.selection = selection
        if let format = format {
            self.format = format
        }
    }

    func makeUIView(context: Context) -> UITextField {
        textField.placeholder = placeholder
        textField.inputView = datePicker
        textField.addTarget(self, action: #selector(textFieldDidChange(_:)), for: .allEvents)

        datePicker.datePickerMode = .date
        datePicker.addTarget(self, action: #selector(setDate), for: .valueChanged)

        setToolbar(selector: #selector(complete))

        return textField
    }

    func updateUIView(_ uiView: UITextField, context: Context) {
        uiView.setContentHuggingPriority(.defaultHigh, for: .vertical)
        uiView.setContentHuggingPriority(.defaultLow, for: .horizontal)
        textField.text = selection.wrappedValue
        datePicker.date = CustomDateFormatter.formatTo(format, selection.wrappedValue)
    }

    @objc func textFieldDidChange(_ textField: UITextField) {
        selection.wrappedValue = textField.text ?? ""
    }

    @objc private func complete() {
        textField.endEditing(true)
        setDate()
    }

    @objc private func setDate() {
        textField.text = CustomDateFormatter.formatFrom(format, datePicker.date)
    }
}
