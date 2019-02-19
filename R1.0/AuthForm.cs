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
    public partial class AuthForm : Form
    {
        private bool isLogged = false;
        public AuthForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        public int CheckUser(string login, string password)
        {
            string sql = "SELECT nickname, inn, password, flag from users";
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if ((reader.GetValue(0).ToString() == login | reader.GetValue(1).ToString() == login) & (reader.GetValue(2).ToString() == password)){
                            if (Convert.ToInt32(reader.GetValue(3)) == 0)
                                return 0;
                            else if(Convert.ToInt32(reader.GetValue(3)) == 1)
                                return 1;
                            else if (Convert.ToInt32(reader.GetValue(3)) == 2)
                                return 2;
                        } else
                        {
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1;
            }
            return -1;
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            if(usernameBox.Text.Length > 0 & passwordBox.Text.Length > 0)
            {
                int getUserFlag = CheckUser(usernameBox.Text, passwordBox.Text);
                if (getUserFlag != -1)
                {
                    if (getUserFlag == 0)
                    {
                        UserForm uf = new UserForm();
                        uf.Show();
                        this.Visible = false;
                    }
                    else if (getUserFlag == 1)
                    {
                        RieltorForm rf = new RieltorForm();
                        rf.Show();
                        this.Visible = false;
                    }
                    else if (getUserFlag == 2)
                    {
                        AdminForm af = new AdminForm();
                        af.Show();
                        this.Visible = false;
                    }
                }
                
            }
        }
    }
}

