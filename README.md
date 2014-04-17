UniqueRNG
=========

Unique Random Number Generator

Proof of Concept in C#.

Uses various methods to generate sequences of unique random numbers.

#Currently implemented methods

## Congruence using prime numbers

Uses a prime number close to 2^32 to create a one-to-one mapping of all numbers in the range of 0 to 2^32. This effectively creates a randomized sequence that's unique for the first 2^32 elements and starts repeating after that.

## Linear Feedback Shift Register

Uses bitwise shifting and logical operations to create a sequence of random numbers where the next number is dependant on the previous. This is effectively random-like and guarantees a unique sequence.

## PRBS31 (Pseudorandom binary sequence)

A more basic for of LFSR. Slower, but simpler. 

The sequences generated using PRBS31 start to repeat after 2^31 -1 / 31 elements.
