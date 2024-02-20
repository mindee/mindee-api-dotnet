using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Fr.CarteGrise
{
    /// <summary>
    /// Document data for Carte Grise, API version 1.
    /// </summary>
    public class CarteGriseV1Document : IPrediction
    {
        /// <summary>
        /// The vehicle's license plate number.
        /// </summary>
        [JsonPropertyName("a")]
        public StringField A { get; set; }

        /// <summary>
        /// The vehicle's first release date.
        /// </summary>
        [JsonPropertyName("b")]
        public DateField B { get; set; }

        /// <summary>
        /// The vehicle owner's full name including maiden name.
        /// </summary>
        [JsonPropertyName("c1")]
        public StringField C1 { get; set; }

        /// <summary>
        /// The vehicle owner's address.
        /// </summary>
        [JsonPropertyName("c3")]
        public StringField C3 { get; set; }

        /// <summary>
        /// Number of owners of the license certificate.
        /// </summary>
        [JsonPropertyName("c41")]
        public StringField C41 { get; set; }

        /// <summary>
        /// Mentions about the ownership of the vehicle.
        /// </summary>
        [JsonPropertyName("c4a")]
        public StringField C4A { get; set; }

        /// <summary>
        /// The vehicle's brand.
        /// </summary>
        [JsonPropertyName("d1")]
        public StringField D1 { get; set; }

        /// <summary>
        /// The vehicle's commercial name.
        /// </summary>
        [JsonPropertyName("d3")]
        public StringField D3 { get; set; }

        /// <summary>
        /// The Vehicle Identification Number (VIN).
        /// </summary>
        [JsonPropertyName("e")]
        public StringField E { get; set; }

        /// <summary>
        /// The vehicle's maximum admissible weight.
        /// </summary>
        [JsonPropertyName("f1")]
        public StringField F1 { get; set; }

        /// <summary>
        /// The vehicle's maximum admissible weight within the license's state.
        /// </summary>
        [JsonPropertyName("f2")]
        public StringField F2 { get; set; }

        /// <summary>
        /// The vehicle's maximum authorized weight with coupling.
        /// </summary>
        [JsonPropertyName("f3")]
        public StringField F3 { get; set; }

        /// <summary>
        /// The document's formula number.
        /// </summary>
        [JsonPropertyName("formula_number")]
        public StringField FormulaNumber { get; set; }

        /// <summary>
        /// The vehicle's weight with coupling if tractor different than category M1.
        /// </summary>
        [JsonPropertyName("g")]
        public StringField G { get; set; }

        /// <summary>
        /// The vehicle's national empty weight.
        /// </summary>
        [JsonPropertyName("g1")]
        public StringField G1 { get; set; }

        /// <summary>
        /// The car registration date of the given certificate.
        /// </summary>
        [JsonPropertyName("i")]
        public DateField I { get; set; }

        /// <summary>
        /// The vehicle's category.
        /// </summary>
        [JsonPropertyName("j")]
        public StringField J { get; set; }

        /// <summary>
        /// The vehicle's national type.
        /// </summary>
        [JsonPropertyName("j1")]
        public StringField J1 { get; set; }

        /// <summary>
        /// The vehicle's body type (CE).
        /// </summary>
        [JsonPropertyName("j2")]
        public StringField J2 { get; set; }

        /// <summary>
        /// The vehicle's body type (National designation).
        /// </summary>
        [JsonPropertyName("j3")]
        public StringField J3 { get; set; }

        /// <summary>
        /// Machine Readable Zone, first line.
        /// </summary>
        [JsonPropertyName("mrz1")]
        public StringField Mrz1 { get; set; }

        /// <summary>
        /// Machine Readable Zone, second line.
        /// </summary>
        [JsonPropertyName("mrz2")]
        public StringField Mrz2 { get; set; }

        /// <summary>
        /// The vehicle's owner first name.
        /// </summary>
        [JsonPropertyName("owner_first_name")]
        public StringField OwnerFirstName { get; set; }

        /// <summary>
        /// The vehicle's owner surname.
        /// </summary>
        [JsonPropertyName("owner_surname")]
        public StringField OwnerSurname { get; set; }

        /// <summary>
        /// The vehicle engine's displacement (cm3).
        /// </summary>
        [JsonPropertyName("p1")]
        public StringField P1 { get; set; }

        /// <summary>
        /// The vehicle's maximum net power (kW).
        /// </summary>
        [JsonPropertyName("p2")]
        public StringField P2 { get; set; }

        /// <summary>
        /// The vehicle's fuel type or energy source.
        /// </summary>
        [JsonPropertyName("p3")]
        public StringField P3 { get; set; }

        /// <summary>
        /// The vehicle's administrative power (fiscal horsepower).
        /// </summary>
        [JsonPropertyName("p6")]
        public StringField P6 { get; set; }

        /// <summary>
        /// The vehicle's power to weight ratio.
        /// </summary>
        [JsonPropertyName("q")]
        public StringField Q { get; set; }

        /// <summary>
        /// The vehicle's number of seats.
        /// </summary>
        [JsonPropertyName("s1")]
        public StringField S1 { get; set; }

        /// <summary>
        /// The vehicle's number of standing rooms (person).
        /// </summary>
        [JsonPropertyName("s2")]
        public StringField S2 { get; set; }

        /// <summary>
        /// The vehicle's sound level (dB).
        /// </summary>
        [JsonPropertyName("u1")]
        public StringField U1 { get; set; }

        /// <summary>
        /// The vehicle engine's rotation speed (RPM).
        /// </summary>
        [JsonPropertyName("u2")]
        public StringField U2 { get; set; }

        /// <summary>
        /// The vehicle's CO2 emission (g/km).
        /// </summary>
        [JsonPropertyName("v7")]
        public StringField V7 { get; set; }

        /// <summary>
        /// Next technical control date.
        /// </summary>
        [JsonPropertyName("x1")]
        public StringField X1 { get; set; }

        /// <summary>
        /// Amount of the regional proportional tax of the registration (in euros).
        /// </summary>
        [JsonPropertyName("y1")]
        public StringField Y1 { get; set; }

        /// <summary>
        /// Amount of the additional parafiscal tax of the registration (in euros).
        /// </summary>
        [JsonPropertyName("y2")]
        public StringField Y2 { get; set; }

        /// <summary>
        /// Amount of the additional CO2 tax of the registration (in euros).
        /// </summary>
        [JsonPropertyName("y3")]
        public StringField Y3 { get; set; }

        /// <summary>
        /// Amount of the fee for managing the registration (in euros).
        /// </summary>
        [JsonPropertyName("y4")]
        public StringField Y4 { get; set; }

        /// <summary>
        /// Amount of the fee for delivery of the registration certificate in euros.
        /// </summary>
        [JsonPropertyName("y5")]
        public StringField Y5 { get; set; }

        /// <summary>
        /// Total amount of registration fee to be paid in euros.
        /// </summary>
        [JsonPropertyName("y6")]
        public StringField Y6 { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($":a: {A}\n");
            result.Append($":b: {B}\n");
            result.Append($":c1: {C1}\n");
            result.Append($":c3: {C3}\n");
            result.Append($":c41: {C41}\n");
            result.Append($":c4a: {C4A}\n");
            result.Append($":d1: {D1}\n");
            result.Append($":d3: {D3}\n");
            result.Append($":e: {E}\n");
            result.Append($":f1: {F1}\n");
            result.Append($":f2: {F2}\n");
            result.Append($":f3: {F3}\n");
            result.Append($":g: {G}\n");
            result.Append($":g1: {G1}\n");
            result.Append($":i: {I}\n");
            result.Append($":j: {J}\n");
            result.Append($":j1: {J1}\n");
            result.Append($":j2: {J2}\n");
            result.Append($":j3: {J3}\n");
            result.Append($":p1: {P1}\n");
            result.Append($":p2: {P2}\n");
            result.Append($":p3: {P3}\n");
            result.Append($":p6: {P6}\n");
            result.Append($":q: {Q}\n");
            result.Append($":s1: {S1}\n");
            result.Append($":s2: {S2}\n");
            result.Append($":u1: {U1}\n");
            result.Append($":u2: {U2}\n");
            result.Append($":v7: {V7}\n");
            result.Append($":x1: {X1}\n");
            result.Append($":y1: {Y1}\n");
            result.Append($":y2: {Y2}\n");
            result.Append($":y3: {Y3}\n");
            result.Append($":y4: {Y4}\n");
            result.Append($":y5: {Y5}\n");
            result.Append($":y6: {Y6}\n");
            result.Append($":Formula Number: {FormulaNumber}\n");
            result.Append($":Owner's First Name: {OwnerFirstName}\n");
            result.Append($":Owner's Surname: {OwnerSurname}\n");
            result.Append($":MRZ Line 1: {Mrz1}\n");
            result.Append($":MRZ Line 2: {Mrz2}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
