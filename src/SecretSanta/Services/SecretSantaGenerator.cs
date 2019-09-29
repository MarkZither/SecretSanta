using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecretSanta.Extentions;

namespace SecretSanta.Services
{
    public static class SecretSantaGenerator
    {
        public static IDictionary<T, T> Generate<T>(IList<T> participants)
        {
            return Generate(participants, new Dictionary<T, T>());
        }

        public static IDictionary<T, T> Generate<T>(IList<T> participants, IDictionary<T, T> bannedPairings)
        {
            
            var to = participants.GetShuffle();

            foreach (var from in participants.GetShuffle().GetPermutations())
            {
                var result = to.ZipToKV(from);
                var isValidPairings = false;
                var tries = 1;
                while (!isValidPairings && tries < 10)
                {
                    if (PairingIsValid(bannedPairings, result))
                    {
                        isValidPairings = true;
                        return result.ToDictionary();
                    }
                    tries++;
                }
            }

            throw new ApplicationException("No valid santa list can be generated");
        }

        private static bool PairingIsValid<T>(IDictionary<T, T> bannedPairings, IEnumerable<KeyValuePair<T, T>> result)
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
            return GenerateAll(participants, new Dictionary<T, T>());
        }

        public static IEnumerable<IDictionary<T, T>> GenerateAll<T>(IList<T> participants, IDictionary<T, T> bannedPairings)
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
