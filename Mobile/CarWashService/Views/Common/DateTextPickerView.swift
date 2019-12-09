//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit
import Combine

final class DateTextPickerView: UIViewRepresentable {
    typealias UIViewType = UITextField

    private var dateField = UITextField()
    private var datePicker = UIDatePicker()

    private var placeholder: String
    private var selection: Binding<String>
    private var format = CustomDateFormatter.datePickerFormat

    init(_ placeholder: String, selection: Binding<String>, format: String? = nil) {
        self.placeholder = placeholder
        self.selection = selection
        if let format = format {
            self.format = format
        }
    }

    func makeUIView(context: Context) -> UIViewType {
        dateField.placeholder = placeholder
        dateField.inputView = datePicker
        dateField.addTarget(self, action: #selector(textFieldDidChange(_:)), for: .allEvents)
        datePicker.datePickerMode = .date

        let toolbar = UIToolbar()
        toolbar.sizeToFit()
        let flexibleSpaceButtonItem = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButtonItem = UIBarButtonItem(barButtonSystemItem: .done, target: self, action: #selector(complete))
        toolbar.setItems([flexibleSpaceButtonItem, doneButtonItem], animated: true)
        dateField.inputAccessoryView = toolbar

        datePicker.addTarget(self, action: #selector(setDate), for: .valueChanged)

        return dateField
    }

    func updateUIView(_ uiView: UIViewType, context: Context) {
        uiView.setContentHuggingPriority(.defaultHigh, for: .vertical)
        uiView.setContentHuggingPriority(.defaultLow, for: .horizontal)
        dateField.text = selection.wrappedValue
        datePicker.date = CustomDateFormatter.formatTo(format, selection.wrappedValue)
    }

    @objc func textFieldDidChange(_ textField: UITextField) {
        selection.wrappedValue = textField.text ?? ""
    }

    @objc private func complete() {
        dateField.endEditing(true)
        setDate()
    }

    @objc private func setDate() {
        dateField.text = CustomDateFormatter.formatFrom(format, datePicker.date)
    }
}

extension DateTextPickerView {
    func setRoundedBorderTextFieldStyle() -> DateTextPickerView {
        dateField.borderStyle = .roundedRect
        return self
    }
}
