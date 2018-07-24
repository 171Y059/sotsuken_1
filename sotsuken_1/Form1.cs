using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sotsuken_1
{
    public partial class Form1 : Form       // メインのフォーム
    {
        private static Form1 f1;            // Form1オブジェクトを保持するためのフィールド
        private System.Threading.Mutex mu;  // ミューテックス（多重起動防止）


        public Form1()                      // コンストラクタ
        {
            InitializeComponent();
        }

        public static Form1 Instance        // Form1オブジェクトを取得・設定するプロパティ
        {
            get { return f1; }
            set { f1 = value; }
        }

        public bool btnPCEnabled            // 使用時間のボタンを設定するプロパティ
        {
            set { buttonPCTime.Enabled = value; }
        }

        public bool btnSuiminEnabled        // 睡眠時間のボタンを設定するプロパティ
        {
            set { buttonSuiminTime.Enabled = value; }
        }

        private void Form1_Load(object sender, EventArgs e) // ロード
        {
            Instance = this;     // 参照代入

            mu = new System.Threading.Mutex(false, "sotsuken_1");

            if ( mu.WaitOne(0, false) == false )
            {
                MessageBox.Show("既に起動しています", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void buttonPCTime_Click(object sender, EventArgs e) // PC使用時間のフォーム開く
        {
            Form2 f2 = new Form2();
            f2.Show();                              //モードレスなのでDispose()いらない

            if (!f2.IsDisposed)                     // Form2表示中は二重に表示できないように
            {
                buttonPCTime.Enabled = false;       // ボタンを押せなくする
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)  // 閉じる
        {
            mu.Close(); // ミューテックス解放
            Close();
        }

        private void buttonSuiminTime_Click(object sender, EventArgs e) // 睡眠時間のフォーム開く
        {
            Form4 f4 = new Form4();
            f4.Show();

            if (!f4.IsDisposed)
            {
                buttonSuiminTime.Enabled = false;
            }
        }
    }
}
