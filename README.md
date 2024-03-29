# FsharpLoadedDiceRoller

### About the project

This is a simple [executable](Program.fs) and [class](FsharpLoadedDiceRoller.fs) meant to help popularize the usage of the novel [Fast Loaded Dice Roller](https://arxiv.org/pdf/2003.03830.pdf)* 
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

### Citation
\* Original paper:
```
@inproceedings{saad2020fldr,
title           = {The Fast Loaded Dice Roller: A Near-optimal Exact Sampler for Discrete Probability Distributions},
author          = {Saad, Feras A. and Freer, Cameron E. and Rinard, Martin C. and Mansinghka, Vikash K.},
booktitle       = {AISTATS 2020: Proceedings of the 23rd International Conference on Artificial Intelligence and Statistics},
volume          = 108,
series          = {Proceedings of Machine Learning Research},
address         = {Palermo, Sicily, Italy},
publisher       = {PMLR},
year            = 2020,
keywords        = {random variate generation, sampling, discrete random variables},
abstract        = {This paper introduces a new algorithm for the fundamental problem of generating a random integer from a discrete probability distribution using a source of independent and unbiased random coin flips. This algorithm, which we call the Fast Loaded Dice Roller (FLDR), has efficient complexity properties in space and time: the size of the sampler is guaranteed to be linear in the number of bits needed to encode the target distribution and the sampler consumes (in expectation) at most 6.5 bits of entropy more than the information-theoretically minimal rate, independently of the values or size of the target distribution. We present an easy-to-implement, linear-time preprocessing algorithm and a fast implementation of the FLDR using unsigned integer arithmetic. Empirical evaluations establish that the FLDR is 2x--10x faster than multiple baseline algorithms for exact sampling, including the widely-used alias and interval samplers. It also uses up to 10000x less space than the information-theoretically optimal sampler, at the expense of a less than 1.5x runtime overhead.},
}
```
