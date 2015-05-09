

let fractalRemote = __SOURCE_DIRECTORY__ + "/../Fractal/Fractal.Remote/bin/Release/Fractal.Remote.exe"       
let fractal = __SOURCE_DIRECTORY__ + "/../Fractal/Fractal/bin/Release/Fractal.exe"     

let start (filePath:string) =
    System.Diagnostics.Process.Start(filePath) |> ignore


start fractalRemote
start fractal 