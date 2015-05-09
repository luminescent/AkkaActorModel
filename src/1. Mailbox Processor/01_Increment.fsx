﻿#if INTERACTIVE
#I @"../../bin"

#r @"Newtonsoft.Json.dll"
#r @"FsPickler.dll"
#r @"FSharp.PowerPack.Linq.dll"
#r @"Akka.dll"
#r @"Akka.FSharp.dll"
#endif

open Akka.FSharp
open Akka.Actor
open System

type Message =
    | Increment
    | Print

type OtherMessage =
    | Decrement
    | Display

type SimpleActor() as this =
    inherit ReceiveActor()  

    let state = ref 0 // mutable is safe!!

    do
        // Specialize receive
        this.Receive<Message>(fun m -> 
                                match m with
                                | Print -> printfn "%i" !state
                                | Increment -> state := !state + 1)

        this.Receive<OtherMessage>(fun m -> 
                                match m with
                                | Display -> printfn "%i" !state
                                | Decrement -> state := !state - 1)


    override this.Unhandled(msg:obj) = // can be used for dead letter
            printfn "What should I do with this thing: %A" msg //(msg.GetType())



let system = ActorSystem.Create("example2")  

let actor = system.ActorOf<SimpleActor>("SimpleActor")

actor.Tell Print // Message
actor.Tell Increment
actor.Tell Increment
actor.Tell Increment
actor.Tell Print

actor <! Decrement  // OtherMessage
actor <! Decrement
actor <! Display

actor <! "ciao"


system.AwaitTermination()

printfn "Done"




