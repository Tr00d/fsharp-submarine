namespace PetProject.Application

module Program =

    type Submarine =
        { Aim: int
          Position: int
          Depth: int }
        static member Default =
            { Aim = 0; Position = 0; Depth = 0 }

    type CommandType =
        | Up
        | Down
        | Forward

    type Command =
        { CommandType: CommandType
          Value: int }

    type RawCommand = { CommandName: string; Value: string }


    let logAim submarine =
        printfn $"Aim %i{submarine.Aim}"
        submarine

    let logPosition submarine =
        printfn $"Position %i{submarine.Position}"
        submarine

    let logDepth submarine =
        printfn $"Depth %i{submarine.Depth}"
        submarine

    let logState submarine =
        submarine |> logAim |> logPosition |> logDepth

    let moveUp submarine (command: Command) =
        { submarine with Aim = submarine.Aim - command.Value }


    let moveDown submarine (command: Command) =
        { submarine with Aim = submarine.Aim + command.Value }

    let moveForward submarine (command: Command) =
        { submarine with
            Position = submarine.Position + command.Value
            Depth = submarine.Depth + submarine.Aim * command.Value }

    let splitCommand (command: string) = command.Split(' ')

    let verifyCommandComposition command =
        let split = command |> splitCommand

        if split.Length = 2 then
            Some(command)
        else
            None

    let verifyCommandValue command =
        let split = command |> splitCommand

        if split[1] |> System.Int32.TryParse |> fst = true then
            Some(command)
        else
            None

    let convertCommand (command: string []) =
        match command[0] with
        | "down" ->
            Some(
                { CommandType = Down
                  Value = command[1] |> int }
            )
        | "up" ->
            Some(
                { CommandType = Up
                  Value = command[1] |> int }
            )
        | "forward" ->
            Some(
                { CommandType = Forward
                  Value = command[1] |> int }
            )
        | _ -> None

    let applyCommand submarine command =
        match command.CommandType with
        | Up -> moveUp submarine command
        | Down -> moveDown submarine command
        | Forward -> moveForward submarine command

    let moveSubmarine submarine command =
        match command with
        | None -> submarine
        | Some someCommand -> applyCommand submarine someCommand

    let parseCommand command =
        Some(command)
        |> Option.bind verifyCommandComposition
        |> Option.bind verifyCommandValue
        |> Option.map splitCommand
        |> Option.bind convertCommand

    let executeCommand (submarine: Submarine) (command: string) =
        command |> parseCommand |> moveSubmarine submarine

    let stringCommands =
        [ "down 2"
          "up"
          "forward 2"
          "up 2"
          "forward 1"
          "down 2"
          "forward 1"
          "backward 2"
          "up forward" ]


    stringCommands
    |> List.map parseCommand
    |> List.fold moveSubmarine Submarine.Default
    |> logState
    |> ignore
