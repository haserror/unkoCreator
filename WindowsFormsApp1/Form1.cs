using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string WORD0 = "う";
        private const string WORD1 = "ん";
        private const string WORD2 = "こ";
        private const string WORD3 = "ー";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "うんこ製造機";
            btnEncrypt.Text = "うんこ→";
            textBox1.Text = "うんこにしたい文字列を入力してね";
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            textBox2.Text = encrypt(textBox1.Text);
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            textBox1.Text = decrypt(textBox2.Text);
        }

        private void BtnAkita_Click(object sender, EventArgs e)
        {
            Close();
        }


        private string encrypt(string s)
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

            rtn = rtn.Replace("0", WORD0);
            rtn = rtn.Replace("1", WORD1);
            rtn = rtn.Replace("2", WORD2);
            rtn = rtn.Replace("3", WORD3);

            return rtn;
        }

        private string decrypt(string s)
        {
            string rtn = "";

            for (int i = 0; i < s.Length; i += 8)
            {
                string val = s.Substring(i, 8);
                val = val.Replace(WORD0, "0");
                val = val.Replace(WORD1, "1");
                val = val.Replace(WORD2, "2");
                val = val.Replace(WORD3, "3");

                int code = (int)RadixConvert.ToDecimal(val, 4);
                rtn += (char)code;
            }

            return rtn;
        }

    }
}
