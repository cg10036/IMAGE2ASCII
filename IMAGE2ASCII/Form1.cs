using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMAGE2ASCII
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Clipboard.SetText(IMAGE2ASCII((Bitmap)Image.FromFile(openFileDialog.FileName)));
                MessageBox.Show("Copied!");
            }
            else
            {
                MessageBox.Show("Cancled!");
            }
        }

        private unsafe string IMAGE2ASCII(Bitmap bitmap)
        {
            StringBuilder stringBuilder = new StringBuilder();
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* scan0 = (byte*)bitmapData.Scan0.ToPointer();
            for(int i = 0;i < bitmapData.Height;i++)
            {
                byte* row = scan0 + (i * bitmapData.Stride);
                for(int j = 0;j < bitmapData.Width;j++)
                {
                    Color color = Color.FromArgb(row[j * 3 + 2], row[j * 3 + 1], row[j * 3]);
                    switch(Convert.ToInt32(color.GetBrightness() * 10))
                    {
                        case 0:
                            stringBuilder.Append(" ");
                            break;
                        case 1:
                            stringBuilder.Append(".");
                            break;
                        case 2:
                            stringBuilder.Append("-");
                            break;
                        case 3:
                            stringBuilder.Append(":");
                            break;
                        case 4:
                            stringBuilder.Append("*");
                            break;
                        case 5:
                            stringBuilder.Append("+");
                            break;
                        case 6:
                            stringBuilder.Append("=");
                            break;
                        case 7:
                            stringBuilder.Append("%");
                            break;
                        case 8:
                            stringBuilder.Append("@");
                            break;
                        case 9:
                            stringBuilder.Append("#");
                            break;
                        case 10:
                            stringBuilder.Append("#");
                            break;
                    }
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}
