# FsharpLoadedDiceRoller

### About the project

This is a simple [executable](Program.fs) and [class](FsharpLoadedDiceRoller.fs) meant to help popularize the usage of the novel [Fast Loaded Dice Roller](https://arxiv.org/pdf/2003.03830.pdf) 
discrete sampling algorithm. 

### Usage
```
$ bin\Release\netcoreapp3.1\FsharpLoadedDiceRoller.exe --help
USAGE: FsharpLoadedDiceRoller [--help] [--rolls <rolls>] [--verbose] [--histogram <histogram>]
                              [--distribution [<distribution>...]] [--license]

OPTIONS:

    --rolls <rolls>       specify number of rolls to calculate (default: 100000).
    --verbose             specify to print result of roll.
    --histogram <histogram>
                          specify whether to print end histogram (default: true).
    --distribution [<distribution>...]
                          specify the weighted distribution to use (default: 0 1 2 3 4).
    --license             display license information about FsharpLoadedDiceRoller.
    --help                display this list of options.
```

Example command (weighted distribution and roll count):
```
$ bin\Release\netcoreapp3.1\FsharpLoadedDiceRoller.exe --distribution 0 1 2 3 4 --rolls 100000
Histogram results:
0 10232 19940 30008 39820
```