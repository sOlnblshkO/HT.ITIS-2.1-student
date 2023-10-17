open System
open System.Net.Http

let sendRequest (url: string) : string = 
    let httpClient = new HttpClient()

    let responseAsync = async {
        let! response = httpClient.GetAsync(url) |> Async.AwaitTask
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }

    Async.RunSynchronously responseAsync

[<EntryPoint>]
let main argv =    
    let url = $"https://localhost:65174/calculate?value1={argv[0]}&operation={argv[1]}&value2={argv[2]}"
    let result = sendRequest url
    printfn "%s" result
    0
