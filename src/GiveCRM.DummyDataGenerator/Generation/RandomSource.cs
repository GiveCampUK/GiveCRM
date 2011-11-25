using System;
using System.Collections.Generic;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class RandomSource
    {
        private readonly Random random = new Random();

        public int NextInt(int max)
        {
            return random.Next(max);
        }

        public int NextInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public double NextDouble(int max)
        {
            return max * random.NextDouble();
        }

        public DateTime NextDateTime()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();

            int range = (DateTime.Today - start).Days;           
            return start.AddDays(gen.Next(range));            
        }

        public string PhoneDigits()
        {
            string prefix = "0" + random.Next(100) + " ";
            return prefix + random.Next(10000).ToString("0000") + random.Next(10000).ToString("0000");
        }

        /// <summary>
        /// Returns a random element from the specified list.
        /// </summary>
        public T PickFromList<T>(IList<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            int index = random.Next(list.Count);
            return list[index];
        }

        /// <summary>
        /// Returns <c>true</c> if a random percentage is less than or 
        /// equal to the supplied value, otherwise <c>false</c>.
        /// </summary>
        public bool Percent(int passPercent)
        {
            var value = random.Next(100);
            return value <= passPercent;
        }

        /// <summary>
        /// Returns a random (uppercase) letter.
        /// </summary>
        public string Letter()
        {
            var offset = random.Next(25);
            var index = 'A' + offset;

            var letter = (char) index;
            return new string(new[] {letter});
        }

        public bool Bool()
        {
            var value = random.Next(100);
            return value < 50;
        }
    }
}
