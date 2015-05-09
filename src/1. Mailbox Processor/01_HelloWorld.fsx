// Create Console-application, then NuGet: Install-Package Akka
//module AkkaConsoleApplication = 

#if INTERACTIVE
#I @"../../bin"

#r @"Newtonsoft.Json.dll"
#r @"FsPickler.dll"
#r @"FSharp.PowerPack.Linq.dll"
#r @"Akka.dll"
#r @"Akka.FSharp.dll"
#endif

open Akka.Actor
open Akka
open Akka.FSharp
open Newtonsoft.Json
open System

type GreetMsg =
    | Greet of who:string


let system = System.create "MySystem" <| Configuration.load()

    
// functional 
let greeter = 
    // the function spawn instantiate an ActorRef
    // spawn attaches the behavior to our system and returns an ActorRef
    // We can use ActorRef to pass messages

    // ActorFactory -> Name -> f(Actor<Message> -> Cont<'Message, 'return>) -> ActorRef
    spawn system "Greeter-Functional"
    <| fun mailbox ->
        let rec loop() = actor { // tail recursive function,
                                 // which uses an actor { ... } computation expression 
            let! msg = mailbox.Receive()
            match msg with
            | Greet(w) ->printfn "Hello %s" w                
            return! loop() }
        loop()

greeter <! GreetMsg.Greet("AKKA.Net!!")

let actor = select "akka://MySystem/user/Greeter-Functional" system
actor <! GreetMsg.Greet("AKKA.Net!!")


system.Shutdown()


// #Using Actor
// Actors are one of Akka's concurrent models.
// An Actor is a like a thread instance with a mailbox. 
// It can be created with system.ActorOf: use receive to get a message, and <! to send a message.
// This example is an EchoServer which can receive messages then print them.

