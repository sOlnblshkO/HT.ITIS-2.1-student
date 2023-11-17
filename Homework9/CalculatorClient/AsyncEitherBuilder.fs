module CalculatorClient.AsyncEitherBuilder

type AsyncEitherBuilder() =
    member builder.Bind((x: Async<Result<'a, 'b>>), (f: 'a -> Async<Result<'c, 'b>>)) : Async<Result<'c, 'b>> =
        async {
            let! x = x

            match x with
            | Ok a -> return! f a
            | Error b -> return Error b
        }

    member builder.ReturnFrom(x) = async.ReturnFrom x

let asyncEither = AsyncEitherBuilder()
