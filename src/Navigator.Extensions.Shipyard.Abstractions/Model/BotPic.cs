using System.IO;

namespace Navigator.Extensions.Shipyard.Abstractions.Model
{
    /// <summary>
    /// Bot Picture.
    /// </summary>
    public class BotPic
    {
        /// <summary>
        /// Picture in stream format.
        /// </summary>
        public MemoryStream File { get; init; }
       
        /// <summary>
        /// Mime type of the picture. 
        /// </summary>
        public string MimeType { get; init; } = "image/png";
    }
}