//
//  CarWashServiceMark.swift
//  CarWashService
//
//  Created by Anna Boykova on 14.12.2019.
//  Copyright © 2019 VXDESIGN.STORE. All rights reserved.
//

import MapKit

class CarWashServiceMark: NSObject, MKAnnotation {
  let title: String?
  // TODO:subtitle
  let coordinate: CLLocationCoordinate2D
  
  init(title: String, coordinate: CLLocationCoordinate2D) {
    self.title = title
    self.coordinate = coordinate
    
    super.init()
  }
}
