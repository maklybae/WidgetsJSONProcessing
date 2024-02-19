namespace AdvancedTools
{
    public static class RandomExtensions
    {
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