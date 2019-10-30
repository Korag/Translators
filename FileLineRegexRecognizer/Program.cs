using FileLineRegexRecognizer.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace FileLineRegexRecognizer
{
    class Program
    {
        public static List<Data> listOfObjects = new List<Data>();

        static void DecodeStringLineByRegexPattern(string singleLine, string regexPattern)
        {
            Match m = Regex.Match(singleLine, regexPattern);
            Data data = new Data();

            data.Integer = Int32.Parse(m.Groups["Integer"].Value);
            data.String = m.Groups["String"].Value.Trim();
            data.Date = DateTime.ParseExact(m.Groups["Date"].Value, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            listOfObjects.Add(data);

            Console.WriteLine(singleLine);
        }


        static void Main(string[] args)
        {
            string url = @"C:\Users\Łukasz\Documents\Visual Studio 2017\Projects\Translators\FileLineRegexRecognizer\File\mixedFormats.txt";

            //^(?<Integer>\d+) {1,31}(?<String>.*) {1,31}(?<Date>(([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1]))){1,31}
            string fixedSizeRegex = "^(?<Integer>\\d+) {1,31}(?<String>.*) {1,31}(?<Date>(([1-2]\\d{3})\\-([0]\\d|[1][0-2])\\-([[0-2]\\d|[3][0-1]))){1,31}";

            //^ *"? *?(?<Integer>\d+) *"? *, *"? *(?<String>.*)"{0,1} *, *"? *(?<Date>(([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1]))) *"? *,? *$
            string jsonRegex = "^ *{ *\"Integer\": *(?<Integer>\\d+), *\"String\": *\"(?<String>.*)\", *\"Date\": *\"(?<Date>(([1-2]\\d{3})\\-([0]\\d|[1][0-2])\\-([[0-2]\\d|[3][0-1])))\" * }";

            //^ *"? *?(?<Integer>\d+) *"? *, *"? *(?<String>.*)"{0,1} *, *"? *(?<Date>(([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1]))) *"? *,? *$
            string csvRegex = "^ *\"? *?(?<Integer>\\d+) *\"? *, *\"? *(?<String>.*)\"{0,1} *, *\"? *(?<Date>(([1-2]\\d{3})\\-([0]\\d|[1][0-2])\\-([[0-2]\\d|[3][0-1]))) *\"? *,? *";

            //^ *<root> *<Integer>(?<Integer>\d+)<\/Integer> *<String>(?<String>.*)<\/String> *<Date>(?<Date>(([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1])))<\/Date> *<\/root> *$
            string xmlRegex = "^ *<root> *<Integer>(?<Integer>\\d+)<\\/Integer> *<String>(?<String>.*)<\\/String> *<Date>(?<Date>(([1-2]\\d{3})\\-([0]\\d|[1][0-2])\\-([[0-2]\\d|[3][0-1])))<\\/Date> *<\\/root> *$";

            using (StreamReader sr = new StreamReader(url))
            {
                string singleLine;
                Console.WriteLine("Original lines:");

                while ((singleLine = sr.ReadLine()) != null)
                {
                    //fixedsize 32characters per field
                    if (Regex.IsMatch(singleLine, fixedSizeRegex))
                    {
                        DecodeStringLineByRegexPattern(singleLine, fixedSizeRegex);
                    }

                    //json
                    if (Regex.IsMatch(singleLine, jsonRegex))
                    {
                        DecodeStringLineByRegexPattern(singleLine, jsonRegex);
                    }

                    //xml
                    if (Regex.IsMatch(singleLine, xmlRegex))
                    {
                        DecodeStringLineByRegexPattern(singleLine, xmlRegex);
                    }

                    //csv
                    if (Regex.IsMatch(singleLine, csvRegex))
                    {
                        DecodeStringLineByRegexPattern(singleLine, csvRegex);
                    }

                }

                sr.Close();
            }

            string json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("JSON");
            Console.WriteLine(json);

            Console.ReadLine();
        }
    }
}
