using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMvvm.Tests
{
    internal static class ICollectionExtensions
    {
        private static readonly Random random = new Random();

        public static T RandomElement<T>(this ICollection<T> collection)
        {
            return collection.ElementAt(ICollectionExtensions.random.Next(0, collection.Count));
        }
    }
}