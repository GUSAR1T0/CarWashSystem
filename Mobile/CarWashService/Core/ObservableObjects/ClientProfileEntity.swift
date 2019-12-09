//
// Created by Roman Mashenkin on 09.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import Combine

class ClientProfileEntity: ObservableObject, Entity {
    typealias TModel = ClientProfileModel

    var firstName: String = ""
    var lastName: String = ""
    var email: String = ""
    var phone: String = ""
    var birthday: String = ""

    func toEntity(_ model: TModel) {
        firstName = model.firstName
        lastName = model.lastName
        email = model.email ?? ""
        phone = model.phone ?? ""
        birthday = model.birthday == nil || model.birthday == "" ? "" : prepareBirthday(model.birthday!)
    }

    func toModel() -> ClientProfileModel {
        let model = ClientProfileModel()
        model.firstName = firstName
        model.lastName = lastName
        model.email = email == "" ? nil : email
        model.phone = phone == "" ? nil : phone
        model.birthday = birthday == "" ? nil : prepareBirthday()
        return model
    }

    private func prepareBirthday(_ birthday: String) -> String {
        let modified = birthday[..<birthday.index(birthday.startIndex, offsetBy: 10)]
        let date = CustomDateFormatter.formatTo(CustomDateFormatter.serverFormat, String(modified))
        return CustomDateFormatter.formatFrom(CustomDateFormatter.datePickerFormat, date)
    }

    private func prepareBirthday() -> String {
        let date = CustomDateFormatter.formatTo(CustomDateFormatter.datePickerFormat, birthday)
        return CustomDateFormatter.formatFrom(CustomDateFormatter.serverFormat, date)
    }
}
