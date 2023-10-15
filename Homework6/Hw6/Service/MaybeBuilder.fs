module Hw6.Service.MaybeBuilder

type MaybeBuilder() =
    member this.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok ok -> f ok
        | Error err -> Error err
    member this.Return x: Result<'a,'b> =
        Ok x
        
let maybe = MaybeBuilder()