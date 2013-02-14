C# Refactoring Example
======================

Credits to Robert C. Martin for the example code and walkthrough.
For those curious about the Algorithm used here, it is explained on
[Wikipedia](http://en.wikipedia.org/wiki/Sieve_of_Eratosthenes)

Original Code([V1](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V1/GeneratePrimes.cs))
------------------

Consider the PrimeGenerator code in [V1](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V1/GeneratePrimes.cs). This program generates prime numbers. 
It is one big function with many single-letter variables and comments to help us read it.

The unit test for GeneratePrimes is shown in [here](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V1/GeneratePrimes.cs). It takes a statistical approach, checking whether the generator can generate primes up to 0, 2, 3, and 100. In the first case, there should be
￼￼￼￼￼￼￼no primes. In the second, there should be one prime, and it should be 2. In the third, there should be two primes, and they should be 2 and 3. In the last case, there should be 25 primes, the last of which is 97. If all these tests pass, I make the assumption that the generator is working. 
I doubt that this is foolproof, but I can't think of a reasonable scenario in which these tests would pass but the function fail.

Refactoring Step 1([V2](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V2/GeneratePrimesV2.cs)):
-----------------------

It seems pretty clear that the main function wants to be three separate functions. 

The first initializes all the variables and sets up the sieve. The second executes the sieve, and the third loads the sieved results into an integer array. 

To expose this structure more clearly, I extracted those functions into three separate methods [V2](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V2/GeneratePrimesV2.cs). 

I also removed a few unnecessary comments and changed the name of the class to PrimeGenerator. The tests all still ran.
Extracting the three functions forced me to promote some of the variables of the function to static fields of the class. 
This makes it much clearer which variables are local and which have wider influence.

Refactoring Step 2([V3](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V3/GeneratePrimesV3.cs)):
------------------------

The InitializeSieve function is a little messy, so I cleaned it up considerably (in [V3](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V3/GeneratePrimesV3.cs)). First, I replaced all usages of the s variable with f.Length. 
Then I changed the names of the three functions to something a bit more expressive. Finally, I rearranged the innards of InitializeArrayOfIntegers (née InitializeSieve) to be a little nicer to read. The tests all still ran.

Refactoring Step 3 ([V4](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V4/GeneratePrimeTest.cs)):
------------------------

Next, I looked at CrossOutMultiples. There were a number of statements in this function, and in others, of the form 
```if(f[i] == true) ```. 

The intent was to check whether i was uncrossed, so I changed the name of f to unCrossed. But this led to ugly statements, such as:
``` unCrossed[i] = false```. 

I found the double negative confusing. So I changed the name of the array to isCrossed and changed the sense of all the Booleans. The tests all still ran.
I got rid of the initialization that set isCrossed[0] and isCrossed[1] to true and simply made sure that no part of the function used the isCrossed array for indexes less than 2. I extracted the inner loop of the CrossOutMultiples function and called it CrossOutMultiplesOf. I also thought that if (isCrossed[i] == false) was confusing, so I created a function called NotCrossed and changed the if statement to if (NotCrossed(i)). 

The tests all still ran. I spent a bit of time writing a comment that tried to explain why you have to iterate only up to the square root of the array size. This led me to extract the calculation into a function where I could put the explanatory comment. In writing the comment, I realized that the square root is the maximum prime factor of any of the integers in the array. So I chose that name for the variables and functions that dealt with it. The result of all these refactorings are in [V4](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V4/GeneratePrimeTest.cs). The tests all still ran.

Refactoring Step 4([V5](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V5/GeneratePrimesV5.cs)):
-------------------------

The last function to refactor is PutUncrossedIntegersIntoResult. This method has two parts. The first counts the number of uncrossed integers in the array and creates the result array of that size. The second moves the uncrossed integers into the result array. I extracted the first part into its own function and did some miscellaneous cleanup (In [V5](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/V5/GeneratePrimesV5.cs)). The tests all still ran.

Refactoring Step 5([Final Code](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/Final/GeneratePrimesFinal.cs)):
-------------------------------

Next, I made one final pass over the whole program, reading it from beginning to end, rather like one would read a geometric proof. This is an important step. So far, I've been refactoring fragments. Now I want to see whether the whole program hangs together as a readable whole.

First, I realize that I don't like the name InitializeArrayOfIntegers. What's being initialized is not, in fact, an array of integers but an array of Booleans. But InitializeArrayOfBooleans is not an improvement. What we are really doing in this method is uncrossing all the relevant integers so that we can then cross out the multiples. So I change the name to UncrossIntegersUpTo. I also realize that I don't like the name isCrossed for the array of Booleans. So I change it to crossedOut. The tests all still run.

One might think that I'm being frivolous with these name changes, but with a refactoring browser, you can afford to do these kinds of tweaks; they cost virtually nothing. Even without a refactoring browser, a simple search and replace is pretty cheap. And the tests strongly mitigate any chance that we might unknowingly break something.

I don't know what I was smoking when I wrote all that maxPrimeFactor stuff. Yikes! The square root of the size of the array is not necessarily prime. That method did not calculate the maximum prime factor. The explanatory comment was simply wrong. So I rewrote the comment to better explain the rationale behind the square root and rename all the variables appropriately.[2] The tests all still run.

[2] I once watched Kent Beck refactor this very same program. He did away with the square root altogether. His rationale was that the square root was difficult to understand and that no test that failed if you iterated right up to the size of the array. I can't bring myself to give up the efficiency. I guess that shows my assembly language roots.

What the devil is that +1 doing in there? It must have been paranoia. I was afraid that a fractional square root would convert to an integer that was too small to serve as the iteration limit. But that's silly. The true iteration limit is the largest prime less than or equal to the square root of the size of the array. I'll get rid of the +1.

The tests all run, but that last change makes me pretty nervous. I understand the rationale behind the square root, but I've got a nagging feeling that there may be some corner cases that aren't being covered. So I'll write another test that checks that there are no multiples in any of the prime lists between 2 and 500. (See the TestExhaustive function in Listing 5-8.) The new test passes, and my fears are allayed.

The rest of the code reads pretty nicely. So I think we're done. The final version is shown in [Final Code](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/Final/GeneratePrimesFinal.cs) and [Final Test](https://github.com/faraazkhan/CSharpRefactoringExample/blob/master/Final/GeneratePrimesTestFinal.cs) files.
