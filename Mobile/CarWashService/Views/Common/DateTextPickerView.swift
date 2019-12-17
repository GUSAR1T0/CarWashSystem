//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit
import Combine

class CustomUIDatePicker: UIDatePicker {
    private var view: DateTextPickerView? = nil

    func setView(_ view: DateTextPickerView) {
        self.view = view
    }

    func setup() {
        addTarget(view, action: #selector(updateDate), for: .valueChanged)
    }

    @objc func updateDate() {
        if let view = view {
            view.textField.text = CustomDateFormatter.formatFrom(view.format, view.datePicker.date)
        }
    }
}

class CustomUIToolbar: UIToolbar {
    private var view: DateTextPickerView? = nil

    func setView(_ view: DateTextPickerView) {
        self.view = view
    }

    func setup() {
        sizeToFit()
        let flexibleSpaceButtonItem = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let doneButtonItem = UIBarButtonItem(barButtonSystemItem: .done, target: view, action: #selector(complete))
        setItems([flexibleSpaceButtonItem, doneButtonItem], animated: true)
        isUserInteractionEnabled = true
    }

    @objc public func updateDate() {
        if let view = view {
            view.textField.text = CustomDateFormatter.formatFrom(view.format, view.datePicker.date)
        }
    }

    @objc public func complete() {
        view?.textField.endEditing(true)
        updateDate()
    }
}

struct DateTextPickerView: CustomTextPickerView {
    private(set) var textField: UITextField
    private(set) var placeholder: String
    @Binding private(set) var selection: String

    private(set) var datePicker = CustomUIDatePicker()
    private(set) var format = CustomDateFormatter.datePickerFormat

    class Coordinator: NSObject, UITextFieldDelegate {
        var parent: DateTextPickerView

        init(_ dateTextPickerView: DateTextPickerView) {
            self.parent = dateTextPickerView
        }

        func textFieldShouldEndEditing(_ textField: UITextField) -> Bool {
            self.parent.selection = textField.text ?? ""
            return true
        }
    }

    init(_ placeholder: String, selection: Binding<String>, format: String? = nil) {
        self.textField = UITextField()
        self.placeholder = placeholder
        self._selection = selection
        if let format = format {
            self.format = format
        }
    }

    func makeCoordinator() -> Coordinator {
        Coordinator(self)
    }

    func makeUIView(context: UIViewRepresentableContext<DateTextPickerView>) -> UITextField {
        textField.placeholder = placeholder
        textField.inputView = datePicker
        textField.delegate = context.coordinator

        datePicker.datePickerMode = .date
        datePicker.setView(self)
        datePicker.setup()

        let toolbar = CustomUIToolbar()
        toolbar.setView(self)
        toolbar.setup()
        textField.inputAccessoryView = toolbar

        return textField
    }

    func updateUIView(_ uiView: UITextField, context: Context) {
        uiView.setContentHuggingPriority(.defaultHigh, for: .vertical)
        uiView.setContentHuggingPriority(.defaultLow, for: .horizontal)
        textField.text = selection
        datePicker.date = CustomDateFormatter.formatTo(format, selection)
    }
}
