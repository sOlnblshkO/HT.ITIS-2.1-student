module Hw6Client.Program 
open System
open System.Net.Http
open System.Diagnostics.CodeAnalysis

[<ExcludeFromCodeCoverage>]
let convertOperation input = 
    match input with
    | "+" -> "Plus"
    | "-" -> "Minus"
    | "*" -> "Multiply"
    | "/" -> "Divide"
    | _ -> input

[<ExcludeFromCodeCoverage>]
let checkInput (input : string) =
    let args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
    match args.Length with
    | 3 -> Ok args
    | _ -> Error "Wrong length"
    
let getAsync (client:HttpClient) (url:string) = 
    async {
        let! response = client.GetAsync(url) |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }

 
[<ExcludeFromCodeCoverage>]  
[<EntryPoint>]
let main arg =
    while (true) do 
        let input =  Console.ReadLine() |> checkInput
        use httpClient = new HttpClient()
        match input with
        | Ok args ->
            let url = $"http://localhost:5000/calculate?value1={args[0]}&operation={args[1] |> convertOperation}&value2={args[2]}";
            printfn $"Result: {getAsync httpClient url |> Async.RunSynchronously}"
        | Error error -> printfn $"{error}"
    0