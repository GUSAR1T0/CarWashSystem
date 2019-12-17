//
//  MapView.swift
//  CarWashService
//
//  Created by Anna Boykova on 13.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import MapKit

struct MapView: UIViewRepresentable {
    public var coordinateX: Decimal?
    public var coordinateY: Decimal?
    public var title: String?

    func makeUIView(context: Context) -> MKMapView {
        MKMapView(frame: .zero)
    }

    func updateUIView(_ view: MKMapView, context: Context) {
        if let coordinateX = coordinateX, let coordinateY = coordinateY, let title = title {
            let coordinateX = NSDecimalNumber(decimal: coordinateX)
            let coordinateY = NSDecimalNumber(decimal: coordinateY)
            let coordinate = CLLocationCoordinate2D(latitude: CLLocationDegrees(truncating: coordinateX), longitude: CLLocationDegrees(truncating: coordinateY))
            let span = MKCoordinateSpan(latitudeDelta: 0.005, longitudeDelta: 0.005)
            let region = MKCoordinateRegion(center: coordinate, span: span)
            view.setRegion(region, animated: true)
            let mark = CarWashServiceMark(title: title, coordinate: coordinate)
            view.addAnnotation(mark)
        }
    }
}

struct MapView_Previews: PreviewProvider {
    static var previews: some View {
        MapView(coordinateX: 56.327056, coordinateY: 56.327056, title: "Nizhny Novgorod Kremlin")
    }
}
