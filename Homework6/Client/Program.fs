open System.Net.Http
open System

let sendRequest(args: string[]) =
    try
        async {
                let url = $"https://localhost:53884/calculate?value1={args[0]}&operation={args[1]}&value2={args[2]}"
                use client = new HttpClient()
                let! response = client.GetStringAsync(url) |> Async.AwaitTask
                printfn $"{response}"
                } |> Async.RunSynchronously
    with
    | ex -> printfn $"{ex.Message}"
        
        
[<EntryPoint>]
let main input =
    while true do
        let input = Console.ReadLine()
        let args = input.Split ' '
        match args.Length = 3 with
        | true -> sendRequest args
        | false -> printfn "Incorrect arguments"
    0
    
    