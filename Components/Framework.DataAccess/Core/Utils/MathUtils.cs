namespace Framework.CommonServices.BusinessDomain.Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// Extension method which gracefully handles divide by zero errors.
        /// </summary>
        public static decimal SafeDivideBy(this decimal numerator, decimal denominator)
        {
            if (denominator == 0) return 0;
            return numerator / denominator;
        }
    }
}
