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
    public partial class Form5 : Form   // 履歴のフォーム
    {
        private MySqlConnection connection = new MySqlConnection();

        public Form5()
        {
            InitializeComponent();
        }

        private bool Connection()   // 接続できたかどうかの関数
        {
            bool flag = true;

            // sotsukenデータベースに接続する文字列
            connection.ConnectionString = "server = localhost; user id = root; password = 1234; database = sotsuken;";

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

        public bool CONNECTION  // 関数の結果を返すプロパティ
        {
            get { return Connection(); }
        }

        private void Form5_Load(object sender, EventArgs e) // ロード
        {
            
            // ListViewのプロパティ  
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.HideSelection = false;    // フォーカス変えたとき選択解除されないように

            listView1.Columns.Add("名前");
            listView1.Columns.Add("年齢");
            listView1.Columns.Add("寝た時間");
            listView1.Columns.Add("起きた時間");
            listView1.Columns.Add("睡眠時間");
            listView1.Columns.Add("推奨に対して");
            listView1.Columns.Add("ID");

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from suimin;";
            cmd.Connection = connection;    // 接続情報

            MySqlDataReader dr = cmd.ExecuteReader();   // データ取得

            while (dr.Read())
            {
                ListViewItem item = new ListViewItem( dr[0].ToString() );   // 0列目のアイテムを生成

                for (int i = 1; i < dr.FieldCount; i++)
                {
                    item.SubItems.Add( dr[i].ToString() );  // サブアイテム（1列目以降）を追加
                }
                
                listView1.Items.Add(item);  // リストビューに追加
            }
            
            // ヘッダーの設定
            foreach (ColumnHeader ch in listView1.Columns)
            {
                ch.Width = -2;                              // 列自動調節（-2を設定する）
                ch.TextAlign = HorizontalAlignment.Right;   // テキスト右寄せ
            }

            connection.Close();

            // 件数カウントする
            connection.Open();

            cmd.CommandText = "select count(ID) from suimin;";
            var retVal = cmd.ExecuteScalar();
            label2.Text = "件数：" + retVal.ToString() + "件";

            connection.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)  // 閉じるボタン
        {
            Close();
        }

        private void buttonDel_Click(object sender, EventArgs e)    // 削除するボタン
        {
            if (listView1.SelectedItems.Count == 0) // 何も選択されていなかったら
            {
                MessageBox.Show("削除したいデータを選択してください", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = listView1.SelectedItems[0];

            DialogResult result = MessageBox.Show("このデータを削除しますか？\n\n名前：" + item.Text + "\n年齢：" + item.SubItems[1].Text + "\n寝た時間：" + item.SubItems[2].Text + "\n起きた時間：" + item.SubItems[3].Text + "\n睡眠時間：" + item.SubItems[4].Text + "\n推奨に対して：" + item.SubItems[5].Text, "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (Connection() == true)
                {   
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = connection;

                    cmd.CommandText = "delete from suimin where ID = " + int.Parse(item.SubItems[6].Text) + ";";

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("削除しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
                else
                {
                    MessageBox.Show("データベースに接続できません。\n削除できませんでした。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                return;
            }
        }
    }
}
