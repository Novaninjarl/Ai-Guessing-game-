using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace web_scraping
{
    internal class Ai 
    {
        string name = "";
        string url = "";
        int Max = 100000000;
        int Mini = -100000000;
        List<int> usedque = new List<int>();
                                           

        public int MiniMax(int depth, int nodeindex, bool Aiturn, int[] nodesval, int alpha, int beta)
        {


            if (depth == 3)
            {

                return nodesval[nodeindex];

            }

            if (Aiturn)
            {
                int bestques = Mini;
                for (int i = 0; i < 2; i++)
                {
                    int pop = MiniMax(depth + 1, (nodeindex * 2) + i, false, nodesval, alpha, beta);
                    bestques = Math.Max(bestques, pop);
                    alpha = Math.Max(alpha, pop);
                    if (beta <= alpha)
                    {
                        break;
                    }

                }




                return bestques; 
            }
            else
            {
                int bestans = Max;

                for (int i = 0; i < 2; i++)
                {
                    int pop = MiniMax(depth + 1, (nodeindex * 2) + i, true, nodesval, alpha, beta);
                    bestans = Math.Min(bestans, pop);
                    beta = Math.Min(beta, pop);
                    if (beta <= alpha)
                    {
                        break;
                    }

                }


                return bestans;

            }
        }
        public void invalidque(string id)
        {

            string que = "";
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlConnection con2 = new MySqlConnection(connection);
            string query = " select count(CharactersId) from characters where (locate(@char,info1) or    locate(@char,info2) or  locate(@char,info3) or   locate(@char,info4) or   locate(@char,info5) )and  ( locate(@char2,info1) or    locate(@char2,info2) or  locate(@char2,info3) or   locate(@char2,info4) or   locate(@char2,info5))";
            string query2 = "select quepop from questions where locate(@char2,QuestionForm)";

            string[] ch = { "Kingdom:", "Phylum:", "Class:", " Clade:", "Order:", "Created by", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced by", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };

            for (int i = 0; i < ch.Length; i++)
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@char", queform(id));
                cmd.Parameters.AddWithValue("@char2", ch[i]);
                con.Open();
                MySqlDataReader re = cmd.ExecuteReader();
                que = ch[i];
                while (re.Read())
                {
                    int pop = int.Parse(re.GetString(0));


                    if (pop == 0)
                    {

                        MySqlCommand cm = new MySqlCommand(query2, con2);
                        cm.Parameters.AddWithValue("@char2", que);
                        con2.Open();
                        MySqlDataReader r = cm.ExecuteReader();
                        while (r.Read())
                        {
                            usedque.Add(int.Parse(r.GetString(0)));

                        }
                        con2.Close();

                    }
                }
                con.Close();

            }





        }
        public string solution(string name, string charform)
        {
            string sol = "";
            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            string query = "select CharacteristicsId from characteristics where CharactersName=@name and Charform= @char ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@char", charform);
            con.Open();
            MySqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {
                sol = re.GetString(0);

            }
            con.Close();

            return sol;

        }
        public List<int> turn()
        {
            return usedque;
        }
        static string queform(string que)
        {
            string qu = "";
            string[] ch = { "Kingdom:", "Phylum:", "Class:", " Clade:", "Order:", "Created by", "Parent(s)", "Spouse(s)", "Born", "Gender", "Occupation", "Children", "Age", "Died", "Voiced by", "Species", "Based on", "Origin", "Affiliation", "Status", "First appearance", "First game", "Author", "Country", "Genre", "Team affiliations", "Abilities", "Place of origin", "Founded", "Enemies", "Powers/Skills", "Alias", "Role", "Aliases" };
            for (int i = 0; i < ch.Length; i++)
            {
                if (que.Contains(ch[i]))
                {
                    qu = ch[i];

                }
            }
            return qu;
        }
        public void result(string chara, int max, string chara2, string chara3 , string chara4 , string chara5 )
        {

            var connection = "server=localhost;database=lectorem;user id=root;password=Gumballninja100;";
            MySqlConnection con = new MySqlConnection(connection);
            if (chara2 == null && chara3 == null && chara5 == null && chara4 == null && chara != null)
            {
                for (int i = 5; i <= max; i+=5)
                {
                    int n1 = i - 4;
                    int n2 = i - 3;
                    int n3 = i - 2;
                    int n4 = i - 1;
                    int n5 = i;
                    string query = "select * from characters Where locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ") order by Popularity asc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    int k = 0;

                    while (re.Read())
                    {
                        if (k == 0)
                        {
                            name = re.GetString(1);
                            url = re.GetString(2);
                        }
                        k++;

                    }

                    con.Close();

                }
               
            }
           
            if (chara2!=null&&chara3==null&&chara5==null&&chara4==null&&chara!=null)
            {
                for (int i = 5; i <= max; i += 5)
                {
                    int n1 = i - 4;
                    int n2 = i - 3;
                    int n3 = i - 2;
                    int n4 = i - 1;
                    int n5 = i;

                    string query = "select * from characters Where (locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + ")) and (locate(@char2, " + "info" + n1 + ") or locate(@char2," + "info" + n2 + " ) or locate(@char2," + "info" + n3 + ")or locate(@char2," + "info" + n4 + ")or locate(@char2, " + "info" + n5 + ")) order by Popularity asc";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara);
                    cmd.Parameters.AddWithValue("@char2", chara2);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    int k = 0;

                    while (re.Read())
                    {
                        if (k == 0)
                        {
                            name = re.GetString(1);
                            url = re.GetString(2);
                        }
                        k++;

                    }

                    con.Close();

                }
              
            }
            if (chara2 != null && chara3 != null && chara5 == null && chara4 == null && chara != null)
            {
                for (int i = 5; i <= max; i += 5)
                {
                    int n1 = i - 4;
                    int n2 = i - 3;
                    int n3 = i - 2;
                    int n4 = i - 1;
                    int n5 = i;

                    string query = "select * from characters Where (locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + "))and (locate(@char2, " + "info" + n1 + ") or locate(@char2," + "info" + n2 + " ) or locate(@char2," + "info" + n3 + ")or locate(@char2," + "info" + n4 + ")or locate(@char2," + "info" + n5 + "))and (locate(@char3," + "info" + n1 + ") or locate(@char3," + "info" + n2 + " ) or locate(@char3," + "info" + n3 + ")or locate(@char3," + "info" + n4 + ")or locate(@char3, " + "info" + n5 + ")) order by Popularity asc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara);
                    cmd.Parameters.AddWithValue("@char2", chara2);
                    cmd.Parameters.AddWithValue("@char3", chara3);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    int k = 0;

                    while (re.Read())
                    {
                        if (k == 0)
                        {
                            name = re.GetString(1);
                            url = re.GetString(2);
                        }
                        k++;

                    }

                    con.Close();


                }
               
            }
            if (chara2 != null && chara3 != null && chara5 == null && chara4 != null && chara != null)
            {
                for (int i = 5; i <= max; i += 5)
                {
                    int n1 = i - 4;
                    int n2 = i - 3;
                    int n3 = i - 2;
                    int n4 = i - 1;
                    int n5 = i;
                    string query = "select * from characters Where (locate(@char , " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, "+ "info" + n5 + "))and (locate(@char2, " + "info" + n1 + ") or locate(@char2," + "info" + n2 + " ) or locate(@char2," + "info" + n3 + ")or locate(@char2," + "info" + n4 + ")or locate(@char2," + "info" + n5 + "))and (locate(@char3," + "info" + n1 + ") or locate(@char3," + "info" + n2 + " ) or locate(@char3," + "info" + n3 + ")or locate(@char3, " + "info" + n4 + ")or locate(@char3, " + "info" + n5 + "))and (locate(@char4," + "info" + n1 + ") or locate(@char4," + "info" + n2 + " ) or locate(@char4," + "info" + n3 + ")or locate(@char4," + "info" + n4 + ")or locate(@char4," + "info" + n5 + ")) order by Popularity asc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara);
                    cmd.Parameters.AddWithValue("@char2", chara2);
                    cmd.Parameters.AddWithValue("@char3", chara3);
                    cmd.Parameters.AddWithValue("@char4", chara4);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    int k = 0;

                    while (re.Read())
                    {
                        if (k == 0)
                        {
                            name = re.GetString(1);
                            url = re.GetString(2);
                        }
                        k++;

                    }

                    con.Close();
                }
              
            }
            if (chara2 != null && chara3 != null && chara5 != null && chara4 != null && chara != null)
            {
                for (int i = 5; i <= max; i += 5)
                {
                    int n1 = i - 4;
                    int n2 = i - 3;
                    int n3 = i - 2;
                    int n4 = i - 1;
                    int n5 = i;
                    string query = "select * from  characters Where (locate(@char, " + "info" + n1 + ") or locate(@char," + "info" + n2 + " ) or locate(@char," + "info" + n3 + ")or locate(@char, " + "info" + n4 + ")or locate(@char, " + "info" + n5 + "))and (locate(@char2, " + "info" + n1 + ") or locate(@char2," + "info" + n2 + " ) or locate(@char2," + "info" + n3 + ")or locate(@char2," + "info" + n4 + ")or locate(@char2," + "info" + n5 + "))and (locate(@char3," + "info" + n1 + ") or locate(@char3," + "info" + n2 + " ) or locate(@char3," + "info" + n3 + ")or locate(@char3," + "info" + n4 + ")or locate(@char3, " + "info" + n5 + "))and (locate(@char4," + "info" + n1 + ") or locate(@char4," + "info" + n2 + " ) or locate(@char4," + "info" + n3 + ")or locate(@char4," + "info" + n4 + ")or locate(@char4," + "info" + n5 + "))and (locate(@char5," + "info" + n1 + ") or locate(@char5," + "info" + n2 + " ) or locate(@char5," + "info" + n3 + ")or locate(@char5," + "info" + n4 + ")or locate(@char5," + "info" + n5 + ")) order by Popularity asc";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@char", chara);
                    cmd.Parameters.AddWithValue("@char2", chara2);
                    cmd.Parameters.AddWithValue("@char3", chara3);
                    cmd.Parameters.AddWithValue("@char4", chara4);
                    cmd.Parameters.AddWithValue("@char5", chara5);
                    con.Open();
                    MySqlDataReader re = cmd.ExecuteReader();
                    int k = 0;

                    while (re.Read())
                    {
                        if (k == 0)
                        {
                            name = re.GetString(1);
                            url = re.GetString(2);
                        }
                        k++;

                    }

                    con.Close();

                }
               
            }





        }
        public string getname()
        {
            return name;
        }
        public string getUrl()
        {
            return url;
        }
        public void reset()
        {
            name = "";
            url = "";
           
        }


    }

}
