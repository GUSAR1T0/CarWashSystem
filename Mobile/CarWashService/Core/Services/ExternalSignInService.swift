//
// Created by Roman Mashenkin on 25.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import AuthenticationServices

class ExternalSignInContextProvider: NSObject {
    private weak var anchor: ASPresentationAnchor!

    init(_ anchor: ASPresentationAnchor) {
        self.anchor = anchor
    }
}

extension ExternalSignInContextProvider: ASWebAuthenticationPresentationContextProviding {
    func presentationAnchor(for session: ASWebAuthenticationSession) -> ASPresentationAnchor {
        anchor
    }
}

protocol ExternalSignInHandler: class {
    var session: ASWebAuthenticationSession? { get set }
    var contextProvider: ExternalSignInContextProvider? { get set }

    func externalSignIn(url: URL, callbackScheme: String, completion: @escaping ASWebAuthenticationSession.CompletionHandler)
}

extension ExternalSignInHandler {
    func externalSignIn(url: URL, callbackScheme: String, completion: @escaping ASWebAuthenticationSession.CompletionHandler) {
        let session = ASWebAuthenticationSession(url: url, callbackURLScheme: callbackScheme) { url, error in
            completion(url, error)
        }

        session.presentationContextProvider = contextProvider
        session.prefersEphemeralWebBrowserSession = true
        session.start()
        self.session = session
    }
}

class ExternalSignInService: ExternalSignInHandler {
    var session: ASWebAuthenticationSession? = nil
    var contextProvider: ExternalSignInContextProvider?

    func signIn(schema: Int, success: @escaping (String) -> Void, error: @escaping (String) -> Void) -> Void {
        let externalSignInUrl = URL(string: "\(ServerConfiguration.host)\(Requests.InitializeExternalSignIn)?schema=\(schema)")!
        externalSignIn(url: externalSignInUrl, callbackScheme: Requests.RedirectExternalSignIn) { url, exception in
            if let exception = exception {
                error(exception.localizedDescription)
            } else if let url = url {
                guard let components = URLComponents(url: url, resolvingAgainstBaseURL: false),
                      let item = components.queryItems?.first(where: { $0.name == "token" }),
                      let token = item.value else {
                    error("Couldn't get token from server")
                    return
                }
                success(token)
            }
        }
    }
}
