//
// Created by Roman Mashenkin on 05.01.2020.
// Copyright (c) 2020 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI

struct CarWashOrderClientView: View {
    @EnvironmentObject private var lookupStorage: LookupStorage
    @State private var isCarModelChooseModalActive = false
    @Binding var selectedClientCarId: Int?
    @Binding var clientCarList: [ClientCarModel]
    @Binding var workingHours: WorkingHoursModel?
    @Binding var clientBlockIsCompleted: Bool
    @State private var isLoaded = false

    @State private var clientCar: ClientCarModel? = nil

    @State private var date = Date()
    @State private var hour = 0
    @State private var minute = 0

    private var selectedClientCarIdProxy: Binding<Int?> {
        Binding<Int?>(get: { self.selectedClientCarId }, set: {
            self.selectedClientCarId = $0
            self.clientCar = self.clientCarList.first(where: { car in car.id == self.selectedClientCarId })
            self.clientBlockIsCompleted = self.clientCar != nil && self.dateTimeState
        })
    }

    private var dateProxy: Binding<Date> {
        Binding<Date>(get: { self.date }, set: {
            self.date = $0
            self.dateTimeState = self.defineState()
            self.clientBlockIsCompleted = self.clientCar != nil && self.dateTimeState
        })
    }

    private var hourProxy: Binding<Int> {
        Binding<Int>(get: { self.hour }, set: {
            self.hour = $0
            self.dateTimeState = self.defineState()
            self.clientBlockIsCompleted = self.clientCar != nil && self.dateTimeState
        })
    }

    private var minuteProxy: Binding<Int> {
        Binding<Int>(get: { self.minute }, set: {
            self.minute = $0
            self.dateTimeState = self.defineState()
            self.clientBlockIsCompleted = self.clientCar != nil && self.dateTimeState
        })
    }

    @State private var dateTimeState = false

    private let hours = 0...23
    private let minutes = [0, 15, 30, 45]

    private let clientProfileController = ClientProfileController()

    private func fillModels(_ models: [ClientCarModel]) {
        self.clientCarList.removeAll()
        for model in models {
            self.clientCarList.append(model)
        }
    }

    private func getClientCarString(clientCar: ClientCarModel?) -> String? {
        self.lookupStorage.getCarModelNameWithGovernmentPlate(modelId: clientCar?.modelId, governmentPlate: clientCar?.governmentPlate)
    }

    private func getTimeRange(_ date: Date) -> (String?, String?) {
        guard let weekday = DateTimeUtils.getDayOfWeek(date) else {
            return (nil, nil)
        }

        switch (weekday) {
        case .monday:
            return (workingHours?.monday.startTime, workingHours?.monday.stopTime)
        case .tuesday:
            return (workingHours?.tuesday.startTime, workingHours?.tuesday.stopTime)
        case .wednesday:
            return (workingHours?.wednesday.startTime, workingHours?.wednesday.stopTime)
        case .thursday:
            return (workingHours?.thursday.startTime, workingHours?.thursday.stopTime)
        case .friday:
            return (workingHours?.friday.startTime, workingHours?.friday.stopTime)
        case .saturday:
            return (workingHours?.saturday.startTime, workingHours?.saturday.stopTime)
        case .sunday:
            return (workingHours?.sunday.startTime, workingHours?.sunday.stopTime)
        }
    }

