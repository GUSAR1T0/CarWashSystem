//
// Created by Roman Mashenkin on 23.11.2019.
// Copyright (c) 2019 VXDESIGN.STORE. All rights reserved.
//

import Foundation

struct HttpClientService {
    public typealias ResponseCallback<Response: Codable> = (Response) -> Void
    private static let DefaultResponseCallback: (Any) -> Void = { _ in
    }
    private static let EmptyResponseCallback: (EmptyResponse) -> Void = { (response: EmptyResponse) in
    }
    public typealias ErrorCallback = (ErrorResult) -> Void
    private static let DefaultErrorCallback: (Any) -> Void = { _ in
    }
    private static let EmptyErrorCallback: (ErrorResult) -> Void = { (error: ErrorResult) in
    }

    private func prepareRequest(endpoint: String, method: String) throws -> URLRequest {
        guard let urlComponent = URLComponents(string: ServerConfiguration.host + endpoint), let url = urlComponent.url else {
            throw HttpClientError(message: "Couldn't create URL to make request")
        }

        var request = URLRequest(url: url)
        request.httpMethod = method
        return request
    }

    private static func addBodyToRequest<Request: Codable>(request: inout URLRequest, body: Request) {
        if let jsonData = try? JSONEncoder().encode(body) {
            request.setValue("application/json", forHTTPHeaderField: "Content-Type")
            request.setValue("application/json", forHTTPHeaderField: "Accept")
            request.httpBody = jsonData
        }
    }

    private func makeRequest<Response: Codable>(request: URLRequest, success: @escaping ResponseCallback<Response>, error: @escaping ErrorCallback) {
        URLSession.shared.dataTask(with: request) { data, response, exception in
            do {
                if exception != nil {
                    throw HttpClientError(message: "Couldn't send request / get response", error: exception)
                } else if Response.self != EmptyResponse.self, let data = data, let response = response as? HTTPURLResponse {
                    if response.statusCode < 400 {
                        guard let parsedResponse: Response = HttpClientService.parseResponse(data: data) else {
                            throw HttpClientError(message: "Couldn't parse successful response")
                        }
                        success(parsedResponse)
                    } else if response.statusCode != 401 {
                        guard let parsedResponse: ErrorResponse = HttpClientService.parseResponse(data: data) else {
                            throw HttpClientError(message: "Couldn't parse error response")
                        }
                        error(ErrorResult(reasonCode: response.statusCode, response: parsedResponse))
                    } else {
                        error(ErrorResult(reasonCode: 401))
                    }
                }
            } catch let exception {
                error(ErrorResult(httpClientError: exception))
            }
        }.resume()
    }

    private static func parseResponse<Response: Codable>(data: Data) -> Response? {
        do {
            let decoder = JSONDecoder()
            decoder.keyDecodingStrategy = .useDefaultKeys
            let response = try decoder.decode(Response.self, from: data)
            return response
        } catch {
            return nil
        }
    }

    func get<Response: Codable>(
            endpoint: String,
            success: @escaping ResponseCallback<Response> = HttpClientService.DefaultResponseCallback,
            error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback
    ) throws {
        let urlRequest = try! prepareRequest(endpoint: endpoint, method: HttpMethod.GET)
        makeRequest(request: urlRequest, success: success, error: error)
    }

    func get(endpoint: String, error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback) throws {
        try! get(endpoint: endpoint, success: HttpClientService.EmptyResponseCallback, error: error)
    }

    func post<Request: Codable, Response: Codable>(
            endpoint: String,
            request: Request,
            success: @escaping ResponseCallback<Response> = HttpClientService.DefaultResponseCallback,
            error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback
    ) throws {
        var urlRequest = try! prepareRequest(endpoint: endpoint, method: HttpMethod.POST)
        HttpClientService.addBodyToRequest(request: &urlRequest, body: request)
        makeRequest(request: urlRequest, success: success, error: error)
    }

    func post<Request: Codable>(endpoint: String, request: Request, error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback) throws {
        try! post(endpoint: endpoint, request: request, success: HttpClientService.EmptyResponseCallback, error: error)
    }

    func put<Request: Codable, Response: Codable>(
            endpoint: String,
            request: Request,
            success: @escaping ResponseCallback<Response> = HttpClientService.DefaultResponseCallback,
            error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback
    ) throws {
        var urlRequest = try! prepareRequest(endpoint: endpoint, method: HttpMethod.PUT)
        HttpClientService.addBodyToRequest(request: &urlRequest, body: request)
        makeRequest(request: urlRequest, success: success, error: error)
    }

    func put<Request: Codable>(endpoint: String, request: Request, error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback) throws {
        try! put(endpoint: endpoint, request: request, success: HttpClientService.EmptyResponseCallback, error: error)
    }

    func delete<Response: Codable>(
            endpoint: String,
            success: @escaping ResponseCallback<Response> = HttpClientService.DefaultResponseCallback,
            error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback
    ) throws {
        let urlRequest = try! prepareRequest(endpoint: endpoint, method: HttpMethod.DELETE)
        makeRequest(request: urlRequest, success: success, error: error)
    }

    func delete(endpoint: String, error: @escaping ErrorCallback = HttpClientService.DefaultErrorCallback) throws {
        try! delete(endpoint: endpoint, success: HttpClientService.EmptyResponseCallback, error: error)
    }
}
