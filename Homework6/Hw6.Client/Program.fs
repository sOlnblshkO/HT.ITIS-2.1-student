open System.Net.Http

let sendRequestAsync(client : HttpClient) (url : string) =
    async {
        let! response = Async.AwaitTask (client.GetAsync url)
        let! res = Async.AwaitTask (response.Content.ReadAsStringAsync())
        return res
    }
    
let solve =
    use handler = new HttpClientHandler()
    use client = new HttpClient(handler)
    let args = [|"10"; "Plus"; "5"|]
    let url = $"http://localhost:5000/calculate?value1={args[0]}&operation={args[1]}&value2={args[2]}";
    printfn $"{Async.RunSynchronously(sendRequestAsync client url)}"