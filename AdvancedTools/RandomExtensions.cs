namespace AdvancedTools
{
    // <summary>
    /// Provides extension methods for the <see cref="Random"/> class.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Shuffles the elements of a list using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="random">The <see cref="Random"/> instance to use for shuffling.</param>
        /// <param name="lst">The list to be shuffled.</param>
        /// <remarks>
        /// This method shuffles the elements of the list in-place using the Fisher-Yates algorithm.
        /// </remarks>
        public static void Shuffle<T>(this Random random, List<T> lst)
        {
            int n = lst.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                (lst[k], lst[n]) = (lst[n], lst[k]);
            }
        }
    }
}