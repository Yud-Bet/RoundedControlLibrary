namespace RoundedControl
{
    static class clsExtension_Double
    {
        public static double ForceBounds(this double thisDouble, int begin, int end)
        {
            if (thisDouble < begin) return begin;
            if (thisDouble > end) return end;
            return thisDouble;
        }
    }
}
