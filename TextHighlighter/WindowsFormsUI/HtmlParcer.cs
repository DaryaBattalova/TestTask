using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace WindowsFormsUI
{
    class HtmlParcer
    {
        private HtmlParser parcer = new HtmlParser();

        /// <summary>
        /// Gets coordinates of a word from html.
        /// </summary>
        /// <param name="textToFind"></param>
        /// <returns></returns>
        public List<int> GetCoordinates(string textToFind)
        {
            string[] array = textToFind.Split(' ');
            List<string> titles = new List<string>();


            string str = File.ReadAllText(Path.Combine(GetPath(), @"..\..\Resources\zones.html"));
            IHtmlDocument doc = parcer.Parse(str);

            foreach (var v in array)
            {
               var items = doc.QuerySelectorAll("span").Where(item => item.Text() == v);
                foreach (var item in items)
                {
                    titles.Add(item.GetAttribute("title"));
                }
            }

            List<int> list = GetNumbers(titles.ToArray());

            return list;
        }



        private List<int> GetNumbers(string[] coordinates)
        {
            List<int> list = new List<int>();
            string[] str = new string[coordinates.Length];

            for (int i = 0; i < coordinates.Length; i++)
            {
                str = coordinates[i].Split(' ');
                str[4] = str[4].Replace(';', ' ');
                for (int j = 1; j < 5; j++)
                {
                    int num = Convert.ToInt32(str[j]);
                    list.Add(num);
                }
            }
            return list;
        }

        private string GetPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            return path;
        }
    }
}
