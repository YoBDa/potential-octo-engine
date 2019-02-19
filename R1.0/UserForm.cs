using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace R1._0
{
    public partial class UserForm : Form
    {
        private string uname = "";
        private string pwd = "";
        private string inn = "";
        private string pass_series = "";
        private string pass_number = "";
        private string full_name = "";
        public UserForm(string username)
        {
            InitializeComponent();
            uname = username;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            {
                string sql = $"SELECT  fullname, inn, passport, password FROM users WHERE nickname='{uname}'";
                MySqlConnection conn = DBUtils.GetDBConnection();
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        full_name = reader.GetString(0);
                        inn = reader.GetString(1);
                        pass_series = reader.GetString(2).Substring(0, 4);
                        pass_number = reader.GetString(2).Substring(4, 6);
                        pwd = reader.GetString(3);
                    }

                    DBConnection_stat.Text = "Connected";
                    uname_label.Text = "Здравствуйте, " + full_name.Split(' ')[1];
                    FIOBox.Text = full_name;
                    passSeriesBox.Text = pass_series;
                    passNumberBox.Text = pass_number;
                    innBox.Text = inn;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    DBConnection_stat.Text = "Disconnected";
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            {
                string sql = $"SELECT  fullname, inn, passport, password FROM users WHERE nickname='{uname}'";
                MySqlConnection conn = DBUtils.GetDBConnection();
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        full_name = reader.GetString(0);
                        inn = reader.GetString(1);
                        pass_series = reader.GetString(2).Substring(0, 4);
                        pass_number = reader.GetString(2).Substring(4, 6);
                        pwd = reader.GetString(3);
                    }

                    DBConnection_stat.Text = "Connected";
                    uname_label.Text = "Здравствуйте, " + full_name.Split(' ')[1];
                    FIOBox.Text = full_name;
                    passSeriesBox.Text = pass_series;
                    passNumberBox.Text = pass_number;
                    innBox.Text = inn;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    DBConnection_stat.Text = "Disconnected";
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE users SET fullname = {FIOBox.Text}, inn = {innBox}," +
                $" passport = {passSeriesBox.Text+passNumberBox.Text} WHERE nickname='{uname}'";
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

        }
    }
}
