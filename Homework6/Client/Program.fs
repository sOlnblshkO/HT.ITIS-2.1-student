open System
open System.Net.Http

let convertOperation operation =
    match operation with
    | "+" -> "Plus"
    | "-" -> "Minus"
    | "*" -> "Multiply"
    | "/" -> "Divide"
    | _ -> operation


let sendRequestAsync(client: HttpClient, url: string) =
    async {
        let! response = Async.AwaitTask(client.GetStringAsync(url))
        return response
    }


[<EntryPoint>]
let main args =
    let input = Console.ReadLine()
    let args = input.Split(" ")
    let operation = convertOperation args[1]
    let client = new HttpClient()
    match args.Length with
    | 3 ->
        let url = $"http://localhost:58737/calculate?value1={args[0]}&operation={operation}&value2={args[2]}"
        let result = Async.RunSynchronously(sendRequestAsync(client, url))
        printfn $"Result: {result}"
    | _ -> printfn "Something went wrong"
    
    0