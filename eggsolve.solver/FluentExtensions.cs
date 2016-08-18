using System;
using System.Collections.Generic;

namespace eggsolve.solver
{
    public static class FluentExtensions
    {
        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values.
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/>
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var max = sourceIterator.Current;
                var maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <remarks>
        /// If more than one element has the minimal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current minimal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values.
        /// </summary>
        /// <remarks>
        /// If more than one element has the minimal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current minimal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/>
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            if (comparer == null) throw new ArgumentNullException("comparer");
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        /// <summary>
        /// Performs the given action for each item in the sequence.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">Sequence of items being operated on.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The original sequence.</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                T capturedItem = item; // Necessary to prevent captured variable bug.
                action(capturedItem);
            }

            return items;
        }

        /// <summary>
        /// Performs the given action for each item in the list.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">List of items being operated on.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The original sequence.</returns>
        public static IList<T> Each<T>(this IList<T> items, Action<T> action)
        {

            for (int i = 0; i < items.Count; i++)
            {
                T capturedItem = items[i]; // Necessary to prevent captured variable bug.
                action(capturedItem);
            }

            return items;
        }

        /// <summary>
        /// Performs the given action for each item in the array.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">Array of items being operated on.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The original sequence.</returns>
        public static T[] Each<T>(this T[] items, Action<T> action)
        {

            for (int i = 0; i < items.Length; i++)
            {
                T capturedItem = items[i]; // Necessary to prevent captured variable bug.
                action(capturedItem);
            }

            return items;
        }

        /// <summary>
        /// Performs the given action for each item in the sequence.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">Sequence of items being operated on.</param>
        /// <param name="action">The action to be performed, which may also use the index variable.</param>
        /// <returns>The original sequence.</returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            int i = 0;
            foreach (T item in items)
            {
                T capturedItem = item; // Necessary to prevent captured variable bug.
                action(capturedItem, i++);
            }

            return items;
        }

        /// <summary>
        /// Performs the given action for each item in the list.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">List of items being operated on.</param>
        /// <param name="action">The action to be performed, which may also use the index variable.</param>
        /// <returns>The original sequence.</returns>
        public static IList<T> Each<T>(this IList<T> items, Action<T, int> action)
        {

            for (int i = 0; i < items.Count; i++)
            {
                T capturedItem = items[i]; // Necessary to prevent captured variable bug.
                action(capturedItem, i);
            }

            return items;
        }

        /// <summary>
        /// Performs the given action for each item in the array.
        /// </summary>
        /// <typeparam name="T">Type of item being iterated on.</typeparam>
        /// <param name="items">Array of items being operated on.</param>
        /// <param name="action">The action to be performed, which may also use the index variable.</param>
        /// <returns>The original sequence.</returns>
        public static T[] Each<T>(this T[] items, Action<T, int> action)
        {

            for (int i = 0; i < items.Length; i++)
            {
                T capturedItem = items[i]; // Necessary to prevent captured variable bug.
                action(capturedItem, i);
            }

            return items;
        }

    }

}
