using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;



namespace SurveyQuestionsConfigurator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string questiontext = textBox1.Text;
            int questionOrder = (int)numericUpDown1.Value;

            string connectionString =
    ConfigurationManager
        .ConnectionStrings["SurveyDb"]
        .ConnectionString;

            string sql = "INSERT INTO Questions (question_text,question_order,question_type) VALUES (@questiontext,@order,1)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@questiontext", questiontext);
                    cmd.Parameters.AddWithValue("@order", questionOrder);
                    con.Open();

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"{rows} row inserted");

                }




            }
            textBox1.Clear();
            numericUpDown1.Value = 0;
        }
    }
}

