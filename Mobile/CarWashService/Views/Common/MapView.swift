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
    @State var isActionSheetActive: Binding<Bool>

    class MapViewCoordinator: NSObject, MKMapViewDelegate {
        var mapView: MapView

        init(_ mapView: MapView) {
            self.mapView = mapView
        }

        func mapView(_ mapView: MKMapView, viewFor annotation: MKAnnotation) -> MKAnnotationView? {
            let annotationView = MKMarkerAnnotationView(annotation: annotation, reuseIdentifier: "customView")
            annotationView.canShowCallout = true
            annotationView.markerTintColor = ApplicationColor.Primary.toUIColor()
            annotationView.rightCalloutAccessoryView = UIButton(type: .detailDisclosure)
            return annotationView
        }

        func mapView(_ mapView: MKMapView, annotationView view: MKAnnotationView, calloutAccessoryControlTapped control: UIControl) {
            guard (view.annotation as? CarWashServiceMark) != nil else {
                return
            }

            self.mapView.isActionSheetActive.wrappedValue = true
        }
    }

    func makeCoordinator() -> MapViewCoordinator {
        MapViewCoordinator(self)
    }

    func makeUIView(context: Context) -> MKMapView {
        let mapView = MKMapView(frame: .zero)
        mapView.delegate = context.coordinator
        return mapView
    }

    func updateUIView(_ view: MKMapView, context: Context) {
        if let coordinateX = self.coordinateX, let coordinateY = self.coordinateY, let title = self.title {
            let coordinateX = NSDecimalNumber(decimal: coordinateX)
            let coordinateY = NSDecimalNumber(decimal: coordinateY)
            let coordinate = CLLocationCoordinate2D(latitude: CLLocationDegrees(truncating: coordinateX), longitude: CLLocationDegrees(truncating: coordinateY))
            let span = MKCoordinateSpan(latitudeDelta: 0.01, longitudeDelta: 0.01)
            let region = MKCoordinateRegion(center: coordinate, span: span)
            view.setRegion(region, animated: true)
            let mark = CarWashServiceMark(title: title, coordinate: coordinate)
            view.addAnnotation(mark)
        }
    }
}

struct MapView_Previews: PreviewProvider {
    static var previews: some View {
        MapView(coordinateX: MapViewCustomPoint.coordinateX, coordinateY: MapViewCustomPoint.coordinateY, title: MapViewCustomPoint.name, isActionSheetActive: State(initialValue: false).projectedValue)
    }
}
