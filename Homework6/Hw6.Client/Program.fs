module Hw6.Client
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
let main args =
    use handler = new HttpClientHandler()
    use client = new HttpClient(handler)
    let input = Console.ReadLine()
    let args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
    if args.Length = 3 then
        let url = $"http://localhost:5000/calculate?value1={args[0]}&operation={parseOperation args[1]}&value2={args[2]}";
        printfn $"{Async.RunSynchronously(sendRequestAsync client url)}"
    0