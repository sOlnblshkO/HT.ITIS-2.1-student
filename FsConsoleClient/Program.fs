open System
open System.Net.Http

let sendRequest (url: string) = 
    let httpClient = new HttpClient()

    let responseAsync = async {
        let! response = httpClient.GetAsync(url) |> Async.AwaitTask
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        if response.IsSuccessStatusCode then
            let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
            return Some content
        else return None
    }

    Async.RunSynchronously responseAsync

[<EntryPoint>]
let main argv =    
    let url = $"https://localhost:65174/calculate?value1={argv[0]}&operation={argv[1]}&value2={argv[2]}"
    match sendRequest url with
    | Some content -> printfn "%s" content
    | None -> printfn "Failed to get data from %s" url
    0
