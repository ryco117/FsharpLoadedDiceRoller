(*
This file is part of FsharpLoadedDiceRoller

FsharpLoadedDiceRoller is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

FsharpLoadedDiceRoller is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with FsharpLoadedDiceRoller. If not, see <https://www.gnu.org/licenses/>.
*)
(*
Original paper detailing algorithm for The Fast Loaded Dice Roller (FLDR)
https://arxiv.org/pdf/2003.03830.pdf

Official C and Python implementations
https://github.com/probcomp/fast-loaded-dice-roller
*)

namespace FsharpLoadedDiceRoller

type FairCoin =
    abstract Flip: unit -> bool

type FLDRoller(fairCoin: FairCoin, dist: int[]) =
    let n = dist.Length
    let m = Array.fold (fun acc x -> acc + x) 0 dist
    let k =
        let rec f x i =
            if x = 1 then i else f (x >>> 1) (i + 1) in
        let k' = f m 0
        if (1 <<< k') < m then k' + 1 else k'
    let arr = Array.zeroCreate (n + 1)
    do Array.iteri (fun i _ -> arr.[i] <- if i < n then dist.[i] else (1 <<< k) - m) arr
    let h = Array.zeroCreate k
    let H = Array.zeroCreate (n + 1)
    do Array.iteri (fun i _ -> H.[i] <- Array.zeroCreate k) H
    do for j = 0 to k - 1 do
        let mutable d = 0
        for i = 0 to n do
            let w = (arr.[i] >>> (k - 1) - j) &&& 1
            h.[j] <- h.[j] + w
            if w > 0 then
                H.[d].[j] <- i
                d <- d + 1

    member _.Sample() =
        let mutable d = 0
        let mutable c = 0
        let rec looper () =
            let b = fairCoin.Flip ()
            d <- 2 * d + (if b then 0 else 1)
            if d < h.[c] then
                if H.[int d].[c] < n then
                    H.[int d].[c]
                else
                    d <- 0
                    c <- 0
                    looper ()
            else
                d <- d - h.[c]
                c <- c + 1
                looper ()
        looper ()