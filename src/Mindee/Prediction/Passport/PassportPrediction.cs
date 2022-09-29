using System.Collections.Generic;
using Mindee.Prediction.Commun;

namespace Mindee.Prediction.Passport
{
    public sealed class PassportPrediction : PredictionBase
    {
        public BirthDate BirthDate { get; set; }

        public BirthPlace BirthPlace { get; set; }

        public Country Country { get; set; }

        public ExpiryDate ExpiryDate { get; set; }

        public Gender Gender { get; set; }

        public List<GivenName> GivenNames { get; set; }

        public IdNumber IdNumber { get; set; }

        public IssuanceDate IssuanceDate { get; set; }

        public Mrz1 Mrz1 { get; set; }

        public Mrz2 Mrz2 { get; set; }

        public Surname Surname { get; set; }
    }
}
