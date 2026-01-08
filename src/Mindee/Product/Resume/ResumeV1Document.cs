using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Resume
{
    /// <summary>
    ///     Resume API version 1.2 document data.
    /// </summary>
    public class ResumeV1Document : IPrediction
    {
        /// <summary>
        ///     The location information of the candidate, including city, state, and country.
        /// </summary>
        [JsonPropertyName("address")]
        public StringField Address { get; set; }

        /// <summary>
        ///     The list of certificates obtained by the candidate.
        /// </summary>
        [JsonPropertyName("certificates")]
        [JsonConverter(typeof(ObjectListJsonConverter<ResumeV1Certificates, ResumeV1Certificate>))]
        public ResumeV1Certificates Certificates { get; set; }

        /// <summary>
        ///     The ISO 639 code of the language in which the document is written.
        /// </summary>
        [JsonPropertyName("document_language")]
        public StringField DocumentLanguage { get; set; }

        /// <summary>
        ///     The type of the document sent.
        /// </summary>
        [JsonPropertyName("document_type")]
        public ClassificationField DocumentType { get; set; }

        /// <summary>
        ///     The list of the candidate's educational background.
        /// </summary>
        [JsonPropertyName("education")]
        [JsonConverter(typeof(ObjectListJsonConverter<ResumeV1Educations, ResumeV1Education>))]
        public ResumeV1Educations Education { get; set; }

        /// <summary>
        ///     The email address of the candidate.
        /// </summary>
        [JsonPropertyName("email_address")]
        public StringField EmailAddress { get; set; }

        /// <summary>
        ///     The candidate's first or given names.
        /// </summary>
        [JsonPropertyName("given_names")]
        public IList<StringField> GivenNames { get; set; } = new List<StringField>();

        /// <summary>
        ///     The list of the candidate's technical abilities and knowledge.
        /// </summary>
        [JsonPropertyName("hard_skills")]
        public IList<StringField> HardSkills { get; set; } = new List<StringField>();

        /// <summary>
        ///     The position that the candidate is applying for.
        /// </summary>
        [JsonPropertyName("job_applied")]
        public StringField JobApplied { get; set; }

        /// <summary>
        ///     The list of languages that the candidate is proficient in.
        /// </summary>
        [JsonPropertyName("languages")]
        [JsonConverter(typeof(ObjectListJsonConverter<ResumeV1Languages, ResumeV1Language>))]
        public ResumeV1Languages Languages { get; set; }

        /// <summary>
        ///     The ISO 3166 code for the country of citizenship of the candidate.
        /// </summary>
        [JsonPropertyName("nationality")]
        public StringField Nationality { get; set; }

        /// <summary>
        ///     The phone number of the candidate.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public StringField PhoneNumber { get; set; }

        /// <summary>
        ///     The candidate's current profession.
        /// </summary>
        [JsonPropertyName("profession")]
        public StringField Profession { get; set; }

        /// <summary>
        ///     The list of the candidate's professional experiences.
        /// </summary>
        [JsonPropertyName("professional_experiences")]
        [JsonConverter(
            typeof(ObjectListJsonConverter<ResumeV1ProfessionalExperiences, ResumeV1ProfessionalExperience>))]
        public ResumeV1ProfessionalExperiences ProfessionalExperiences { get; set; }

        /// <summary>
        ///     The list of social network profiles of the candidate.
        /// </summary>
        [JsonPropertyName("social_networks_urls")]
        [JsonConverter(typeof(ObjectListJsonConverter<ResumeV1SocialNetworksUrls, ResumeV1SocialNetworksUrl>))]
        public ResumeV1SocialNetworksUrls SocialNetworksUrls { get; set; }

        /// <summary>
        ///     The list of the candidate's interpersonal and communication abilities.
        /// </summary>
        [JsonPropertyName("soft_skills")]
        public IList<StringField> SoftSkills { get; set; } = new List<StringField>();

        /// <summary>
        ///     The candidate's last names.
        /// </summary>
        [JsonPropertyName("surnames")]
        public IList<StringField> Surnames { get; set; } = new List<StringField>();

        /// <summary>
        ///     A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            var givenNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                GivenNames.Select(item => item));
            var surnames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 10)),
                Surnames.Select(item => item));
            var hardSkills = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                HardSkills.Select(item => item));
            var softSkills = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 13)),
                SoftSkills.Select(item => item));
            var result = new StringBuilder();
            result.Append($":Document Language: {DocumentLanguage}\n");
            result.Append($":Document Type: {DocumentType}\n");
            result.Append($":Given Names: {givenNames}\n");
            result.Append($":Surnames: {surnames}\n");
            result.Append($":Nationality: {Nationality}\n");
            result.Append($":Email Address: {EmailAddress}\n");
            result.Append($":Phone Number: {PhoneNumber}\n");
            result.Append($":Address: {Address}\n");
            result.Append($":Social Networks:{SocialNetworksUrls}");
            result.Append($":Profession: {Profession}\n");
            result.Append($":Job Applied: {JobApplied}\n");
            result.Append($":Languages:{Languages}");
            result.Append($":Hard Skills: {hardSkills}\n");
            result.Append($":Soft Skills: {softSkills}\n");
            result.Append($":Education:{Education}");
            result.Append($":Professional Experiences:{ProfessionalExperiences}");
            result.Append($":Certificates:{Certificates}");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
