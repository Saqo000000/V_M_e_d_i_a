using System.Text.RegularExpressions;

namespace Assignment_2
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Attaches string's each 6 letter word desired replacement, and changes url to our desired proxy url
        /// </summary>
        /// <param name="responseBody">String to be modified</param>
        /// <param name="proxyUrl">Desired Url to replace</param>
        /// <param name="postfix">Desired string to attach to the end of the word</param>
        /// <returns></returns>
        public static string ModifyResponseBody(this string responseBody, string proxyUrl, string postfix)
        {
            responseBody = Regex.Replace(responseBody, "(?<=^|\\s)(\\w{6})(?=\\s|$)", postfix);
            responseBody = responseBody.Replace("href=\"/", $"href=\\{proxyUrl}");
            return responseBody;
        }
    }
}
