open System
open System.Net
open System.Net.Http
open System.Text
open System.Text.Json
open CalculatorClient.AsyncEitherBuilder

type Response =
    { isSuccess: bool
      errorMessage: string
      result: double }

let getInput () = Console.ReadLine()

let deserialize responseStream =
    async {
        try
            let! parsedResponse =
                (JsonSerializer.DeserializeAsync<Response> responseStream).AsTask()
                |> Async.AwaitTask

            return Ok parsedResponse
        with :? JsonException ->
            return Error "Could not parse response"
    }

let normalize (response: Response) =
    async {
        if response.isSuccess then
            return Ok response.result
        else
            return Error response.errorMessage
    }

let send expression =
    async {
        use client = new HttpClient()

        try
            let! response =
                client.PostAsync(
                    "http://localhost:5190/Calculator/CalculateMathExpression",
                    new StringContent(
                        $"expression={WebUtility.UrlEncode expression}",
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded"
                    )
                )
                |> Async.AwaitTask

            match response.StatusCode with
            | HttpStatusCode.OK ->
                let! content = response.Content.ReadAsStreamAsync() |> Async.AwaitTask
                return Ok content
            | other -> return Error $"Request returned status code {other}"
        with :? HttpRequestException ->
            return Error "Failed to send request"
    }

let rec queryLoop () =
    let result =
        asyncEither {
            let expression = getInput ()
            let! contentStream = send expression
            let! response = deserialize contentStream
            return! normalize response
        }
        |> Async.RunSynchronously

    match result with
    | Ok result -> printfn $"Result: {result}"
    | Error error -> printfn $"Error: {error}"

    queryLoop ()

queryLoop ()
