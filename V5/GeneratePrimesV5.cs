private static void PutUncrossedIntegersIntoResult() {
result = new int[NumberOfUncrossedIntegers()]; for (int j = 0, i = 2; i < isCrossed.Length; i++) {
    if (NotCrossed(i))
      result[j++] = i;
} }
private static int NumberOfUncrossedIntegers() {
int count = 0;
for (int i = 2; i < isCrossed.Length; i++) {
    if (NotCrossed(i))
      count++; // bump count.
}
  return count;
}