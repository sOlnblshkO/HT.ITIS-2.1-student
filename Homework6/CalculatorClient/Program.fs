open System
open System.Net
open System.Net.Http

let getInput () =
    printf "value1: "
    let value1 = Console.ReadLine()
    printf "operation: "
    let operation = Console.ReadLine()
    printf "value2: "
    let value2 = Console.ReadLine()
    (value1, operation, value2)

let send value1 (operation: string) value2 =
    async {
        use client = new HttpClient()

        let! response =
            client.GetAsync($"http://localhost:5000/calculate?value1={value1}&operation={operation}&value2={value2}")
            |> Async.AwaitTask

        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask

        return
            match response.StatusCode with
            | HttpStatusCode.OK -> content
            | _ -> $"Error: {content}"
    }

let rec queryLoop () =
    try
        async {
            let value1, operation, value2 = getInput ()
            let! result = send value1 operation value2
            printfn $"%s{result}"
        }
        |> Async.RunSynchronously
    with e ->
        printfn $"Error: {e.Message}"

    queryLoop ()

queryLoop ()
