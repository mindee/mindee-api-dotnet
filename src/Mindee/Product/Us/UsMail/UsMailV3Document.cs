using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Mindee.Parsing;
using Mindee.Parsing.Standard;

namespace Mindee.Product.Us.UsMail
{
    /// <summary>
    /// US Mail API version 3.0 document data.
    /// </summary>
    public class UsMailV3Document : IPrediction
    {
        /// <summary>
        /// Whether the mailing is marked as return to sender.
        /// </summary>
        [JsonPropertyName("is_return_to_sender")]
        public BooleanField IsReturnToSender { get; set; }

        /// <summary>
        /// The addresses of the recipients.
        /// </summary>
        [JsonPropertyName("recipient_addresses")]
        [JsonConverter(typeof(ObjectListJsonConverter<UsMailV3RecipientAddresses, UsMailV3RecipientAddress>))]
        public UsMailV3RecipientAddresses RecipientAddresses { get; set; }

        /// <summary>
        /// The names of the recipients.
        /// </summary>
        [JsonPropertyName("recipient_names")]
        public IList<StringField> RecipientNames { get; set; } = new List<StringField>();

        /// <summary>
        /// The address of the sender.
        /// </summary>
        [JsonPropertyName("sender_address")]
        public UsMailV3SenderAddress SenderAddress { get; set; }

        /// <summary>
        /// The name of the sender.
        /// </summary>
        [JsonPropertyName("sender_name")]
        public StringField SenderName { get; set; }

        /// <summary>
        /// A prettier representation of the current model values.
        /// </summary>
        public override string ToString()
        {
            string recipientNames = string.Join(
                "\n " + string.Concat(Enumerable.Repeat(" ", 17)),
                RecipientNames.Select(item => item));
            StringBuilder result = new StringBuilder();
            result.Append($":Sender Name: {SenderName}\n");
            result.Append($":Sender Address:{SenderAddress.ToFieldList()}");
            result.Append($":Recipient Names: {recipientNames}\n");
            result.Append($":Recipient Addresses:{RecipientAddresses}");
            result.Append($":Return to Sender: {IsReturnToSender}\n");
            return SummaryHelper.Clean(result.ToString());
        }
    }
}
