
namespace EsHeichSample.Client
{
    using System;

    public static class ComparableExtensions
    {
        public static bool IsInRange<TType>(this TType value, TType min, TType max)
            where TType : IComparable
        {
            var greaterThan = value.CompareTo(min) == 1 | value.CompareTo(min) == 0;
            var lessThan = value.CompareTo(max) == -1 | value.CompareTo(max) == 0;

            if(lessThan & greaterThan)
                return true;
            else
                return false;
        }
    }
}
