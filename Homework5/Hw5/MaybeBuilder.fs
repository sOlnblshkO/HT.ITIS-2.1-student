module Hw5.MaybeBuilder


type MaybeBuilder() =
    member builder.Bind(a, f) : Result<'e, 'd> =
        match a with
        | Ok okValue -> f okValue
        | Error errorValue -> Error errorValue

    member builder.Return x : Result<'a, 'b> = Ok x

let maybe = MaybeBuilder()
