using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {

        Dictionary<string, List<string>> itemDict = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();


            comboBox1.Items.Add("Player");
            comboBox1.Items.Add("Enemies");
            comboBox1.Items.Add("Tiles");
            

            List<string> playerList = new List<string>();
            playerList.Add("Player");

            List<string> enemyList = new List<string>();
            enemyList.Add("Homing");
            enemyList.Add("Roaming");
            enemyList.Add("Flying");
            enemyList.Add("Jumping");

            List<string> tileList = new List<string>();
            tileList.Add("Solid Ground");
            tileList.Add("Breakable Block");
            tileList.Add("Background");

            itemDict.Add("Player", playerList);
            itemDict.Add("Enemies", enemyList);
            itemDict.Add("Tiles", tileList);

            comboBox1.Text = "Player";

            listView1.View = View.Tile;
            //listView1.Items.Add("Player");

            //listView1.Items.Add("Enemy 1");
            //listView1.Items.Add("Enemy 2");

            //listView1.Items.Add("Solid Ground");
            //listView1.Items.Add("Breakable Block");
            //listView1.Items.Add("Background");

            comboBox2.Items.Add("Basic");
            comboBox2.Text = "Basic";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Clear();

            List<string> itemList = itemDict[comboBox1.Text];

            foreach (string s in itemList)
            {
                listView1.Items.Add(s);
            }
        }
    }
}
