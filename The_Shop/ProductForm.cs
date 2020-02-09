﻿using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace The_Shop
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            try
            {
                DbConnector.conn.Open();
            }
            catch
            {
            }
            try
            {
                RefreshList();
            }
            catch
            {
                MessageBox.Show("Can't connect to server...");
            }
        }
        private void RefreshList()
        {
            ProductListBox.Items.Clear();
            MySqlCommand mysql_query = DbConnector.conn.CreateCommand();
            mysql_query.CommandText = $"SELECT ID,Name,Quantity FROM Warehouse";
            MySqlDataReader mysql_result;
            mysql_result = mysql_query.ExecuteReader();
            while (mysql_result.Read())
            {
                ProductListBox.Items.Add(mysql_result.GetString(0).ToString() + " " + " " + mysql_result.GetString(1).ToString() + " - " + mysql_result.GetString(2).ToString());
            }
            mysql_result.Close();
        }
        int idProd;
        private void ProductListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string textString = ProductListBox.SelectedItem.ToString();
            string idText = textString.Substring(0, 2);
            idProd = int.Parse(idText);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (ProductListBox.SelectedIndex != -1)
            {
                if (PriceBox.Value > 1 && PriceBox.Value < 100)
                {
                    MySqlCommand mysql_query = DbConnector.conn.CreateCommand();
                    mysql_query.CommandText = $"SELECT ID,Name,Quantity,Picture FROM Warehouse WHERE ID = '{idProd}'";
                    MySqlDataReader mysql_result;
                    mysql_result = mysql_query.ExecuteReader();
                    while (mysql_result.Read())
                    {
                        Product.name = mysql_result.GetString(1).ToString();
                        Product.picture = mysql_result.GetString(3).ToString();
                    }
                    Product.price = (double)PriceBox.Value;
                    Product.quantity = (int)QuantityBox.Value;
                    mysql_result.Close();
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show("Price must be greater than 0 and smaller than 100");
                
            }
            else
                MessageBox.Show("Select product to add");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
        int tmpX, tmpY;
        bool mousedown;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            tmpX = Cursor.Position.X;
            tmpY = Cursor.Position.Y;
            mousedown = true;
        }

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
    }
}
