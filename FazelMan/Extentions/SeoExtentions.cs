using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FazelMan.Extentions
{
    public static class SeoExtentions
    {
        /// <summary>
        /// Implements <see cref="ISlugHelper.GenerateSlug(string)"/>
        /// </summary>
        public static string ToSlug(this string inputString)
        {
            inputString = inputString.ToLower();
            inputString = inputString.Trim();
            inputString = CleanWhiteSpace(inputString, true);
            inputString = ApplyReplacements(inputString, new Dictionary<string, string>
            {
                { " ", "_" }
            });
            inputString = RemoveDiacritics(inputString);
            inputString = DeleteCharacters(inputString, @"[^a-zA-Z0-9آ-ی\-\._]");
            inputString = Regex.Replace(inputString, "--+", "-");

            return inputString;
        }

        private static string CleanWhiteSpace(string str, bool collapse)
        {
            return Regex.Replace(str, collapse ? @"\s+" : @"\s", " ");
        }

        // Thanks http://stackoverflow.com/a/249126!
        private static string RemoveDiacritics(string str)
        {
            var stFormD = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string ApplyReplacements(string str, Dictionary<string, string> replacements)
        {
            var sb = new StringBuilder(str);

            foreach (KeyValuePair<string, string> replacement in replacements)
            {
                sb = sb.Replace(replacement.Key, replacement.Value);
            }

            return sb.ToString();
        }

        private static string DeleteCharacters(string str, string regex)
        {
            return Regex.Replace(str, regex, "");
        }

    }

}
