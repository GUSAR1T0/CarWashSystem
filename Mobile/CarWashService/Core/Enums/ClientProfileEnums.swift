//
//  ClientProfileEnums.swift
//  CarWashService
//
//  Created by Anna Boykova on 05.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct ClientProfileFieldTitle {
    static let Email = "Email Address"
    static let FirstName = "First Name"
    static let LastName = "Last Name"
    static let Phone = "Phone"
    static let Birthday = "Birthday"
}

struct ClientProfileFieldName {
    static let Email = "Enter your Email Address"
    static let FirstName = "Enter your First Name"
    static let LastName = "Enter your Last Name"
    static let Phone = "Enter your Phone"
    static let Birthday = "Enter your Birthday"
}

struct ClientProfileViewText {
    static let QuestionAboutLogOut = "Are you sure that you want to log out?"
    static let QuestionAboutCarDeletion = "Are you sure that you want to delete car?"
    static let LogOutButtonText = "Log out"
    static let LogOutSubmitButtonText = "Submit"
    static let SaveButtonText = "Save"
    static let AddNewCarButtonText = "Add new car"
    static let DeleteCarButtonText = "Delete"
    static let DeleteCarSubmitButtonText = "Submit"
    static let EditCarButtonText = "Edit"
    static let NoCarText = "No cars"
}

struct ClientCarViewType {
    static let AddView = "Add new car"
    static let EditView = "Update car info"
}

struct ClientCarFieldTitle {
    static let Model = "Model"
    static let GovernmentPlate = "Government Plate"
}

struct ClientCarFieldName {
    static let Model = "Choose a car model"
    static let GovernmentPlate = "Enter your Government Plate"
}
