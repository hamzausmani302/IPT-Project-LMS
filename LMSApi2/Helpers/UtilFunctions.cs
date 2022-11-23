using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LMSApi2.Helpers
{
    public class UtilFunctions
    {
        public static string generateClassCode() {
            const string src = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 8;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                char c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static int ParseString(string str) {
            if (int.TryParse(str, out int integerId)) {
                return integerId;
            }
            throw new BadRequestException();
        
        }
    }
}
