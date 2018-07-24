using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sotsuken_1
{
    public partial class Form4 : Form   // 睡眠時間のフォーム
    {
        private int neHour = 0;     // 寝た時
        private int neMin = 0;      // 寝た分
        private int okiHour = 0;    // 起きた時
        private int okiMin = 0;     // 起きた分

        private int okiSum = 0;     // 起きた時 + 起きた分
        private int neSum = 0;      // 寝た時 + 寝た分

        private int hour = 0;       // 睡眠時間
        private int min = 0;        // 分

        private int age = 0;        // 年齢
        private decimal suiMin = 0; // 睡眠時間（分単位）
        private string str1, str2;  // labelに表示する用

        private MySqlConnection connection = new MySqlConnection();

        public Form4()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)  // 閉じるボタン
        {
            Close();
        }

        private void f4_FormClosing(object sender, FormClosingEventArgs e)  // Form4が閉じられるとき
        {
            Form1.Instance.btnSuiminEnabled = true; // ボタン押せるように
        }

        private void Form4_Load(object sender, EventArgs e) // ロード
        {
            // sotsukenデータベースに接続する文字列
            connection.ConnectionString = "server = localhost; user id = root; password = 1234; database = sotsuken;";

            // 文字数制限 12文字
            textBox1.MaxLength = 12;

            label7.Text = "";
            label11.Text = "";
            label14.Text = "";
            label16.Text = "";
        }

        private void buttonHis_Click(object sender, EventArgs e)    // 履歴ボタン
        {
            Form5 f5 = new Form5();

            if (f5.CONNECTION)
            {
                f5.ShowDialog();
            }
            else
            {
                MessageBox.Show("データベースに接続できませんでした。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)   // 計算ボタン
        {
            if (textBox1.Text != "")
            {
                label16.Text = textBox1.Text + "さんの結果";
            }
            else
            {
                label16.Text = "名無しさんの結果";
            }

            Calc(); // 睡眠時間計算

            Result();   // 質判定

            // 履歴残す
            if ( Connection() == true )
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "insert into suimin(名前, 年齢, 寝た時間, 起きた時間, 睡眠時間, 推奨に対して) values('" + 
                                                        textBox1.Text + "', " + 
                                                        (int)numericUpDown5.Value + "," + 
                                                        "'" + numericUpDown1.Value.ToString() + "時" + numericUpDown2.Value.ToString() + "分" + "'," + 
                                                        "'" + numericUpDown3.Value.ToString() + "時" + numericUpDown4.Value.ToString() + "分" + "'," + 
                                                        "'" + hour.ToString() + "時間" + min.ToString() + "分" + "'," + 
                                                        "'" + label14.Text + "'" + ");";

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            else
            {
                MessageBox.Show("データベースに接続できませんでした。\nこの履歴は残りません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Calc() // 睡眠時間計算
        {
            // 寝た時分代入
            neHour = (int)numericUpDown1.Value * 60;    // * 60 → 分計算
            neMin = (int)numericUpDown2.Value;

            // 起きた時分代入
            okiHour = (int)numericUpDown3.Value * 60;
            okiMin = (int)numericUpDown4.Value;

            // 寝た時 + 寝た分
            neSum = neHour + neMin;

            // 起きた時 + 起きた分
            if (okiHour < neHour)   // 日付跨いだ場合は
            {
                okiSum = (okiHour + 24 * 60) + okiMin;  // 起きた時間に24時間足す
            }
            else
            {
                okiSum = okiHour + okiMin;
            }

            // (起きた時分 - 寝た時分) / 60　で　睡眠時間算出
            hour = (okiSum - neSum) / 60;

            if (hour == 0)  // 0だったら
            {
                hour = 24;  // 24時間以上寝てる
            }

            // (起きた時分 - 寝た時分) % 60　で　分単位の睡眠時間算出
            min = (okiSum - neSum) % 60;

            label7.Text = hour.ToString() + "時間" + min.ToString() + "分";
        }

        private void Result()   // 質判定
        {
            age = (int)numericUpDown5.Value;

            suiMin = hour * 60 + min;  // 睡眠時間（分単位）

            suiMin = Math.Round(suiMin / 60);   // 四捨五入　1:29 → 1:00　　1:30 → 2:00

            if (age == 0)
            {
                str1 = "12～15時間";

                if (suiMin >= 12 && suiMin <= 15)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 12)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else if (age <= 2)
            {
                str1 = "11～14時間";

                if (suiMin >= 11 && suiMin <= 14)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 11)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else if (age <= 5)
            {
                str1 = "10～13時間";

                if (suiMin >= 10 && suiMin <= 13)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 10)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else if (age <= 13)
            {
                str1 = "9～11時間";

                if (suiMin >= 9 && suiMin <= 11)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 9)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else if (age <= 17)
            {
                str1 = "8～10時間";

                if (suiMin >= 8 && suiMin <= 10)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 8)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else if (age <= 64)
            {
                str1 = "7～9時間";

                if (suiMin >= 7 && suiMin <= 9)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 7)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }
            else
            {
                str1 = "7～8時間";

                if (suiMin >= 7 && suiMin <= 8)
                {
                    str2 = "適しています";
                }
                else if (suiMin < 7)
                {
                    str2 = "少ないです";
                }
                else
                {
                    str2 = "多いです";
                }
            }

            label11.Text = str1;
            label14.Text = str2;
        }

        private bool Connection()   // 接続できたかどうかの関数
        {
            bool flag = true;

            try
            {
                connection.Open();

                return flag;
            }
            catch (Exception ex)
            {
                connection.Close();
                flag = false;

                return flag;
            }
        }
    }
}
