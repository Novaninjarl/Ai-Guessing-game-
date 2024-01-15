using mshtml;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pkix;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace web_scraping
{
    public partial class Form2 : Form
    {
        string[] chara = new string[20];
        string[] name = new string[20];
        Ai li = new Ai();
        int count = 0;
        List<int> used = new List<int>();
        List<String> related  = new List<String>();
        string[] addedchara = new string[5];
        int ni = 0;
        List<String> newchara = new List<String>();
      

        public Form2()
        {
            InitializeComponent();
            Chlabel.Visible = false;
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

       


      private void bunifuTileButton3_Click(object sender, EventArgs e)
      {
            Form1 ad = new Form1();
            ad.Show();
    
          

      }
        public void disque(int pop)
        {
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = " select QuestionForm from questions where quepop=@pop ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@pop", pop);
            con.Open();
            MySqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {
                QueLabel.Text = re.GetString(0);
               

            }
            con.Close();

        }
        public void dischara(string que)
        {
            int k = 0;
            string qu= convert(que);

            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "(select info1,Popularity,CharacterName from characters where locate(@char, info1))union (select info2,Popularity,CharacterName   from characters where locate(@char, info2)) union (select info3, Popularity,CharacterName  from characters where locate(@char, info3)) union (select info4,Popularity,CharacterName  from characters where locate(@char, info4)) union (select info5,Popularity,CharacterName  from  characters where locate(@char, info5)) order by Popularity asc  ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@char", qu);
            con.Open();
            MySqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {
                if (k != 20)
                {
                    chara[k] = re.GetString(0);
                    name[k] = re.GetString(2);
                    k++;
                }
               
               
               
            }
            con.Close();
        



        }
   
        private void QueLabel_Click(object sender, EventArgs e)
        {

        }
        public void game(int[] nodes,bool t)
        {
            Textb.Clear();

            int pop = li.MiniMax(0, 0, true, nodes, -100000, 1000000);

            used.Add(pop);
            if (pop == 0)
            {
                bunifuThinButton24.Visible = false;
                pagetab.SelectTab(2);

            }
            else
            {
                disque(pop);
                string h = QueLabel.Text;
                if (t)
                {
                    li.invalidque(h);
                    used.AddRange(li.turn());
                }

            }






        }

        public int[] validnodes()
        {
          
            string[] ch = { "Kingdom:", "Phylum:", "Class:", " Clade:", "Order:", "Created by", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced by", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };
            Form1 lu = new Form1();
            int be = 0;
            
            int max = lu.lastcol() - 5;
            int[] quei = new int[ch.Length];
            for (int i = 0; i < ch.Length; i++)
            { for(int n = 5; n <= max; n+=5)
              {
                be = be+lu.quepop(ch[i],  n-4, n-3, n-2, n-1, n);
                
              }
                    
                if (used.Contains(be)==false)
                {
                    quei[i] = be;
                }
                be = 0;  
            }

           quei = lu.mergesort(quei);
            return quei;
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            Form1 lu = new Form1();
            int max = lu.lastcol() - 5;

            count++;
            if (count % 5== 0)
            {
                
              li.result(addedchara[0],max, addedchara[1], addedchara[2], addedchara[3], addedchara[4]);

                
                Chlabel.Visible = true;
                guess(li.getname(), li.getUrl());
                if (li.getname() == "")
                {
                    pagetab.SelectTab(2);
                    if (ni >= 4)
                    {
                        ni = 0;
                    }
                }
                else
                {
                    pagetab.SelectTab(3);
                    if (ni >= 4)
                    {
                        ni = 0;
                    }
                }
               
             }
            else
            {
                game(validnodes(), false);

            }

        }

       

       

        public string convert(string que)
        {
            string qu = "";
            string[] ch = { "Kingdom", "Phylum", "Class", " Clade", "Order", "Created by", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced by", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };
            for (int i = 0; i < ch.Length; i++)
            {
                if (que.Contains(ch[i]))
                {
                    qu = ch[i];

                }
            }
            return qu;
        }
        public void guess(string name , string url)
        {
            if (name !="")
            {
                Chlabel.Text = name;
                try 
                {
                    var imageStream = HttpWebRequest.Create(url).GetResponse().GetResponseStream();
                    this.pictureBox2.Image = Image.FromStream(imageStream);
                }
                catch
                {
                    var imageStream = HttpWebRequest.Create("https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png").GetResponse().GetResponseStream();
                    this.pictureBox2.Image = Image.FromStream(imageStream);
                 
                }
                
            }
          
           

        }

       
        private void bunifuThinButton22_Click_2(object sender, EventArgs e)
        {
            Form1 lu = new Form1();
            int max = lu.lastcol() - 5;
            if (Textb.Text == "")
            {
                MessageBox.Show("Invalid Input");
            }
            else
            {
                count++;
                addedchara[ni] = Textb.Text;
                newchara.Add(convert(QueLabel.Text) + Textb.Text);
                if (count % 5 == 0)
                {

                    li.result(addedchara[0],max,addedchara[1], addedchara[2], addedchara[3], addedchara[4]);
                    Chlabel.Visible = true;
                    guess(li.getname(), li.getUrl());
                    if (li.getname() == "")
                    {
                        pagetab.SelectTab(2);
                        if (ni >= 4)
                        {
                            ni = -1;
                        }
                    }
                    else
                    {
                        pagetab.SelectTab(3);
                        if (ni >=4)
                        {
                            ni = -1;
                        }
                    }
                  


                }
                else
                {
                    addedchara[ni] = Textb.Text;
                    newchara.Add(convert(QueLabel.Text) + Textb.Text);
                    game(validnodes(), true);
                    if (related.Contains(QueLabel.Text) == false)
                    {
                        related.Add(QueLabel.Text);
                    }
                 
                }

               
                ni++;
            }
          
          
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("left side");
            ColorDialog ui = new ColorDialog();
            if(ui.ShowDialog() == DialogResult.OK)
            {
                bunifuGradientPanel1.GradientBottomLeft = ui.Color;
                bunifuGradientPanel1.GradientTopLeft = ui.Color;
            }
            MessageBox.Show("right side");
            if (ui.ShowDialog() == DialogResult.OK)
            {
                bunifuGradientPanel1.GradientBottomRight = ui.Color;
                bunifuGradientPanel1.GradientTopRight = ui.Color;
            }
            pagetab.SelectTab(1);
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            game(validnodes(), false);
            pagetab.SelectTab(1);
        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Form1 ad = new Form1();
            int max = ad.lastcol() - 5;
            string[] check = newchara.ToArray();
            if (check.Length != 0)
            {
                addchecker(newchara, max);
                tempstore(newchara.ToArray());
            }
          
            ad.Show();
           



        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            game(validnodes(), false);
            pagetab.SelectTab(1);
          
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Array.Clear(addedchara, 0, addedchara.Length);
            li.reset();
            Chlabel.Text = null;
            pictureBox2.Image = null;
            
            game(validnodes(), false);
            pagetab.SelectTab(1);
            ni = 0;
        }
        public  void addchecker(List<String> chara,int max)
        {
            int countre = 0;
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            for(int i = 5; i <= max; i++)
            {
                int n1 = i - 4;
                int n2 = i - 3;
                int n3 = i - 2;
                int n4 = i - 1;
                int n5 = i;
                string query = "select CharacterName from characters Where locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ")";

                for (int n = 0; n < chara.Count; n++)
                {
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara[n]);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    while (re.Read())
                    {
                        re.GetString(0);
                        countre++;
                    }
                    if (countre != 0)
                    {
                        chara.Remove(chara[n]);
                    }
                    con.Close();
                    countre = 0;

                }
            }



        }
        public void tempstore(string[] chara)
        {
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "INSERT IGNORE INTO tempinfo (Info) VALUES (@chara) ";
            for(int i = 0; i < chara.Length; i++)
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@chara", chara[i] );
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result < 0)
                {
                    MessageBox.Show("Error inserting data into Database!");

                }
                con.Close();
            }
           

        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Array.Clear(addedchara, 0, addedchara.Length);
            li.reset();
            used.Clear();
            game(validnodes(), false);
            pagetab.SelectTab(1);
            Chlabel.Text = null;
            pictureBox2.Image = null;
            

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
         bunifuGradientPanel1.GradientBottomLeft = Color.FromArgb(0, 70, 127); ;
         bunifuGradientPanel1.GradientTopLeft = Color.FromArgb(0, 90, 167);
         bunifuGradientPanel1.GradientBottomRight = Color.FromArgb(165, 204, 130);
         bunifuGradientPanel1.GradientTopRight = Color.FromArgb(255, 253, 228);



        }
    }
}
