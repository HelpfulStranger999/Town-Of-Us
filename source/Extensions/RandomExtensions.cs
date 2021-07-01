using System;
using System.Collections.Generic;
using System.Linq;

namespace TownOfUs
{
    public static class RandomExtensions
    {
        private static readonly Random ShuffleRNG = new Random();

        /// <summary>
        ///     Shuffles the element order of the specified list.
        /// </summary>
        public static IEnumerable<TValue> Shuffle<TValue>(this IEnumerable<TValue> values)
        {
            return values.OrderBy(value => ShuffleRNG.Next()).ToArray();
        }

        public static TValue SelectRandomElement<TValue>(this IList<TValue> list)
            => list[ShuffleRNG.Next(list.Count)];
    }
}