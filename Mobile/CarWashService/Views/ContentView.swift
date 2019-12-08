import SwiftUI

class Storage: ObservableObject {
    @Published var isAuthenticated = false
    @Published var clientProfile: ClientAuthenticationProfileModel? = nil

    init() {
        let service = HttpClientService()
        let semaphore = DispatchSemaphore(value: 0)
        try! service.get(endpoint: Requests.GetClientData, success: { (response: ClientAuthenticationProfileModel) in
            self.isAuthenticated = true
            self.clientProfile = response
            semaphore.signal()
        }, error: { (error: ErrorResult) in
            if error.reasonCode != 401 {
                print(error.response ?? error.httpClientError ?? "Unhandled exception")
            }
            try! service.delete(endpoint: Requests.SignOut)
            semaphore.signal()
        })
        semaphore.wait()
    }
}

struct ContentView: View {
    @EnvironmentObject var storage: Storage

    var body: some View {
        VStack {
            if !self.storage.isAuthenticated {
                SignInView()
            } else {
                MainView()
                        .animation(.spring())
                        .transition(.slide)
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
