open System.Net.Http

open System

let getOperation operation =
    match operation with
    | "+" -> "Plus"
    | "-" -> "Minus"
    | "*" -> "Multiply"
    | "/" -> "Divide"
    | _ -> operation

let sendRequestAsync(client: HttpClient) (requestUrl: string) =
    async {
        let! response = Async.AwaitTask (client.GetAsync requestUrl)
        let! returnContent = Async.AwaitTask (response.Content.ReadAsStringAsync())
        return returnContent
    }

[<EntryPoint>]
let main args =
    let inputData = Console.ReadLine()
    use handler = new HttpClientHandler()
    use httpClient = new HttpClient(handler)
    let args = inputData.Split(" ")
    match args.Length with
    | 3 ->
        let url = $"https://localhost:5001/calculate?value1={args[0]}&operation={args[1] |> getOperation}&value2={args[2]}"
        let responseNumber = Async.RunSynchronously(sendRequestAsync httpClient url)
        printfn $"Result %A{responseNumber}"
    | _ -> printfn "Some errors Oups :/"
    
    0