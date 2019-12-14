//
// Created by Roman Mashenkin on 14.12.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import SwiftUI
import Combine

struct RefreshableScrollView<Content: View>: View {
    @State public var locked = false
    @State public var refreshing = false

    private let content: () -> Content
    private let action: (@escaping () -> Void) -> Void

    public init(action: @escaping (@escaping () -> Void) -> Void, @ViewBuilder content: @escaping () -> Content) {
        self.action = action
        self.content = content
    }

    public var body: some View {
        ScrollView {
            VStack {
                GeometryReader { geometry in
                    HStack {
                        Spacer()
                        ActivityIndicator(isAnimating: self.$locked, style: .large).animation(.spring())
                        Text(!self.locked ? "Pull to refresh" : "Processing...")
                        Spacer()
                    }
                            .preference(key: RefreshableKeyTypes.RefreshablePreferenceKey.self, value: [RefreshableKeyTypes.RefreshablePreferenceData(bounds: geometry.frame(in: CoordinateSpace.global))])
                            .offset(y: -100)
                }.offset(y: -50)
                content().opacity(self.locked ? 0.5 : 1)
            }
        }
                .onPreferenceChange(RefreshableKeyTypes.RefreshablePreferenceKey.self) { values in
                    guard let bounds = values.first?.bounds else {
                        return
                    }
                    self.refresh(offsetY: bounds.origin.y)
                }
    }

    func refresh(offsetY: CGFloat) {
        if offsetY > 250 {
            if !self.refreshing {
                self.locked = true
            }
        } else if offsetY <= 140 {
            if self.locked && !self.refreshing {
                self.refreshing = true
                self.action() {
                    self.locked = false
                    self.refreshing = false
                }
            }
        }
    }
}

struct RefreshableKeyTypes {
    struct RefreshablePreferenceData: Equatable {
        let bounds: CGRect
    }

    struct RefreshablePreferenceKey: PreferenceKey {
        typealias Value = [RefreshablePreferenceData]
        static var defaultValue: Value = []

        static func reduce(value: inout Value, nextValue: () -> Value) {
            value.append(contentsOf: nextValue())
        }
    }
}

struct ActivityIndicator: UIViewRepresentable {
    @Binding var isAnimating: Bool
    let style: UIActivityIndicatorView.Style

    func makeUIView(context: UIViewRepresentableContext<ActivityIndicator>) -> UIActivityIndicatorView {
        UIActivityIndicatorView(style: style)
    }

    func updateUIView(_ uiView: UIActivityIndicatorView, context: UIViewRepresentableContext<ActivityIndicator>) {
        isAnimating ? uiView.startAnimating() : uiView.stopAnimating()
    }
}
