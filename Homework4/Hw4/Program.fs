let main args =
    let arg1 = args[0]
    let arg2 = args[2]
    let operation = args[1]
    
    printfn "%s" $"{arg1}{operation}{arg2}"