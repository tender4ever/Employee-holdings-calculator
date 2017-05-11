using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmployeeTrust
{
    public partial class Form1 : Form
    {
        public int a;                           //員工自提金
        public int countA = 0;                  //累積員工自提金
        public int a1;                          //員工自提金返還15%

        public int b;                           //公司獎勵金
        public int countB = 0;                  //累積公司獎勵金
        public int b1;                          //公司獎勵金返還15%

        public int countAB = 0;                 //總計累積領回 
        public int c1;                          //總返還
        public int totalValue = 0;              //帳戶總價值
        public double c;                        //投資報酬率
        public int d;                           //離職獎勵金
        
        public Form1()
        {
            InitializeComponent();
        }

        public void beforeBackMoney(int i, int countA, int countB, int totalValue) {
            richTextBox1.Text += "\n";
            richTextBox1.Text += "-----------------------------------------------------------------------------------------------------------";
            richTextBox1.Text += "\n" + "\n" + "第" + i + "年" + "\n";
            richTextBox1.Text += "自提金累積金額 : " + countA + " + ";
            richTextBox1.Text += "獎勵金累積金額 : " + countB + " = ";
            richTextBox1.Text += "帳戶總價值 : " + totalValue + "\n";
        }

        public void afterBackMoney(int a1, int b1, int c1, int countAB, int countA, int countB, int totalValue, int d, double c) {
            richTextBox1.Text += "\n";
            richTextBox1.Text += "自提金領回15% : " + a1 + "\n";
            richTextBox1.Text += "獎勵金領回15% : " + b1 + "\n";
            richTextBox1.Text += "總領回 : " + c1 + "\n";
            richTextBox1.Text += "總計累積領回 : " + countAB + "\n";
            richTextBox1.Text += "\n";
            richTextBox1.Text += "自提金累積金額 : " + countA + " + ";
            richTextBox1.Text += "獎勵金累積金額 : " + countB + " = ";
            richTextBox1.Text += "帳戶總價值 : " + totalValue + "\n";
            richTextBox1.Text += "\n";
            richTextBox1.Text += "離職可獲得的獎勵金 : " + d + "\n";
            richTextBox1.Text += "投資報酬率 : (獎勵金領回15% + 離職可獲得的獎勵金) / 自提金累積金額 = " + c + "%" + "\n";
        }
        //公司獎勵金提撥比例
        public void percentMoney(int i) {
            a = Convert.ToInt32(textBox1.Text);
            if (i < 3)
            {
                b = a;
            }
            else if (i >= 3 && i < 6) {
                b = (int)Math.Round(a * 1.2);
            }
            else if (i >= 6 && i < 10)
            {
                b = (int)Math.Round(a * 1.4);
            }
            else {
                b = (int)Math.Round(a * 1.5);
            }
            countA += a;
            countB += b;
            totalValue = countA + countB;
        }
        //每年可返還的股票與現金、總返還金額、累積自提金、累積獎勵金、投資報酬率
        public void backMoney(int i) {
            a1 = countA * 15 / 100;
            b1 = countB * 15 / 100;
            c1 = a1 + b1;
            countAB += c1;
            countA -= a1;
            countB -= b1;
            totalValue = countA + countB;
            retireMoney(i);
            
            c = Math.Round((double)(b1 + d) / countA * 100);
        }
        //離職時，獲得的公司獎勵金
        public void retireMoney(int i) { 
            if(i < 6){
                d = countB * 0 / 100;
            }
            else if(i >= 6 && i < 10){
                d = countB * 30 / 100;
            }
            else if (i >= 10 && i < 15) {
                d = countB * 50 / 100;
            }
            else if (i >= 15 && i < 20)
            {
                d = countB * 70 / 100;
            }
            else {
                d = countB * 100 / 100;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 20; i++) {

                if (i < 3) {
                    
                    percentMoney(i);
                    beforeBackMoney(i, countA, countB, totalValue);
                }
                else if (i >= 3 && i < 6) {
                    
                    percentMoney(i);
                    beforeBackMoney(i, countA, countB, totalValue);
                    backMoney(i);
                    afterBackMoney(a1, b1, c1, countAB, countA, countB, totalValue, d, c);
                }
                else if (i >= 6 && i < 10)
                {
                    percentMoney(i);
                    beforeBackMoney(i, countA, countB, totalValue);
                    backMoney(i);
                    afterBackMoney(a1, b1, c1, countAB, countA, countB, totalValue, d, c);
                }
                else {
                    percentMoney(i);
                    beforeBackMoney(i, countA, countB, totalValue);
                    backMoney(i);
                    afterBackMoney(a1, b1, c1, countAB, countA, countB, totalValue, d, c);
                }
            }
        }
    }
}