    private func defineState() -> Bool {
        let now = Date()
        var nowHour: Int? = nil
        var nowMinute: Int? = nil
        let formatter = DateFormatter()
        formatter.dateFormat = "yyyy-MM-dd"
        let datePossibleStr = formatter.string(from: self.date)
        let dateNowStr = formatter.string(from: now)

        if datePossibleStr == dateNowStr {
            formatter.dateFormat = "HH:mm"
            let timeNowStr = formatter.string(from: now)
            nowHour = Int(String(timeNowStr.prefix(2)))
            nowMinute = Int(String(timeNowStr.suffix(2)))
        }

        let timeRange = self.getTimeRange(self.date)
        if let start = timeRange.0, let stop = timeRange.1 {
            if let startHourFromRange = Int(String(start.prefix(2))), let startMinuteFromRange = Int(String(start.suffix(2))),
               let stopHourFromRange = Int(String(stop.prefix(2))), let stopMinute = Int(String(stop.suffix(2))) {
                let startHour = nowHour != nil && nowHour! > startHourFromRange ? nowHour! : startHourFromRange
                let startMinute = nowMinute != nil && nowMinute! > startMinuteFromRange ? nowMinute! : startMinuteFromRange
                let stopHour = stopHourFromRange == 0 ? 24 : stopHourFromRange

                if startHour == 0 && stopHour == 24 {
                    if startMinute == 0 && stopMinute == 0 {
                        return true
                    }

                    if startMinute <= self.minute && self.minute <= stopMinute {
                        return true
                    }
                }

                if startHour < self.hour && self.hour < stopHour {
                    return true
                }

                if startHour == self.hour && startMinute <= self.minute && self.minute <= 45 {
                    return true
                }

                if stopHour == self.hour && 0 <= self.minute && self.minute <= stopMinute {
                    return true
                }
            }
        }

        return false
    }

    var body: some View {
        VStack {
            TitledContainer("Car") {
                Button(action: {
                    self.isCarModelChooseModalActive.toggle()
                }) {
                    HStack {
                        Text(self.getClientCarString(clientCar: self.clientCar) ?? "Choose the car")
                                .foregroundColor(.black)
                        Spacer()
                        Image(systemName: "chevron.right")
                                .font(.system(size: 18, weight: .bold))
                                .foregroundColor(.black)
                    }
                            .padding(.horizontal)
                            .padding(.top, 5)
                            .padding(.bottom, 5)
                }.sheet(isPresented: self.$isCarModelChooseModalActive) {
                    NavigationView {
                        CarForOrderSelector(isCarModelChooseModalActive: self.$isCarModelChooseModalActive, selectedClientCarId: self.selectedClientCarIdProxy, clientCarList: self.$clientCarList)
                                .environmentObject(self.lookupStorage)
                    }.accentColor(ApplicationColor.Primary.toColor())
                }
            }
                    .padding(.bottom, 10)
            TitledContainer("Date and Time") {
                VStack {
                    GeometryReader { geometry in
                        HStack {
                            DatePicker("Date", selection: self.dateProxy, in: Date().addingTimeInterval(3600)..., displayedComponents: .date)
                                    .labelsHidden()
                                    .frame(maxWidth: geometry.size.width * 2 / 3)
                                    .clipped()
                            Picker("Hours", selection: self.hourProxy) {
                                ForEach(self.hours, id: \.self) { hour in
                                    Text((hour < 10 ? "0" : "") + String(hour))
                                }
                            }
                                    .labelsHidden()
                                    .frame(maxWidth: geometry.size.width / 10)
                                    .clipped()
                            Picker("Minutes", selection: self.minuteProxy) {
                                ForEach(self.minutes, id: \.self) { minute in
                                    Text((minute == 0 ? "0" : "") + String(minute))
                                }
                            }
                                    .labelsHidden()
                                    .frame(maxWidth: geometry.size.width / 10)
                                    .clipped()
                        }
                                .padding()
                    }
                            .frame(height: 250)
                    OrderDateStatusView(isDateActive: self.dateTimeState)
                            .padding()
                            .offset(y: -10)
                }
            }
        }
                .onAppear {
                    self.clientCar = self.clientCarList.first(where: { car in car.id == self.selectedClientCarId })
                    self.dateTimeState = self.defineState()
                    self.clientBlockIsCompleted = self.clientCar != nil && self.dateTimeState
                    self.clientProfileController.getAllCars(isLoaded: self.$isLoaded) { models in
                        self.fillModels(models)
                    }
                }
    }
}
