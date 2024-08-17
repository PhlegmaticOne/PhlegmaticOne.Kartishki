using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Extensions
{
    public static class ListExtensions
    {
        public static T AtIndexCyclical<T>(this IList<T> list, int index)
        {
            if (index >= 0)
            {
                return list[index % list.Count];
            }

            var indexFromLast = -index % list.Count;
            return indexFromLast == 0 ? list[0] : list[^indexFromLast];
        }

        public static void UpdateRange<T>(this List<T> list, IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }

        public static int IndexOf<T>(this IReadOnlyList<T> list, T itemSearch) where T : IEquatable<T>
        {
            var index = 0;

            foreach (var item in list)
            {
                if (item.Equals(itemSearch))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static IEnumerable<Attacker> ToAttackers(this IEnumerable<DurakPlayer> players)
        {
            return players.Select(x => x.ToAttacker());
        }
    }
}