let license = """
FsharpLoadedDiceRoller - Sample executable for performing the novel Fast Loaded Dice Roller algorithm (https://arxiv.org/pdf/2003.03830.pdf)
Copyright (C) 2020  Ryan Andersen

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.
"""

open System
open Argu

open FsharpLoadedDiceRoller

let defaultRollCount = 100_000
let defaultDistribution = [|0; 1; 2; 3; 4|]
let defaultPrintHist = true

let arrayToString (arr: 'T[]) =
    let sb = new Text.StringBuilder ()
    if arr.Length > 0 then
        sb.Append (arr.[0]) |> ignore
        for i = 1 to arr.Length - 1 do
            sb.Append ' ' |> ignore
            sb.Append (arr.[i]) |> ignore
    sb.ToString ()

type Arguments =
    | Rolls of rolls:uint32
    | Verbose
    | Histogram of histogram:bool
    | Distribution of distribution:int list
    | License

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Rolls _ -> $"specify number of rolls to calculate (default: %i{defaultRollCount})."
            | Verbose -> "specify to print result of roll."
            | Histogram _ -> $"specify whether to print end histogram (default: %b{defaultPrintHist})."
            | Distribution _ -> $"specify the weighted distribution to use (default: %s{arrayToString defaultDistribution})."
            | License -> "display license information about FsharpLoadedDiceRoller."
            | _ -> raise (new Exception "incomplete interface!!")

[<EntryPoint>]
let main argv =
    // Setup simple PRNG
    let rand = new Random ()
    let flipper = { new FairCoin with member _.Flip () = (rand.Next ()) % 2 = 0 }

    // Parse command-line inputs
    let parser =  ArgumentParser.Create<Arguments>(programName = "FsharpLoadedDiceRoller")
    let args = parser.Parse (argv, raiseOnUsage=false)
    if args.IsUsageRequested then printfn "%s" (parser.PrintUsage(programName = "FsharpLoadedDiceRoller")); exit 0
    if args.Contains License then printfn "%s" license; exit 0
    let dist =
        match args.TryGetResult Distribution with
        | Some d -> Array.ofList d
        | None -> defaultDistribution
    let hist = Array.zeroCreate (dist.Length)
    let N =
        match args.TryGetResult Rolls with
        | Some rolls -> int rolls
        | None -> defaultRollCount
    let verbose = args.Contains Verbose
    let printHist = 
        match args.TryGetResult Histogram with
        | Some h -> h
        | None -> defaultPrintHist

    // Let 'er Roll!
    let roller = new FLDRoller (flipper, dist)
    for _ = 1 to N do
        let s = roller.Sample ()
        if verbose then printfn "%i" s
        hist.[s] <- hist.[s] + 1

    // Print histogram
    if printHist then
        printfn "Histogram results:"
        printfn "%s" (arrayToString hist)
    0