using HtmlAgilityPack;
using mshtml;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static web_scraping.Form1;

namespace web_scraping
{
    public partial class Form1 : Form// is the admin form used add charactrs , update questions and upadate characteristics
    {//global variables used for the web scrapping
        string[] ch2 = { "Kingdom", "Phylum", "Class", " Clade", "Order", "Created", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced ", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };
        string[] ch = { "Kingdom:", "Phylum:", "Class:", " Clade:", "Order:", "Created by", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced by", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };
        string s = "";
        int co2 = 0;
        int mode = 0;
        bool flag = false; 

        public Form1()
        {
            InitializeComponent();
            //it reads the count file to get the index needed to continue from where you started when press the Add characters 
            StreamReader re = new StreamReader(@"C:\count.txt");
            co2 = int.Parse(re.ReadLine());   
            re.Close();

          
        }
        public class Character //characteristics of character as properties 
        {
            public string name { get; set; }
            public string image { get; set; }
            public string url { get; set; }
            public string info1 { get; set;}
            public string info2 { get; set;}
            public string info3 { get; set; }
            public string info4 { get; set; }
            public string info5 { get; set; }
            public string po { get; set; }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string[] GetImagesInHTMLString(string htmlString)//function to retreive  valid URL for the image of the specific character
        {
            string[] images = new string[100];
            string pattern = @"<(img)\b[^>]*>";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);//is find the the url that macth the pattern
            MatchCollection matches = rgx.Matches(htmlString);

            for (int i = 0, l = matches.Count; i < l; i++)
            {
                images[i] = matches[i].Value;
            }
            return images;
        }


        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }



        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)//button to add  the charater inputted 
        {
            Bar.Value = 0;
            s = Textbox.Text;
            string[] update = getchara();//it retrives the data inputted from the user when playing the game 

            if (s == "")//is validation if the user didn't input anything 
            {
                MessageBox.Show("Invalid Input");
            }
            else
            {
                if (clone(s))//checks if the user is already added to the database 
                {
                    //if it is true that means the character is not in the database
                    
                    if (update.Length == 0)//if no data is 
                    {
                        tes(true);
                        add.Visible = false;
                        Addque.ButtonText = "Press to continue";
                        Addque.Visible = true;
                        Bar.Value = 0;


                    }
                    else
                    {
                        add.ButtonText = "Add data";
                        Adds.ButtonText = "Add new data";
                        if (mode != 0)
                        {

                            tes(true, 1, update);//this is when the character is not added in database but the user uses dta
                            add.Visible = false;
                            Addque.ButtonText = "Press to continue";
                            Addque.Visible = true;
                            Bar.Value = 0;
                            data();
                            Adds.Visible = false;
                            Adds.ButtonText = "Add Characters";
                            add.ButtonText = "Add Character";

                        }


                    }


                }
                else
                {

                    if (update.Length != 0)
                    {
                        add.ButtonText = "Add data";
                        Adds.Visible = false;
                        if (mode != 0)
                        {
                            tes(true, 2, update);
                            add.Visible = false;
                            Addque.ButtonText = "Press to continue";
                            Addque.Visible = true;
                            Bar.Value = 0;
                            data();
                            add.ButtonText = "Add Character";

                        }
                    }

                }




            }

            mode++;


            

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int max = lastcol() - 5;
          
            for (int i = 0; i < ch.Length; i++)
            {

                chara(ch[i], charach(ch[i], max), charaq(ch2[i]));

            }

            validch();
            newid();
            label1.Text = "it is finished";
            addchara.Visible = false;
            Bar.Value = 40;
            addkey.ButtonText = "Press to continue";
            addkey.Visible = true;
            label1.Text = "";



        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            reset();
            int max = lastcol() - 5;
            string[] qe = { "Whats is the animal Kingdom?", "Whats is the animal Phylum?", "Whats is the animal Class?", "Whats is the animal Clade?", "Whats is the animal Order?", "Who is the character Created by?", "Who is character's Parent(s)?", "Who is the character's Spouse(s)?", "When was the character Born?", "What is the character's Gender?", "What is the character's Occupation?", "Who is the character's Children?", "What is the character's Age?", "When did the character Died?", "Who is the character Voiced by?", "What is the character's Species?", "What is the character's Based on?", "What is the character's Origin?", "What is the character's Affiliation?", "What is the character's Status?", "What is the character's First appearance?", "What is the character's First game?", "Who is the Author?", "What is the character's Country?", "What is the character's Genre?", "What is the character's Team affiliations?", "What is the character's Abilities?", "What is the character's Place of origin?", "When was it Founded?", "Who are  the character's Enemies?", "What is the character's Powers/Skills?", "What is the character's Alias?", "What is the character's Role?" };
            for (int i = 0; i < qe.Length; i++)
            {
                que(qe[i], i, ch[i], max);




            }
            label1.Text = "it is finished";
            Addque.Visible = false;
            Bar.Value = 12;
            addchara.ButtonText = "Press to continue";
            addchara.Visible = true;
            label1.Text = "";



           

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Bar.Value = 0;
            if (Adds.ButtonText == "Add new data")
            {
                tes(true);
                label1.Text = "It is finished";
                add.Visible = false;
                Adds.Visible = false;
                Addque.ButtonText = "Press to continue";
                Addque.Visible = true;
                Bar.Value = 0;
                label1.Text = "";
                Adds.ButtonText = "Add Characters";
                add.ButtonText = "Add Character";


            }
            else
            { 
                Sites d = new Sites();
                label1.Text = co2.ToString(); 
                string site = "";
                string site2 = "";
                StreamReader r = new StreamReader(@"C:\Page.txt");
                string g = r.ReadLine(); 
                string[] f = Regex.Split(g, @"(?<!^)(?=[A-Z])");
                if (f.Length >= 2)
                {
                    site = f[0];
                    site2 = f[1];
                }
                else
                {
                    site = f[0];
                }
                r.Close();


                bool t = false;
                for (int i = 0; i < 50; i++)
                {

                    co2++;

                    s = d.charnames(co2, t, site, site2);
                    
                    if (t == false)
                    {
                        t = true;
                    }
                    else
                    {
                        t = false;
                    }
                   
                    tes(false);

                    if (i != 50)
                    {
                        Bar.Value = i;
                    }
                  
                }


                valid();


                try
                {
                    co2 = d.countnew();
                    StreamWriter re = new StreamWriter(@"C:\count.txt");
                    re.WriteLine(co2.ToString());
                    re.Close();

                }
                catch
                {
                    label2.Text = "error in file";
                }
                Bar.Value = 50;
                label1.Text = "is finished";
                add.Visible = false;
                Adds.Visible = false;
                Addque.ButtonText = "Press to continue";
                Addque.Visible = true;
                Bar.Value = 0;
                label1.Text = "";


             


            }
        }
        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            idsre();
            label1.Text = "it is finished";
            Bar.Value = 50;
            addkey.Visible = false;
            Adds.Visible = true;
            add.Visible = true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void sq(string t2, string t3, string t4, string t5, string t6, string t7, string t8, string t9, string t10)//is the method used to add the information webscrapped by the tes method 
        {
            int t = co2;
            //alter table characters auto_increment=205
            //Sql queries that have used to test 
            // Select* from characters where locate("Genre", info1)  or locate("Genre", info2) or locate("Genre", info3)or locate("Genre", info4)or locate("Genre", info5);
            // select count(CharactersId) from characters where locate("Born", info1) or locate("Born",info2)or locate("Born",info3)or locate("Born",info4)or locate("Born",info5)
            //Select* from characters Where locate("Genre", CharacterName);
            // select count(CharactersId) from characters Where locate("Genre", CharacterName);
            //Select* from characters order by  Popularity asc; 
            //select QuestionForm from questions where quepop=;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "INSERT IGNORE INTO characters ( CharactersId,CharacterName, CharaterImageUrl, CharacterInfoUrl,info1,info2,info3,info4,info5,Popularity) VALUES (@id,@name,@ImageUrl,@InfoUrl,@in1,@in2,@in3,@in4,@in5,@pop)";
            MySqlCommand cmd = new MySqlCommand(query, con);
            string id = t2 + t.ToString();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", t2);
            cmd.Parameters.AddWithValue("@ImageUrl", t3);
            cmd.Parameters.AddWithValue("@InfoUrl", t4);
            cmd.Parameters.AddWithValue("@in1", t5);
            cmd.Parameters.AddWithValue("@in2", t6);
            cmd.Parameters.AddWithValue("@in3", t7);
            cmd.Parameters.AddWithValue("@in4", t8);
            cmd.Parameters.AddWithValue("@in5", t9);
            cmd.Parameters.AddWithValue("@pop", t10);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result < 0)
            {
                MessageBox.Show("Error inserting data into Database!");

            }
            con.Close();
           

        }
        public void tes(bool sm,int mode = 0, string[] chara=null)
        {
            string g = s;
            //*[@id="mw-content-text"]/div[1]/table
            
            string[] xinfo = { "//*[@class='portable - infobox pi - background pi - border - color pi - theme - db - character pi - layout - default']", "//*[@class='portable-infobox pi-background pi-border-color pi-theme-Characters pi-layout-default']", "//*[@class='infobox biography vcard']", "//*[@class='portable-infobox pi-background pi-border-color pi-theme-character pi-layout-default']", "//*[@class='portable-infobox pi-background pi-border-color pi-theme-wikia pi-layout-default']", "//*[@class='infobox']", "//*[@class='infobox vcard']", "//*[@class='portable - infobox pi - background pi - border - color pi - theme - wikia pi - layout - default type - Character']", "//*[@class='infoboxtable']", "//*[@class='box']", "//*[@class='portable-infobox pi-background pi-border-color pi-theme-wikia pi-layout-default type-Character']", " / html / body / div[3] / div[3] / div[5] / div[1] / table", "/html/body/div[3]/div[3]/div[5]/div[1]/table[1]", "/ html / body / div[4] / div[3] / div[2] / main / div[3] / div[2] / div / aside", "/html/body/div[3]/div[3]/div[5]/div[1]/table[2]", "/ html / body / div[4] / div[3] / div[2] / main / div[3] / div[2] / div / table[1]", "/ html / body / div[3] / div[3] / div[5] / div[1] / table[1]", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/table[2]/tbody", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/aside", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/table/tbody", "/html/body/div[3]/div[3]/div[5]/div[1]/table[2]/tbody", " /html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div[1]/aside", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/aside", "/html/body/div[3]/div[3]/div[5]/div[1]/table", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/table[1]/tbody", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/aside", "/html/body/div[3]/div[3]/div[5]/div[1]/table[1]", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]", "/html/body/div[4]/div[3]/div[3]/main/div[3]/div[1]/div/aside", "/html/body/div[4]/div[3]/div[2]/main/div[3]/div[2]/div/aside", "/html/body/div[3]/div[3]/div[5]/div[1]/table/tbody" , " / html / body / div[2] / div / div[3] / main / div[3] / div[3] / div[1] / table[1] / tbody" };
            string[] el = { "div", "tr" };
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            var url = "https://www.google.com/search?q= +" + g + "+Wikipedia";
            Character lu = new Character();
            web.UserAgent = "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
            var doc = web.Load(url);
            var htmlDocument = new HtmlWeb().Load("https://www.google.com/search?q=+" + g + "&tbm=isch");
            var list = GetImagesInHTMLString(htmlDocument.Text);
            var str = list[0];
            string pattern = @"(https://.*);";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            if (str != null)
            {
                Match matches = rgx.Match(str);
                lu.image = matches.Groups[1].Value; 

            }
            var xname = "//div[@class='eqAnXb']";
            HtmlNodeCollection name = doc.DocumentNode.SelectNodes(xname);

      


            if (name != null)
            {
                foreach (var tags in name)
                {
                    HtmlNode[] w = tags.Descendants("em").ToArray();
                    if (w.Length > 0)
                    {
                        if (w.Length >= 2)
                        {
                            lu.name = w[0].InnerText + " " + w[1].InnerText;
                            if (lu.name.Length > g.Length + 3 && w[1].InnerText.Contains(g))
                            {
                                lu.name = w[1].InnerText;
                            }
                            if (lu.name.Length > g.Length + 3 && w[0].InnerText.Contains(g))
                            {
                                lu.name = w[0].InnerText;
                            }
                            if (w[0].InnerText == w[1].InnerText)
                            {
                                lu.name = w[0].InnerText;
                            }

                        }
                        else
                        {
                            lu.name = w[0].InnerText;
                        }
                    }




                }

            }




            string a = lu.name;

            var xlink = "//*[@id=\"rso\"]/div[1]";

            var doc2 = web.Load("https://www.google.com/search?q= +" + a + "+wikip+site+wiki+in");
            HtmlNodeCollection link = doc2.DocumentNode.SelectNodes(xlink);

            if (link != null)
            {
                foreach (var tags in link)
                {
                    lu.url = tags.Descendants("a").FirstOrDefault().Attributes["href"].Value;


                }

            }

            var doc3 = web.Load("https://en.wikipedia.org/wiki/Main_Page");

            try
            {
                doc3 = web.Load(lu.url);
            }
            catch
            {
                label2.Text = lu.url;

            }

            int count = 0;
            for (int u = 0; u < xinfo.Length; u++)            
            {
                HtmlNodeCollection info = doc3.DocumentNode.SelectNodes(xinfo[u]);

                if (info != null)
                {
                    foreach (var tags in info)
                    {
                        for (int d = 0; d < el.Length; d++)
                        {
                            HtmlNode[] w = tags.Descendants(el[d]).ToArray();
                            for (int y = 0; y < w.Length; y++)
                            {
                                for (int i = 0; i < ch.Length; i++)
                                {
                                    if (w[y].InnerText.Contains(ch[i]))
                                    {
                                        count++;
                                        if (count == 1)
                                        {
                                            if (!String.IsNullOrEmpty(w[y].InnerText))
                                            {
                                                lu.info1 = w[y].InnerText;
                                            
                                            }
                                        }
                                        else if (count == 2 && w[y].InnerText != lu.info1 && w[y].InnerText != lu.info3 && w[y].InnerText != lu.info4 && w[y].InnerText != lu.info5) //checking the count and if it not the same text being assigned this is the same for the if statament after this 
                                        {
                                            if (!String.IsNullOrEmpty(w[y].InnerText))
                                            {
                                                lu.info2 = w[y].InnerText;

                                            }
                                        }
                                        else if (count == 3 && w[y].InnerText != lu.info1 && w[y].InnerText != lu.info2 && w[y].InnerText != lu.info4 && w[y].InnerText != lu.info5)
                                        {
                                            if (!String.IsNullOrEmpty(w[y].InnerText))
                                            {
                                                lu.info3 = w[y].InnerText;
                                              
                                            }
                                        }
                                        else if (count == 4 && w[y].InnerText != lu.info1 && w[y].InnerText != lu.info2 && w[y].InnerText != lu.info3 && w[y].InnerText != lu.info5)
                                        {
                                            if (!String.IsNullOrEmpty(w[y].InnerText))
                                            {
                                                lu.info4 = w[y].InnerText;
                                               
                                            }
                                        }
                                        else if (count == 5 && w[y].InnerText != lu.info1 && w[y].InnerText != lu.info2 && w[y].InnerText != lu.info3 && w[y].InnerText != lu.info4)
                                        {
                                            if (!String.IsNullOrEmpty(w[y].InnerText))
                                            {
                                                lu.info5 = w[y].InnerText;
                                               
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var url2 = "https://www.google.com/search?q= +" + a;

            var doc4 = web.Load(url2);
            var pop = "//div[@class='LHJvCe']";
            HtmlNodeCollection popularity = doc4.DocumentNode.SelectNodes(pop);
            if (popularity != null)
            {
                foreach (var tags in popularity)
                {
                    lu.po = tags.Descendants("div").FirstOrDefault().InnerText;
                }
            }
            if (lu.po != null)
            {
                string[] q = Regex.Split(lu.po, "results");
                if (q.Length >= 2)
                {
                    lu.po = Regex.Replace(q[1], @"[^0-9]+", "");

                }

            }
            if (mode ==0) {
               sq(a, lu.image, lu.url, lu.info1, lu.info2, lu.info3, lu.info4, lu.info5, lu.po);
                if (sm == true)
                {
                    label1.Text = a;
                    label2.Text = lu.url;
                    label3.Text = lu.info1;
                    label4.Text = lu.info2;
                    label5.Text = lu.info3;
                    label6.Text = lu.info5;
                    label7.Text = lu.info4;
                    try
                    {
                        var imageStream = HttpWebRequest.Create(lu.image).GetResponse().GetResponseStream();
                        this.pictureBox2.Image = Image.FromStream(imageStream);
                    }
                    catch
                    {
                        var imageStream = HttpWebRequest.Create("https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png").GetResponse().GetResponseStream();
                        this.pictureBox2.Image = Image.FromStream(imageStream);

                    }
                    

                }

            }
          else if(mode==1)
          {
                disImprov(a, lu.url,lu.image , chara[0], chara[1], chara[2], chara[3], chara[4]);
                sq(a, lu.url, lu.image, chara[0], chara[1], chara[2], chara[3], chara[4], lu.po);


          }
          else if (mode == 2)
          {
               disImprov(a, lu.url, lu.image, chara[0], chara[1], chara[2], chara[3], chara[4]);
                emptydata();
               Improv(chara,datamax(lastcol()));
              
         }
            




           
        }
        public void que(string y, int k, string v,int max)
        {
            k++;
            int v2 = 0;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string[] query = { "INSERT IGNORE INTO questions (QuestionsId,QuestionForm,quepop) VALUES (@id,@form,@pop)", "update questions set quepop = @pop where QuestionsId =@id;" };
            for (int i = 5; i < max; i += 5)
            {
                int n1 = i - 4;
                int n2 = i - 3;
                int n3 = i - 2;
                int n4 = i - 1;
                int n5 = i;
                string query2 = "select count(CharactersId) from characters Where locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ")";
          
                MySqlCommand cm = new MySqlCommand(query2, con);
                con.Open();
                cm.Parameters.AddWithValue("@char", v);
             
                MySqlDataReader re = cm.ExecuteReader();

                while (re.Read())
                {
                    v2 = v2+int.Parse(re.GetString(0));
                }
                con.Close();
            }
           


            for (int i = 0; i < query.Length; i++)
            {
                MySqlCommand cmd = new MySqlCommand(query[i], con);


                cmd.Parameters.AddWithValue("@id", k);

                cmd.Parameters.AddWithValue("@form", y);
                cmd.Parameters.AddWithValue("@pop", v2);
                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }

                con.Close();
            }

        }



        private void progressBar1_Click(object sender, EventArgs e)
        {
        }
        public int quepop(string v,int n1,int n2,int n3,int n4,int n5)
        {
        
            int v2 = 0;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
           
               
                string query2 = "select count(CharactersId) from characters Where locate(@char, " + "info"+n1+ ") or locate(@char," + "info" +n2+ " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ")";

                MySqlCommand cm = new MySqlCommand(query2, con);
                con.Open();
                cm.Parameters.AddWithValue("@char", v);
             
                MySqlDataReader re = cm.ExecuteReader();

                while (re.Read())
                {
                    v2=int.Parse(re.GetString(0));
                }
                con.Close();
            

            return v2;
            

        }

        public void valid()
        {
            //   select max(CharacterName) from characters group by CharacterName, info1  having count(1) > 1// is sql that I used to test 



            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string[] query2 = { "Delete From characters WHERE CharacterName is null", "Delete From characters WHERE CharaterImageUrl is null", "Delete From characters WHERE CharacterInfoUrl is null", "Delete From characters WHERE info1 is null", "Delete From characters WHERE info2 is null", "Delete From characters WHERE info3 is null", "Delete From characters WHERE info4 is null", "delete from characters where CharactersId in ( select CharactersId   from (select *, row_number() over(partition by CharacterName, info1 order by CharactersId) as rn  from characters) x  where x.rn > 1)" };
            con.Open();
            for (int i = 0; i < query2.Length; i++)
            {
                MySqlCommand cm = new MySqlCommand(query2[i], con);
                int result2 = cm.ExecuteNonQuery();
                if (result2 < 0)
                {
                    MessageBox.Show("Error deleting data from Database!");

                }
            }
            con.Close();


        }
        public int[] mergesort(int[] ar)
        {
            int mid = ar.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[mid];
            int[] result = new int[ar.Length];
            if (ar.Length <= 1)
            {
                return ar;
            }
            else
            {
                if (ar.Length % 2 != 0)
                {
                    right = new int[mid + 1];
                }
                for (int i = 0; i < mid; i++)
                {
                    left[i] = ar[i];
                }
                int ind = 0;
                for (int i = mid; i < ar.Length; i++)
                {
                    right[ind] = ar[i];
                    ind++;
                }
                left = mergesort(left);
                right = mergesort(right);
                result = merge(left, right);
                return result;
            }

            
        }

        public static int[] merge(int[] left, int[] right)
        {
            int length = left.Length + right.Length;
            int idl = 0; int idr = 0; int idre = 0;
            int[] result = new int[length];
            while (idl < left.Length || idr < right.Length)
            {
                if (idl < left.Length && idr < right.Length)
                {
                    if (left[idl] > right[idr])
                    {
                        result[idre] = left[idl];
                        idre++;
                        idl++;

                    }
                    else
                    {
                        result[idre] = right[idr];
                        idre++;
                        idr++;
                    }

                }
                else if (idl < left.Length)
                {
                    result[idre] = left[idl];
                    idre++;
                    idl++;
                }
                else if (idr < right.Length)
                {
                    result[idre] = right[idr];
                    idre++;
                    idr++;
                }


            }
            return result;
            
        }
        public void chara(string v, string[] c, string q)
        {


            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);

            string query3 = "INSERT IGNORE INTO characteristics (Charform,CharactersName,QuestionsId) VALUES (@form,@name,@Qid)";

            for (int i = 0; i < c.Length; i++)
            {

                MySqlCommand cm = new MySqlCommand(query3, con);
                cm.Parameters.AddWithValue("@form", v);
                cm.Parameters.AddWithValue("@Qid", q);
                cm.Parameters.AddWithValue("@name", c[i]);
                con.Open();

                int result = cm.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();


            }

        }
        public void validch()
        {

            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string[] query3 = { "Delete From characteristics WHERE CharactersName is null", "delete from characteristics where CharacteristicsId in ( select CharacteristicsId   from (select *, row_number() over(partition by CharacteristicsId, CharForm order by CharacteristicsId) as rn  from characteristics) x  where x.rn > 1) " };
            con.Open();
            for (int i = 0; i < query3.Length; i++)
            {
                MySqlCommand cm = new MySqlCommand(query3[i], con);
                int result2 = cm.ExecuteNonQuery();
                if (result2 < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
            }
            con.Close();


        }

        public string[] charach(string v,int max)
        {

            List<string> names = new List<string>();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            for (int i = 5; i < max; i += 5)
            {
                int n1 = i - 4;
                int n2 = i - 3;
                int n3 = i - 2;
                int n4 = i - 1;
                int n5 = i;
                string query = "select CharacterName from  characters  Where locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ") order by Popularity asc";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@char", v);
                con.Open();
                MySqlDataReader re = cmd.ExecuteReader();
                while (re.Read())
                {


                    names.Add(re.GetString(0));


                }
                con.Close();

             
            }
            string[] c = names.ToArray();
            return c;

        }
        public string charaq(string v)
        {


            string q = "";
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query2 = "select QuestionsId from questions where locate(@char, QuestionForm)";


            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@char", v);
            con.Open();
            MySqlDataReader r = cmd2.ExecuteReader();

            while (r.Read())
            {


                q = r.GetString(0);


            }
            con.Close();
            return q;
           
        }
        public void keymaker()
        {
            List<string> names = new List<string>();

            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select CharacterName from characters order by Popularity asc";
            string query2 = "Select CharacteristicsId from characteristics where CharactersName=@name";
            string query3 = "update IGNORE characters set CharactersId = @Id where CharacterName =@name;";

            MySqlCommand cm = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader re = cm.ExecuteReader();

            while (re.Read())
            {
                names.Add(re.GetString(0));

            }
            con.Close();
            string[] chname = names.ToArray();

            for (int i = 0; i < chname.Length; i++)
            {
                string key = "";
                MySqlCommand cmd = new MySqlCommand(query2, con);
                cmd.Parameters.AddWithValue("@name", chname[i]);
                con.Open();
                MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    key = key + r.GetString(0);

                }

                con.Close();
                MySqlCommand c = new MySqlCommand(query3, con);
                c.Parameters.AddWithValue("@name", chname[i]);
                c.Parameters.AddWithValue("@Id", key);
                con.Open();

                int result = c.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();

            }


            
        }
        public void popch()
        {

            List<string> names = new List<string>();
            List<string> po = new List<string>();

            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);

            string query = "select  CharacterName from characters  where Popularity =@value";

            MySqlCommand cmd = new MySqlCommand(query, con);

            con.Open();
            MySqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {


                names.Add(re.GetString(0));


            }
            con.Close();
            string[] c = names.ToArray();

            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            web.UserAgent = "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
            for (int i = 0; i < c.Length; i++)
            {
                var url2 = "https://www.google.com/search?q= +" + c[i];
                Character lu = new Character();
                var doc4 = web.Load(url2);
                var pop = "//div[@class='LHJvCe']";
                HtmlNodeCollection popularity = doc4.DocumentNode.SelectNodes(pop);
                if (popularity != null)
                {
                    foreach (var tags in popularity)
                    {
                        lu.po = tags.Descendants("div").FirstOrDefault().InnerText;
                    }
                }
                if (lu.po != null)
                {
                    string[] q = Regex.Split(lu.po, "results");
                    if (q.Length >= 2)
                    {
                        lu.po = Regex.Replace(q[1], @"[^0-9]+", "");

                    }

                }
                po.Add(lu.po);
            }
            string[] p = po.ToArray();
            string query3 = "update IGNORE characters set Popularity = @pop where CharacterName =@name;";
            for (int i = 0; i < c.Length; i++)
            {

                MySqlCommand cm = new MySqlCommand(query3, con);
                cm.Parameters.AddWithValue("@name", c[i]);
                cm.Parameters.AddWithValue("@pop", p[i]);
                con.Open();

                int result = cm.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");
                }
                con.Close();

            }
           

        }
        public void newid()
        {
            List<string> ids = new List<string>();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select CharacteristicsId from characteristics";
            MySqlCommand cm = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader r = cm.ExecuteReader();
            while (r.Read())
            {
                ids.Add(r.GetString(0));

            }
            con.Close();
            string[] chh = ids.ToArray();
            string query2 = "update IGNORE  characteristics set CharacteristicsId= @key where CharacteristicsId =@old";

            for (int i = 0; i < chh.Length; i++)
            {
                MySqlCommand cmd = new MySqlCommand(query2, con);
                cmd.Parameters.AddWithValue("@key", i);
                cmd.Parameters.AddWithValue("@old", chh[i]);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();
            }


        }

       

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool clone(string name)
        {
            int count = 0;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "Select* from characters   where locate(@name,CharacterName)";
            MySqlCommand cm = new MySqlCommand(query, con);
            cm.Parameters.AddWithValue("@name", name);
            con.Open();
            MySqlDataReader re = cm.ExecuteReader();
           
            while (re.Read())
            {
                re.GetString(0);
                count++;
            }
            con.Close();
            if (count !=0)
            {
                MessageBox.Show("the Character is already added");
                return false;
            }
            else
            {
                return true;
            }

            
        }
        public void Improv(string[]chara,string[] infonames)
        {
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "update characters Set " + infonames[0] + "=@chara1," + infonames[1] +"=@chara2," + infonames[2] +"=@chara3," + infonames[3] +"=@chara4," + infonames[4] +"=@chara5 where CharacterName=@name;";
            MySqlCommand cm = new MySqlCommand(query, con);
            cm.Parameters.AddWithValue("@name", s);
            cm.Parameters.AddWithValue("@chara1", chara[0]);
            cm.Parameters.AddWithValue("@chara2", chara[1]);
            cm.Parameters.AddWithValue("@chara3", chara[2]);
            cm.Parameters.AddWithValue("@chara4", chara[3]);
            cm.Parameters.AddWithValue("@chara5", chara[4]);
            con.Open();
            int result = cm.ExecuteNonQuery();
            if (result < 0)
            {
                MessageBox.Show("Error inserting data into Database!");

            }
            con.Close();
        }
        public void disImprov(string name, string url, string imageurl,string chara1, string chara2, string chara3, string  chara4, string chara5)
        { 
            label1.Text = name;
            label2.Text = url;
            label3.Text = chara1;
            label4.Text = chara2;
            label5.Text = chara3;
            label6.Text = chara4;
            label7.Text = chara5;
          
            try
            {
                var imageStream = HttpWebRequest.Create(imageurl).GetResponse().GetResponseStream();
                this.pictureBox2.Image = Image.FromStream(imageStream);
            }
            catch
            {
                var imageStream = HttpWebRequest.Create("https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png").GetResponse().GetResponseStream();
                this.pictureBox2.Image = Image.FromStream(imageStream);

            }
        }
        public string[] getchara()
        {
            int ids = 0;
            List<string> chara = new List<string>();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select * from tempinfo";
            MySqlCommand cm = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader r = cm.ExecuteReader();
            while (r.Read())
            {
                if (ids <= 5)
                {
                    chara.Add(r.GetString(1));

                }

            }
            con.Close();
          
            return chara.ToArray();
        }
        public void data()
        {
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string[] query2 = { "Delete from tempinfo", "alter table tempinfo auto_increment=1" };
            for (int i = 0; i < query2.Length; i++)
            {
                MySqlCommand cmd = new MySqlCommand(query2[i], con);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();

            }
        }
        public string[] datamax(int numbercol,int mode=0)
        {  

            numbercol = numbercol - 4;
            
            string[] infonames = new string[numbercol];
            if (flag == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    int indname = numbercol + i;
                    infonames[i] = "info" + indname.ToString();
                }

            }
            else if(mode==0&&flag==false)
            {
                numbercol = numbercol - 5;
                for (int i = 0; i < 5; i++)
                {
                    int indname = numbercol + i;
                    infonames[i] = "info" + indname.ToString();
                }

            }
            

            return infonames;
        }
        public void incresedata(string[] infonames,int numbercol)
        {
            int lastind = numbercol - 5;
            string lastcolname = "info" + lastind.ToString();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "ALTER TABLE `lectorem`.`characters` ADD COLUMN `" + infonames[0] + "` VARCHAR(1000) NULL AFTER `" + lastcolname + "`, ADD COLUMN `" + infonames[1] + "` VARCHAR(1000) NULL AFTER `" + infonames[0] + "`, ADD COLUMN `" + infonames[2] + "` VARCHAR(1000) NULL AFTER `" + infonames[1] + "`, ADD COLUMN `" + infonames[3] + "` VARCHAR(1000) NULL AFTER `" + infonames[2] + "`, ADD COLUMN `" + infonames[4] + "` VARCHAR(1000) NULL AFTER `" + infonames[3] + "` ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            if (result < 0)
            {
                MessageBox.Show("Error inserting data into Database!");

            }
            con.Close();

        }
        public void emptydata()
        { int result = 0;
            int lastind = lastcol() - 5;
            string lastcolname = "info" + lastind.ToString();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select count(CharacterName) from characters where "+lastcolname+" is null and CharacterName=@name ";
            MySqlCommand cm = new MySqlCommand(query, con);
            cm.Parameters.AddWithValue("@name", s);
            con.Open();
            MySqlDataReader r = cm.ExecuteReader();
            while (r.Read())
            {

                result = int.Parse(r.GetString(0));

            }
            con.Close();
            if (result == 0)
            {
                flag = true;
                incresedata(datamax(lastcol()), lastcol());
            }
            

        }
        public int lastcol()
        {
            int numbercol = 0;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query2 = "SELECT count(*) FROM information_schema.columns WHERE table_name = 'characters'";
            MySqlCommand cm = new MySqlCommand(query2, con);
            con.Open();
            MySqlDataReader r = cm.ExecuteReader();
            while (r.Read())
            {

                numbercol = int.Parse(r.GetString(0));

            }
            con.Close();
            
            return  numbercol;

        }
        public void reset() 
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            pictureBox2.Image = null;
        }
        public void idsre()
        {
            List<string> ids = new List<string>();
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select CharactersId from characters order by Popularity asc";
            MySqlCommand cm = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader r = cm.ExecuteReader();
            while (r.Read())
            {
                ids.Add(r.GetString(0));

            }
            con.Close();
            string[] chi= ids.ToArray();
            string query2 = "update IGNORE characters set CharactersId=@key where CharactersId =@old";

            for (int i = 0; i < chi.Length; i++)
            {
                
                MySqlCommand cmd = new MySqlCommand(query2, con);
                cmd.Parameters.AddWithValue("@key", i);
                cmd.Parameters.AddWithValue("@old", chi[i]);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();
            }


        
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (label1.Text != "name" || label1.Text != null || label1.Text.Contains("finished") == false)
            {
                reset();
               
            }
        }
    }
}




