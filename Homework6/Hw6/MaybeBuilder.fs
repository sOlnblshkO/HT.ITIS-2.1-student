module Hw6.MaybeBuilder

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Error message -> Error message
        | Ok a -> f a
    member builder.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()