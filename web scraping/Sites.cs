using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using WatiN.Core;
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace web_scraping
{
    internal class Sites 
    {
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        int o = 0;
        Char r = 'A';
                    

        int c = 0;
        string y = "";
        int u = 0;
        int t = 50;
        public string charcomp(int count)
        {
            string m = "";

            t--;

            Random ran = new Random();
            o = ran.Next(0, alpha.Length - 1);
            r = alpha[o];
       
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var url = "https://www.who2.com/a-to-z/" + r + "/";
            var xlist = "/html/body/div[1]/div[1]/section/div/div/div";
            var doc = web.Load(url);
            HtmlNodeCollection list = doc.DocumentNode.SelectNodes(xlist);
           
            if (list != null)
            {
                foreach (var tags in list)
                {

                    HtmlNode[] gg = tags.Descendants("li").ToArray();
                    try
                    {
                        if (count <= gg.Length - 1)
                        {
                            m = gg[count].InnerText;
                        }
                        else
                        {
                            if (t <= gg.Length - 1)
                            {
                                m = gg[t].InnerText;
                            }
                            else
                            {
                                t = gg.Length;
                          
                                m = gg[t].InnerText;
                                
                            }




                        }

                    }
                    catch
                    {
                        m = gg[0].InnerText;
                    }





                }



            }
            return m;

        }
        public string charnames(int count, bool t2,string site ,string site2)
        {


            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            string m = "";


            var url = "https://characters.fandom.com/wiki/Special:AllPages?from=" + site  + "+" + site2;
            var xlist2 = "/html/body/div[4]/div[3]/div[3]/main/div[3]/div/div[4]";
            var doc2 = web.Load(url);
            HtmlNodeCollection list2 = doc2.DocumentNode.SelectNodes(xlist2);
         
            if (list2 != null)
            {
                foreach (var tags in list2)
                {
                    HtmlNode[] gg = tags.Descendants("a").ToArray();
                    if (gg.Length >= 2)
                    {
                        y = gg[1].InnerText;
                    }
                    else
                    {
                        y = gg[0].InnerText;
                    }


                      

                    



                }


            }



            var xlist = "/html/body/div[4]/div[3]/div[3]/main/div[3]/div/div[3]";


            var doc = web.Load(url);
            HtmlNodeCollection list = doc.DocumentNode.SelectNodes(xlist);
            if (list != null)
            {
                foreach (var tags in list)
                {
                    HtmlNode[] gg = tags.Descendants("li").ToArray();
                    u = gg.Length;
                    if (count <= u - 1)
                    {
                        m = gg[count].InnerText;

                    }






                }


            }

            if (count >=u&&u!=0)
            {
                string page = "";
                string w = y.Replace("Next page", "");
              
                page = Regex.Replace(w, @"[^0-9a-zA-Z]+", "");
              
               
                StreamWriter re2 = new StreamWriter(@"C:\Page.txt");
                re2.WriteLine(page);
                re2.Close();       
               count = count-u;
               

                

            }

            c = count;



          if (t2 == true)
            {

                int count2 = count -1;

               m = charcomp(count2);
                
            }
          

            return m;
        }
        public int countnew()
        {
            int New = c;


          return New;
        }



    }


}
