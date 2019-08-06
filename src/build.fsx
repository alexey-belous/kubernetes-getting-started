#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

Target.create "Clean" (fun _ ->
    !! "**/bin"
    ++ "**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "Publish" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter (DotNet.publish id)
)

Target.create "CleanupPack" (fun _ -> 
  Shell.Exec ("docker", "rmi alexeybelous/kubelesson-api:latest --force", "./api") |> ignore
)

Target.create "Pack" (fun _ -> 
  if Shell.Exec ("docker", "build -t alexeybelous/kubelesson-api:latest .", "./api") <> 0
  then failwith "docker build command failed"
  else ()
)

Target.create "Push" (fun _ -> 
  if Shell.Exec ("docker", "push alexeybelous/kubelesson-api:latest", null) <> 0
  then failwith "docker push command failed"
  else ()
)

Target.create "All" ignore

"Clean"
  ==> "Build"
  ==> "All"

"Clean"
  ==> "Publish"
  ==> "CleanupPack"
  ==> "Pack"
  ==> "Push"

Target.runOrDefault "All"
