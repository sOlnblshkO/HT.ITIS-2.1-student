module Hw5.MaybeBuilder

open System

type MaybeBuilder() =
    member this.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok s -> f s
        | Error e -> Error e
        
    member this.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()