using System;

namespace GiveCRM.DummyDataGenerator.Generation
{
    internal class DonationGenerator : BaseGenerator
    {
        private readonly RandomSource random = new RandomSource();

        public DonationGenerator(Action<string> logAction) : base(logAction)
        {}

        internal override string GeneratedItemType{get {return "donations";}}

        internal override void Generate(int numberToGenerate)
        {
            
        }
    }
}