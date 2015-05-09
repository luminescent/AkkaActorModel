
#if INTERACTIVE
#I @"../../bin"

#r @"FsPickler.dll"
#r @"FSharp.PowerPack.Linq.dll"
#r @"Newtonsoft.Json.dll"
#r @"Akka.dll"
#r @"Akka.FSharp.dll"
#endif

open Akka.Actor
open Akka.FSharp
open Akka.Actor
open System

type Message =
    | Increment
    | Print

type SimpleActor () as this =
    inherit ReceiveActor ()

    let state = ref 0 // mutable is safe!!

    do
        this.Receive<Message>(fun m -> match m with
                                       | Print -> printfn "%i" !state
                                       | Increment -> state := !state + 1)


let system = ActorSystem.Create("example2")  

let actor = system.ActorOf<SimpleActor>()

actor.Tell Print
actor.Tell Increment
actor.Tell Increment
actor.Tell Increment
actor.Tell Print
//actor.Tell PoisonPill.Instance


//system.AwaitTermination()
