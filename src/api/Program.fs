namespace Api

module Program = 
    open Suave

    [<EntryPoint>]
    let main argv =
        let serviceInstanceId = System.Guid.NewGuid().ToString()
        startWebServer { 
            defaultConfig 
                with bindings = [HttpBinding.createSimple HTTP "0.0.0.0" 8080]} 
            (sprintf "Hello World! Instance ID: %s" serviceInstanceId |> Successful.OK)
        0 
