using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test;
using Vison;


namespace TestWinform
{
    public partial class Test_Classify : Form
    {
        public static Test_Classify form;
        public Test_Classify()
        {
            InitializeComponent();
            form = this;
        }
        
        //定义全局变量
        public delegate void MyAction(string path, double index, out HObject imgOut , out double type);
        private static string g_strStaticPath = null;
        private static double g_dStaticType = 0;
        private static int g_iIndex = 0;
         
        /// <summary>
        /// 加载图片文件目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton_Path_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "打开图像文件路径";
                ofd.FileName = "";
                ofd.Filter = "bmp(*.bmp)|*.bmp|png(*.png)|*.png|jpg(*.jpg)|*.jpg|所有文件(*.*)|*.*";
                ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                     string staticInt = ofd.FileName;
                     int index = staticInt.LastIndexOf("\\");
                    g_strStaticPath = staticInt.Remove(index);
                }
            }
        }

        
        /// <summary>
        /// 执行下一个图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiButton_Classify_Click(object sender, EventArgs e)
        {
            Vison vison = new Vison();
            HObject m_imgout = null;
            string NewstaticPath = g_strStaticPath.Replace ("\\", "/");
            HTuple  hv_ImageFiles = null;
            HOperatorSet.ListFiles(NewstaticPath, ((new HTuple("files")).TupleConcat(
                   "follow_links")).TupleConcat("recursive"), out hv_ImageFiles);
            HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                "ignore_case"), out hv_ImageFiles);
            MyAction myAction =new MyAction(vison.ClassifyOfHimage); 

            if ((int)(hv_ImageFiles.TupleLength()-1)>= g_iIndex)
            {
                myAction.Invoke(g_strStaticPath, g_iIndex, out m_imgout , out g_dStaticType);
                vison.HobjectToHimage(m_imgout);
                TextBox_Type.Text = Convert.ToString(g_dStaticType);
                g_iIndex++;
            }
            else
            {
                MessageBox.Show("已经到最后一张了");
                g_iIndex--;
                myAction.Invoke(g_strStaticPath, g_iIndex, out m_imgout, out g_dStaticType);
                vison.HobjectToHimage(m_imgout);
                TextBox_Type.Text = Convert.ToString(g_dStaticType);
            }
        }

      
    }
}
