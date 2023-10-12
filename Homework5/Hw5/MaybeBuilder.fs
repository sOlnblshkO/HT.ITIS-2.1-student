module Hw5.MaybeBuilder

open System

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok x -> f x
        | Error e -> Error e
    member builder.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()