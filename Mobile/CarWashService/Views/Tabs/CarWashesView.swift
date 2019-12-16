//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashesView: View {
    @State private var searchText = ""

    // test data
    var carWashes = [
        CarWashModel(
                id: 1,
                name: "Boykosha&Co",
                location: "г Нижний Новгород, ул Максима Горького, д 152",
                description: "“Весна, и любовь, и счастие!” – как будто говорил этот дуб. – И как не надоест вам все один и тот же глупый и бессмысленный обман. Все одно и то же, и все обман! Нет ни весны, ни солнца, ни счастья. Вон смотрите, сидят задавленные мертвые ели, всегда одинокие, и вон и я растопырил свои обломанные, ободранные пальцы, где ни выросли они – из спины, из боков; как выросли – так и стою, и не верю вашим надеждам и обманам”.",
                email: "q@q.q",
                phone: "+78965930459",
                isOpen: true,
                workingHours: WorkingHoursModel(
                        monday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        tuesday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        wednesday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        thursday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        friday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        saturday: WorkingDayModel(startTime: nil, stopTime: nil),
                        sunday: WorkingDayModel(startTime: nil, stopTime: nil)
                ),
                hasCafe: true,
                hasRestZone: true,
                hasParking: false,
                hasWC: true,
                hasCardPayment: true,
                services: [
                    ServiceModel(
                            id: 1,
                            serviceName: "wash",
                            price: 100,
                            duration: "00:30"
                    ),
                    ServiceModel(
                            id: 2,
                            serviceName: "crash",
                            description: "“Весна, и любовь, и счастие!” – как будто говорил этот дуб. – И как не надоест вам все один и тот же глупый и бессмысленный обман. Все одно и то же, и все обман! Нет ни весны, ни солнца, ни счастья. Вон смотрите, сидят задавленные мертвые ели, всегда одинокие, и вон и я растопырил свои обломанные, ободранные пальцы, где ни выросли они – из спины, из боков; как выросли – так и стою, и не верю вашим надеждам и обманам”.",
                            price: 500,
                            duration: "00:50"
                    ),
                    ServiceModel(
                            id: 3,
                            serviceName: "shower",
                            description: "",
                            price: 150,
                            duration: "00:10"
                    )
                ]
        ),
        CarWashModel(
                id: 2,
                name: "Rock",
                location: "г Нижний Новгород, ул Максима Горького, д 160",
                description: "“Весна, и любовь, и счастие!” – как будто говорил этот дуб. – И как не надоест вам все один и тот же глупый и бессмысленный обман. Все одно и то же, и все обман! Нет ни весны, ни солнца, ни счастья. Вон смотрите, сидят задавленные мертвые ели, всегда одинокие, и вон и я растопырил свои обломанные, ободранные пальцы, где ни выросли они – из спины, из боков; как выросли – так и стою, и не верю вашим надеждам и обманам”.",
                email: "y@y.y",
                phone: "+78965930459",
                isOpen: true,
                workingHours: WorkingHoursModel(
                        monday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        tuesday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        wednesday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        thursday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        friday: WorkingDayModel(startTime: "9:00", stopTime: "18:00"),
                        saturday: WorkingDayModel(startTime: nil, stopTime: nil),
                        sunday: WorkingDayModel(startTime: nil, stopTime: nil)
                ),
                hasCafe: true,
                hasRestZone: true,
                hasParking: false,
                hasWC: true,
                hasCardPayment: true,
                services: [
                    ServiceModel(
                            id: 4,
                            serviceName: "wash",
                            description: "test test hello test karl",
                            price: 100,
                            duration: "00:30"
                    ),
                    ServiceModel(
                            id: 5,
                            serviceName: "crash",
                            description: "abc",
                            price: 500,
                            duration: "00:50"
                    ),
                    ServiceModel(
                            id: 6,
                            serviceName: "shower",
                            description: "ffjeifj dsjwiow",
                            price: 150,
                            duration: "00:10"
                    )
                ]
        )
    ]

    // Customization of segment controller
    init() {
        UISegmentedControl.appearance().selectedSegmentTintColor = ApplicationColor.Primary.toRGBA(opacity: 1)
        UISegmentedControl.appearance().setTitleTextAttributes([
            .foregroundColor: UIColor.white
        ], for: .selected)
    }

    var body: some View {
        NavigationView {
            VStack {
                SearchView(search: self.$searchText)
                List {
                    // Filtered list of carWashes
                    ForEach(carWashes.filter {
                        searchText == "" || $0.name.hasPrefix(searchText) || $0.location.hasPrefix(searchText) || $0.location.hasSuffix(searchText)
                    }) { cw in
                        NavigationLink(destination: CarWashItemView(cw)) {
                            CarWashRow(cw.name, location: cw.location, isOpen: cw.isOpen)
                        }
                    }
                }
                        .navigationBarTitle(Text("Car Washes"))
            }
        }
    }
}
