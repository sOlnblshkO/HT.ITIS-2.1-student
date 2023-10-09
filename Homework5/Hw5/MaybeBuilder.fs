module Hw5.MaybeBuilder

open System

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok value -> f a
        | Error err -> Error err
    member builder.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()