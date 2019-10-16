using FileLineRegexRecognizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FileLineRegexRecognizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"..\File\mixedFormats.txt";
            List<Data> listOfObjects = new List<Data>();

            string fixedSizeRegex = "";
            string jsonRegex = "";
            string csvRegex = "";
            string xmlRegex = "";

            using (StreamReader sr = new StreamReader(url))
            {
                string singleLine;

                while ((singleLine = sr.ReadLine()) != null)
                {
                    // fixedsize
                    if (Regex.IsMatch(singleLine, fixedSizeRegex))
                    {
                        Match m = Regex.Match(singleLine, fixedSizeRegex);
                        Data data = new Data();

                        data.Integer = Int32.Parse(m.Groups["integer"].Value);
                        data.String = m.Groups["string"].Value.Trim();
                        data.Date = DateTime.Parse(m.Groups["date"].Value, "yyyy-mm-dd", Culture....);

                        listOfObjects.Add(data);

                        Console.WriteLine(singleLine);
                    }

                    //json
                    //if (Regex.IsMatch(singleLine, jsonRegex))
                    //{
                    //    Match m = Regex.Match(singleLine, jsonRegex);
                    //    Data data = new Data();

                    //    data.Integer = Int32.Parse(m.Groups["integer"].Value);
                    //    data.String = m.Groups["string"].Value.Trim();
                    //    data.Date = DateTime.Parse(m.Groups["date"].Value, "yyyy-mm-dd", Culture....);

                    //    listOfObjects.Add(data);

                    //    Console.WriteLine(singleLine);
                    //}

                    // xml
                    //if (Regex.IsMatch(singleLine, xmlRegex))
                    //{
                    //    Match m = Regex.Match(singleLine, xmlRegex);
                    //    Data data = new Data();

                    //    data.Integer = Int32.Parse(m.Groups["integer"].Value);
                    //    data.String = m.Groups["string"].Value.Trim();
                    //    data.Date = DateTime.Parse(m.Groups["date"].Value, "yyyy-mm-dd", Culture....);

                    //    listOfObjects.Add(data);

                    //    Console.WriteLine(singleLine);
                    //}

                    // csv
                    //if (Regex.IsMatch(singleLine, csvRegex))
                    //{
                    //    Match m = Regex.Match(singleLine, csvRegex);
                    //    Data data = new Data();

                    //    data.Integer = Int32.Parse(m.Groups["integer"].Value);
                    //    data.String = m.Groups["string"].Value.Trim();
                    //    data.Date = DateTime.Parse(m.Groups["date"].Value, "yyyy-mm-dd", Culture....);

                    //    listOfObjects.Add(data);

                    //    Console.WriteLine(singleLine);
                    //}

                }

                sr.Close();
            }

            Console.ReadLine();
        }
    }
}
