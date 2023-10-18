open System
open System.Net.Http

let parseOperation operation =
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
    printf "введите выражение через пробелы: "
    let input = Console.ReadLine()
    let arr = input.Split(" ")
    let operation = parseOperation arr[1]
    let client = new HttpClient()
    
    if arr.Length = 3 then
        let url = $"http://localhost:5000/calculate?value1={arr[0]}&operation={operation}&value2={arr[2]}"
        let result = sendRequestAsync(client, url) |> Async.RunSynchronously
        printfn $"ваш результат: {result}"
    else
        printfn "ошибка исполнения запроса"
    0    