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
    public partial class Form2 : Form   // 使用時間のフォーム
    {
        private bool alarmFlag = false; // アラームセット用
        private int alarmHour = 0;
        private int alarmMin = 0;
        private int alarmSec = 0;

        private int cnt = 0;                       // タイマーカウント用
        private const int noticeTime = 1000;       // 1秒
        //private const int noticeTime = 60000;    // 1分

        private Random rnd = new Random();  // 乱数生成クラス
        private int rndNum = 0;             // 乱数取得用

        // ランダムコメント
        private string[] comment = new string[]
        {
            "適度に休憩しましょう",
            "軽くストレッチをするのをおすすめします",
            "水分補給は大丈夫ですか？",
            "目を休めましょう",
            "10分程度寝るのも有効です"
        };

        private bool autoFlag = false;      // 自動通知セット用
        private bool helpFlag = false;      // ヘルプボタン用
        

        public Form2() // コンストラクタ
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) // ロード
        {
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            buttonStop1.Enabled = false;
            buttonStop2.Enabled = false;

            timer1.Start(); // タイマー起動
        }

        private void timer1_Tick(object sender, EventArgs e) // timer1_Tickイベント
        {
            DateTime now = DateTime.Now;                                // システム日付を取得
            labelDate.Text = now.ToShortDateString();                   // 今日の日付
            labelTime.Text = now.ToLongTimeString().PadLeft(8, ' ');    // 時刻

            if (alarmFlag == true)
            {
                // 設定時刻になった
                if (alarmHour == now.Hour && alarmMin == now.Minute && alarmSec == now.Second)
                {
                    alarmFlag = false;

                    Form3 Dummy = new Form3();  // ダミーフォーム生成
                    Dummy.Opacity = 0;          // 透明化
                    Dummy.ControlBox = false;   // コントロールボックス無効
                    Dummy.StartPosition = FormStartPosition.CenterScreen;   // 画面中央に表示
                    Dummy.Show();
                    Dummy.TopMost = true;       // 最前面に表示

                    // ダミー画面でメッセージボックス表示（これで最前面にメッセージボックスが表示される）
                    DialogResult result = MessageBox.Show(Dummy, "設定時刻になりました", "アラーム", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        groupBox3.Enabled = true;
                        buttonSet.Enabled = true;
                        buttonStop1.Enabled = false;
                        Dummy.Dispose();
                    }
                }
            }
        }

        private void buttonSet_Click(object sender, EventArgs e)    // アラームを設定ボタンクリック
        {
            DialogResult result = MessageBox.Show("設定しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                groupBox3.Enabled = false;
                buttonSet.Enabled = false;
                buttonStop1.Enabled = true;
                alarmFlag = true;

                label1.Text = numericUpDown1.Value.ToString().PadLeft(2, '0') + ":" + numericUpDown2.Value.ToString().PadLeft(2, '0');

                alarmHour = (int)numericUpDown1.Value;
                alarmMin = (int)numericUpDown2.Value;
                alarmSec = 0;
            }
            else
            {
                return;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)  // 閉じるボタン
        {
            Close();
        }

        private void f2_FormClosing(object sender, FormClosingEventArgs e)  // Form2が閉じられるとき
        {
            Form1.Instance.btnPCEnabled = true;   // ボタン押せるように
        }

        private void buttonAuto_Click(object sender, EventArgs e)   // 自動通知開始ボタンクリック
        {
            DialogResult result = MessageBox.Show("5秒経つごとに通知します\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                groupBox2.Enabled = false;
                buttonAuto.Enabled = false;
                buttonStop2.Enabled = true;
                autoFlag = true;

                timer2.Interval = noticeTime;
                timer2.Start();
            }
            else
            {
                return;
            }
        }

        private void timer2_Tick(object sender, EventArgs e) // timer2_Tickイベント
        {
            if (autoFlag == true)
            {
                cnt++;

                if (cnt == 5)                   // 5秒経ったら
                {
                    rndNum = rnd.Next(5);       // 0～5の乱数取得

                    Form3 Dummy = new Form3();  // ダミーフォーム生成
                    Dummy.Opacity = 0;          // 透明化
                    Dummy.ControlBox = false;   // コントロールボックス無効
                    Dummy.StartPosition = FormStartPosition.CenterScreen;   // 画面中央に表示
                    Dummy.Show();
                    Dummy.TopMost = true;       // 最前面に表示

                    // ダミー画面でメッセージボックス表示（これで最前面にメッセージボックスが表示される）
                    DialogResult result = MessageBox.Show(Dummy, "5秒経ちました\n" + comment[rndNum], "お知らせ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    cnt = 0;

                    if (result == DialogResult.Yes)
                    {
                        timer2.Stop();
                        groupBox2.Enabled = true;
                        buttonAuto.Enabled = true;
                        autoFlag = false;
                        Dummy.Dispose();
                    }

                    Dummy.Dispose();
                }
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)   // ヘルプボタン
        {
            helpFlag = !helpFlag;

            if (helpFlag == true)
            {
                Width += 250;   // 横幅 +250

                label4.Text = "コンピュータ上の現在時刻と日付を表示しています。";
                label5.Text = "時分を設定し、「アラームを設定」ボタンを押すと\n設定した時刻に通知をします。";
                label6.Text = "アラームを設定中に「停止」ボタンを押すと\nアラームの設定を取り消します。";
                label7.Text = "「自動通知開始」ボタンを押すと、\n一定時間（5秒）経過するごとに通知をします。\n「停止」ボタンを押すか、通知のメッセージボックスで\n「はい」を選択することで自動通知を終了できます。";
            }
            else
            {
                Width -= 250;   // 横幅戻す

                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
            }

            //label4.Text = "パソコンの長時間連続使用を防止しましょう。\n自分でアラームを設定するか、\n自動通知開始ボタンを押してください。";
        }

        private void buttonStop2_Click(object sender, EventArgs e)   // 停止ボタン2
        {
            buttonAuto.Enabled = true;
            buttonStop2.Enabled = false;
            groupBox2.Enabled = true;
            autoFlag = false;
            timer2.Stop();
            cnt = 0;
        }

        private void buttonStop1_Click(object sender, EventArgs e)  // 停止ボタン1
        {
            DialogResult result = MessageBox.Show("アラームを停止します。\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                groupBox3.Enabled = true;
                buttonSet.Enabled = true;
                buttonStop1.Enabled = false;
                alarmFlag = false;
            }
            else
            {
                return;
            }
        }
    }
}





