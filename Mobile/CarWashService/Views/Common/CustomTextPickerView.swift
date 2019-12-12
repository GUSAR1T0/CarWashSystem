//
// Created by Roman Mashenkin on 12.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import UIKit
import Combine

protocol CustomTextPickerView: UIViewRepresentable where UIViewType == UITextField {
    var textField: UIViewType { get }
    var placeholder: String { get }
    var selection: Binding<String> { get }
}