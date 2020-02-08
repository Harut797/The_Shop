﻿using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace The_Shop
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
            try
            {
                DbConnector.conn.Open();
            }
            catch
            {
                MessageBox.Show("Can't connect to server...");
                this.Close();
            }
        }

        private void SignButton_Click_1(object sender, EventArgs e)
        {
            if (EmailBox.Text != "")
            {
                if (passwordBox.Text != "")
                {
                    MySqlCommand mysql_query = DbConnector.conn.CreateCommand();
                    mysql_query.CommandText = $"SELECT Name,Surname,Email,Password,Level,Money FROM Persons";
                    MySqlDataReader mysql_result;
                    mysql_result = mysql_query.ExecuteReader();
                    while (mysql_result.Read())
                    {
                        if (EmailBox.Text == mysql_result.GetString(2).ToString())
                        {
                            if (passwordBox.Text == mysql_result.GetString(3).ToString())
                            {
                                Account.name = mysql_result.GetString(0).ToString();
                                Account.surname = mysql_result.GetString(1).ToString();
                                Account.email = mysql_result.GetString(2).ToString();
                                Account.level = mysql_result.GetString(4).ToString();
                                Account.money = mysql_result.GetString(5).ToString();

                                //if (mysql_result.GetString(3).ToString() == "Admin")
                                //    Account.admin = true;
                                MessageBox.Show($"Account {Account.name} has been signed...");
                                //Account.signed = true;
                                var ShForm = new ShopForm();
                                ShForm.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Wrong Password!");
                                break;
                            }
                            break;
                        }
                    }
                    MessageBox.Show("Account not found...");
                    mysql_result.Close();
                }
                else
                {
                    passwordBox.BackColor = Color.Brown;
                    MessageBox.Show("Password box is empty");
                }
            }
            else
            {
                EmailBox.BackColor = Color.Brown;
                MessageBox.Show("Nickname or Email box is empty");
            }
        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {
            EmailBox.BackColor = Color.Black;
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            passwordBox.BackColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var formm = new WelcomeForm();
            formm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            var formm = new WelcomeForm();
            formm.Show();
            this.Close();
        }
        int tmpX, tmpY;
        bool mousedown;

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousedown)
            {
                this.Left = this.Left + (Cursor.Position.X - tmpX);
                this.Top = this.Top + (Cursor.Position.Y - tmpY);

                tmpX = Cursor.Position.X;
                tmpY = Cursor.Position.Y;
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            tmpX = Cursor.Position.X;
            tmpY = Cursor.Position.Y;
            mousedown = true;
        }
    }
}
