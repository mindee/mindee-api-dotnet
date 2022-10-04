using System;
using System.IO;
using System.Linq;

namespace Mindee.Domain
{
    static public class FileVerification
    {
        private static readonly string[] _authorizedFileExtensions =
            { 
                ".pdf",
                ".webp",
                ".jpg",
                ".jpeg",
                ".png",
                ".heic",
                ".tiff",
                ".tif",
            };

        public static bool IsFileNameExtensionRespectLimitation(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            return _authorizedFileExtensions.Any(f => f.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
