///<remark>
/// This class Generates prime numbers up to a user specified /// maximum. The algorithm used is the Sieve of Eratosthenes. /// Given an array of integers starting at 2:
/// Find the first uncrossed integer, and cross out all its /// multiples. Repeat until there are no more multiples
/// in the array.
///</remark>
using System;
public class PrimeGenerator {
private static int s; private static bool[] f; private static int[] primes;
public static int[] GeneratePrimeNumbers(int maxValue) {
    if (maxValue < 2)
      return new int[0];
else {
InitializeSieve(maxValue);
Sieve();
LoadPrimes();
return primes; // return the primes
} }
private static void LoadPrimes() {
int i;
int j;
// how many primes are there? int count = 0;
for (i = 0; i < s; i++)
{
      if (f[i])
        count++; // bump count.
}
primes = new int[count];
// move the
for (i = 0,
{
primes into the result j = 0; i < s; i++)
if (f[i])
  primes[j++] = i;
// if prime
￼
￼￼￼￼} }
private static void Sieve() {
int i;
int j;
for (i = 2; i < Math.Sqrt(s) + 1; i++) {
if(f[i]) // if i is uncrossed, cross its multiples. {
for (j = 2 * i; j < s; j += i)
f[j] = false; // multiple is not prime
} }
}
private static void InitializeSieve(int maxValue) {
// declarations
s = maxValue + 1; // size of array f = new bool[s];
int i;
// initialize array to true. for (i = 0; i < s; i++)
f[i] = true;
// get rid of known non-primes
f[0] = f[1] = false; }
}