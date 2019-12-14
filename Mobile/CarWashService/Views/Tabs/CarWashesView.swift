//
// Created by Roman Mashenkin on 08.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashesView: View {
    
    //Customization of segment controler
    init() {
        UISegmentedControl.appearance().selectedSegmentTintColor = ApplicationColor.Primary.toUIColor(opaticity: 1)
        UISegmentedControl.appearance().setTitleTextAttributes(
            [
                .foregroundColor: UIColor.white
        ], for: .selected)
    }
    
    // test data
    var carWashes = [
        CarWashModel(name:"Boykosha&Co", location: "г Нижний Новгород, ул Максима Горького, д 152",
                     description: "“Весна, и любовь, и счастие!” – как будто говорил этот дуб. – И как не надоест вам все один и тот же глупый и бессмысленный обман. Все одно и то же, и все обман! Нет ни весны, ни солнца, ни счастья. Вон смотрите, сидят задавленные мертвые ели, всегда одинокие, и вон и я растопырил свои обломанные, ободранные пальцы, где ни выросли они – из спины, из боков; как выросли – так и стою, и не верю вашим надеждам и обманам”.",
                     isOpen: true,
                     workingHours: [
                             WorkingHoursModel(day: "MO", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "TU", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "WE", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "TH", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "FR", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "SA", startTime: "9:00", endTime: "18:00"),
                             WorkingHoursModel(day: "SU", startTime: "9:00", endTime: "18:00")
                         ],
                     facilities: [
                             FacilityModel(title: Facilities.Cafe, isAvaiable: true),
                             FacilityModel(title: Facilities.RestZone, isAvaiable: true),
                             FacilityModel(title: Facilities.Parking, isAvaiable: false),
                             FacilityModel(title: Facilities.WC, isAvaiable: true),
                             FacilityModel(title: Facilities.CardPayment, isAvaiable: true)
                         ],
                     services: [
                             ServiceModel(name: "wash", description: "test test hello test karl",
                                 price: 100, duration: "00:30"),
                             ServiceModel(name: "crash", description: "abc",
                                 price: 500, duration: "00:50"),
                             ServiceModel(name: "shower", description: "ffjeifj dsjwiow",
                                 price: 150, duration: "00:10")
                         ],
                     contacts: [
                             ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160",
                                          email:"q@q.q", phone:"+78965930459"),
                             ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160А",
                                 email:"y@y.y", phone:"+78965930459"),
                             ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160А/2",
                                 email:"a@a.a", phone:"+78965930459")
                         ]),
        CarWashModel(name:"Rock", location: "г Нижний Новгород, ул Максима Горького, д 160",
        description: "“Весна, и любовь, и счастие!” – как будто говорил этот дуб. – И как не надоест вам все один и тот же глупый и бессмысленный обман. Все одно и то же, и все обман! Нет ни весны, ни солнца, ни счастья. Вон смотрите, сидят задавленные мертвые ели, всегда одинокие, и вон и я растопырил свои обломанные, ободранные пальцы, где ни выросли они – из спины, из боков; как выросли – так и стою, и не верю вашим надеждам и обманам”.",
        isOpen: true,
        workingHours: [
                WorkingHoursModel(day: "MO", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "TU", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "WE", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "TH", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "FR", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "SA", startTime: "9:00", endTime: "18:00"),
                WorkingHoursModel(day: "SU", startTime: "9:00", endTime: "18:00")
            ],
        facilities: [
                FacilityModel(title: Facilities.Cafe, isAvaiable: true),
                FacilityModel(title: Facilities.RestZone, isAvaiable: true),
                FacilityModel(title: Facilities.Parking, isAvaiable: false),
                FacilityModel(title: Facilities.WC, isAvaiable: true),
                FacilityModel(title: Facilities.CardPayment, isAvaiable: true)
            ],
        services: [
                ServiceModel(name: "wash", description: "test test hello test karl",
                    price: 100, duration: "00:30"),
                ServiceModel(name: "crash", description: "abc",
                    price: 500, duration: "00:50"),
                ServiceModel(name: "shower", description: "ffjeifj dsjwiow",
                    price: 150, duration: "00:10")
            ],
        contacts: [
                ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160",
                             email:"q@q.q", phone:"+78965930459"),
                ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160А",
                    email:"y@y.y", phone:"+78965930459"),
                ContactModel(location: "г Нижний Новгород, ул Максима Горького, д 160А/2",
                    email:"a@a.a", phone:"+78965930459")
            ])
    ]
    
    @State private var searchText = ""
    
    var body: some View {
        NavigationView {
            VStack {
                HStack{
                    Image(systemName: "magnifyingglass")

                    TextField("search", text: $searchText, onCommit: {
                        print("onCommit")
                    }).foregroundColor(.primary)

                    Button(action: {
                        self.searchText = ""
                    }) {
                        Image(systemName: "xmark.circle.fill")
                            .opacity(searchText == "" ? 0 : 1)
                    }
                }
                .padding(EdgeInsets(top: 8, leading: 6, bottom: 8, trailing: 6))
                .foregroundColor(.secondary)
                .background(Color(.secondarySystemBackground))
                .cornerRadius(10.0)
                .padding(.horizontal)
                List {
                    // Filtered list of carWashes
                    ForEach(carWashes.filter {
                        $0.name.hasPrefix(searchText) || searchText == ""
                        || $0.location.hasPrefix(searchText)
                        || $0.location.hasSuffix(searchText)}) {
                            cw in NavigationLink(destination: CarWashItemView(cw)) {
                                                    CarWashRow(cw.name,
                                                        address: cw.location,
                                                        isOpen: cw.isOpen)
                                                }
                    }
                }
                .navigationBarTitle(Text("Car Washes"))
            }
        }
    }
}
