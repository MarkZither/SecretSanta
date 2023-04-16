using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecretSanta.Extensions;

namespace SecretSanta.Shared
{
    public static class SecretSantaGenerator
    {
        public static IDictionary<T, T> Generate<T>(IList<T> participants)
        {
            return Generate(participants, new List<KeyValuePair<T, T>>());
        }

        public static IDictionary<T, T> Generate<T>(IList<T> participants, List<KeyValuePair<T, T>> bannedPairings)
        {
            
            var to = participants.GetShuffle();
            int tried = 0;
            foreach (var from in participants.GetShuffle().GetPermutations())
            {
                var result = to.ZipToKV(from);
                var isValidPairings = false;
                var tries = 1;
                while (!isValidPairings && tries < 2)
                {
                    if (PairingIsValid(bannedPairings, result))
                    {
                        isValidPairings = true;
                        return result.ToDictionary();
                    }
                    tries++;
                    tried++;
                }
            }

            throw new ApplicationException("No valid santa list can be generated");
        }

        private static bool PairingIsValid<T>(List<KeyValuePair<T, T>> bannedPairings, IEnumerable<KeyValuePair<T, T>> result)
        {
            foreach (var r in result)
            {
                if (r.Key.Equals(r.Value) || bannedPairings.Contains(r))
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<IDictionary<T, T>> GenerateAll<T>(IList<T> participants)
        {
            return GenerateAll(participants, new List<KeyValuePair<T, T>>());
        }

        public static IEnumerable<IDictionary<T, T>> GenerateAll<T>(IList<T> participants, List<KeyValuePair<T, T>> bannedPairings)
        {
            var to = participants.GetShuffle();

            foreach (var from in participants.GetShuffle().GetPermutations())
            {
                var result = to.ZipToKV(from);

                if (PairingIsValid(bannedPairings, result))
                {
                    yield return result.ToDictionary();
                }
            }
        }
    }
}
