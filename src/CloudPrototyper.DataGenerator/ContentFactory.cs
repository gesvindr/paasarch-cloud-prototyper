using System;

namespace CloudPrototyper.DataGenerator
{
    /// <summary>
    /// Returns content based on decription.
    /// </summary>
    public class ContentFactory
    {
        private static object Lock = new object();
        private static Random Random = new Random();

        private static int GetRandomNumber(int from, int to)
        {
            int rand = 0;
            lock (Lock)
            {
                rand = Random.Next(from, to);
            }
            return rand;
        }
    

        /// <summary>
        /// Returns content based on decription.
        /// </summary>
        public object GetData(string type, int contentSize)
        {
            lock (Random)
            {
                switch (type)
                {
                    case "String":
                    case "System.String":
                    case "string":
                        return CreateString(contentSize);
                    case "Int32":
                    case "System.Int32":
                    case "double":
                    case "int":
                    case "long":
                    case "float":
                        var randomNumber = GetRandomNumber(0, (int)Math.Pow(10, contentSize));
                        return randomNumber;
                    default:
                        throw new ArgumentException("Type not supported");
                }
            }
        }

        private string CreateString(int stringLength)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            char[] chars = new char[stringLength];
            for (int i = 0; i < stringLength; i++)
            {
                var randomBumber = GetRandomNumber(0, allowedChars.Length);
                chars[i] = allowedChars[randomBumber];
            }
            return new string(chars);
        }
    }
}
