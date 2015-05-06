
let start (filePath:string) =
    System.Diagnostics.Process.Start(filePath) |> ignore

let chatServer = __SOURCE_DIRECTORY__ + "/../ChatServer/bin/Release/ChatServer.exe"
let chatClient = __SOURCE_DIRECTORY__ + "/../ChatClient/bin/Release/ChatClient.exe"

chatServer |> start
[0..2] |> Seq.iter (fun _ -> chatClient |> start)


