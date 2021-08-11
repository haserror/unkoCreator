using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_NETA = "うんこー";
        private string[] word = { "", "", "", "" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSeed.Text = DEFAULT_NETA;
            ChangeNeta(DEFAULT_NETA);
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            textBox2.Text = Encrypt(textBox1.Text);
            textBox1.Text = "";
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            string txt = textBox2.Text.Trim();

            if (txt.Length % 8 != 0)
            {
                MessageBox.Show("テキスト不正で復元できなさそう");
                return;
            }

            txt = txt.Replace(word[0], "");
            txt = txt.Replace(word[1], "");
            txt = txt.Replace(word[2], "");
            txt = txt.Replace(word[3], "");
            if (txt.Length != 0)
            {
                MessageBox.Show("テキスト不正で復元できなさそう");
                return;
            }

            textBox1.Text = Decrypt(textBox2.Text);
            textBox2.Text = "";
        }

        private void BtnAkita_Click(object sender, EventArgs e)
        {
            Close();
        }


        private string Encrypt(string s)
        {
            string rtn = "";

            foreach(char c in s.ToCharArray())
            {
                int code = (int)c;
                string encoded = RadixConvert.ToString(code, 4, true);
                for(int l = encoded.Length; l < 8; l++)
                {
                    encoded = "0" + encoded;
                }

                rtn += encoded;
            }

            rtn = rtn.Replace("0", word[0]);
            rtn = rtn.Replace("1", word[1]);
            rtn = rtn.Replace("2", word[2]);
            rtn = rtn.Replace("3", word[3]);

            return rtn;
        }

        private string Decrypt(string s)
        {
            string rtn = "";

            for (int i = 0; i < s.Length; i += 8)
            {
                string val = s.Substring(i, 8);
                val = val.Replace(word[0], "0");
                val = val.Replace(word[1], "1");
                val = val.Replace(word[2], "2");
                val = val.Replace(word[3], "3");

                int code = (int)RadixConvert.ToDecimal(val, 4);
                rtn += (char)code;
            }

            return rtn;
        }

        private void TxtSeed_Leave(object sender, EventArgs e)
        {
            string neta = txtSeed.Text.Trim();
            if (neta.Length != 4)
            {
                MessageBox.Show("ネタは4文字にしてね");
                txtSeed.Focus();
                return;
            }

            ChangeNeta(neta);
        }

        private void ChangeNeta(string neta)
        {
            word[0] = neta.Substring(0, 1);
            word[1] = neta.Substring(1, 1);
            word[2] = neta.Substring(2, 1);
            word[3] = neta.Substring(3, 1);

            string netaText = neta;
            if (word[3] == "ー")
            {
                // 苦肉の策
                netaText = neta.Replace("ー", "");
            }

            lblTitle.Text = netaText + "製造機";
            btnEncrypt.Text = netaText + "製造→";
            textBox1.Text = netaText + "にしたい文字列を入力してね";
        }
    }
}
