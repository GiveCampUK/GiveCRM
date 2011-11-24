namespace GiveCRM.DummyDataGenerator.Generation
{
    using System;
    using System.Collections.Generic;

    internal class RandomSource
    {
        private readonly Random random = new Random();

        public int Next(int max)
        {
            return random.Next(max);
        }

        public int Next(int min, int max)
        {
            return random.Next(min, max);
        }

        public string PhoneDigits()
        {
            string prefix = "0" + random.Next(100) + " ";
            return prefix + random.Next(10000).ToString("0000") + random.Next(10000).ToString("0000");
        }

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

        public bool Percent(int passPercent)
        {
            var value = random.Next(100);
            return value < passPercent;
        }

        public string Letter()
        {
            var offset = random.Next(25);
            var index = 'A' + offset;

            var letter = (char)index;

            return new string(new[] { letter });
        }

        public bool Bool()
        {
            var value = random.Next(100);
            return value < 50;
        }
    }
}
