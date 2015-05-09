#if INTERACTIVE
#I @"../../bin"

#r @"Newtonsoft.Json.dll"
#r @"FsPickler.dll"
#r @"FSharp.PowerPack.Linq.dll"
#r @"Akka.dll"
#r @"Akka.FSharp.dll"
#endif

open System
open Akka.Actor
open Akka.Configuration
open Akka.FSharp


// #Simplify Actor
// There is a simpler way to define an Actor

let system = ActorSystem.Create("FSharp")
//let poisonPill = PoisonPill.Instance

let echoServer = 
    spawn system "EchoServer"
    <| fun mailbox ->
            actor {
                let! message = mailbox.Receive()
                match box message with
                | :? string as msg -> 
                                    printfn "Hello %s" msg
                                    system.Shutdown()
                | _ ->  failwith "unknown message"                
            } 

echoServer <! "F#!"


//system.Shutdown()
system.AwaitTermination()

printfn "Done"