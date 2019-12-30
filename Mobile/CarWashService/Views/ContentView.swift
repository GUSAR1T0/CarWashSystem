import SwiftUI

struct ContentView: View {
    @EnvironmentObject var authenticationStorage: AuthenticationStorage

    var body: some View {
        VStack {
            if !self.authenticationStorage.isAuthenticated {
                SignInView()
            } else {
                MainView()
                        .animation(.spring())
                        .transition(.slide)
            }
        }
        .edgesIgnoringSafeArea(.top)
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
