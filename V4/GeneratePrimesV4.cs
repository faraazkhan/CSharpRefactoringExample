public class PrimeGenerator {
private static bool[] isCrossed; private static int[] result;
public static int[] GeneratePrimeNumbers(int maxValue) {
    if (maxValue < 2)
      return new int[0];
else {
      InitializeArrayOfIntegers(maxValue);
      CrossOutMultiples();
      PutUncrossedIntegersIntoResult();
      return result;
} }
private static void InitializeArrayOfIntegers(int maxValue) {
isCrossed = new bool[maxValue + 1];
for (int i = 2; i < isCrossed.Length; i++)
      isCrossed[i] = false;
  }
private static void CrossOutMultiples() {
int maxPrimeFactor = CalcMaxPrimeFactor(); for (int i = 2; i < maxPrimeFactor + 1; i++) {
      if(NotCrossed(i))
        CrossOutputMultiplesOf(i);
} }
private static int CalcMaxPrimeFactor() {
// We cross out all multiples of p, where p is prime. // Thus, all crossed out multiples have p and q for
// factors. If p > sqrt of the size of the array, then // q will never be greater than 1. Thus p is the
// largest prime factor in the array and is also // the iteration limit.
double maxPrimeFactor = Math.Sqrt(isCrossed.Length) + 1;
    return (int) maxPrimeFactor;
  }
private static void CrossOutputMultiplesOf(int i) {
for (int multiple = 2*i; multiple < isCrossed.Length;
￼
￼
￼￼￼￼multiple += i) isCrossed[multiple] = true;
}
private static bool NotCrossed(int i) {
return isCrossed[i] == false; }
}