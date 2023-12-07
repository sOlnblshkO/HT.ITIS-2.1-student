open System
open System.Net.Http

let parseOperation operation =
    match operation with
    | "+" -> "Plus"
    | "-" -> "Minus"
    | "*" -> "Multiply"
    | "/" -> "Divide"
    | _ -> operation

let sendRequestAsync(client : HttpClient) (url : string) =
    async {
        let! response = Async.AwaitTask (client.GetAsync url)
        let! res = Async.AwaitTask (response.Content.ReadAsStringAsync())
        return res
    }
    
[<EntryPoint>]
let main _ =
    let input = Console.ReadLine()
    use handler = new HttpClientHandler()
    use client = new HttpClient(handler)
    let args = input.Split(" ")
    if args.Length = 3 then
        let url = $"https://localhost:56752/calculate?value1={args[0]}&operation={parseOperation args[1]}&value2={args[2]}"
        let result = Async.RunSynchronously(sendRequestAsync client url)
        printf $"{result}" 
    0