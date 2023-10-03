
[<EntryPoint>]
let main args =
    let arg1 = args[0]
    let operation = args[1]
    let arg2 = args[2]
    
    printfn "%s" $"{arg1}{operation}{arg2}"
    0