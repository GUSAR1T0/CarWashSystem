//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import WebKit
import AuthenticationServices

struct ExternalSignInWebView: UIViewRepresentable {
    let url: String
    let schema: Int
    @State var webAuthSession: ASWebAuthenticationSession?

    func makeUIView(context: Context) -> WKWebView {
        WKWebView()
    }

    func updateUIView(_ uiView: WKWebView, context: Context) {
        uiView.load(URLRequest(url: URL(string: "\(url)?schema=\(self.schema)")!))
    }
}
