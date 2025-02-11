using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestWinform;

namespace Test
{
    internal class Vison 
    {


        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="m_img">输入图片</param>
        public void HobjectToHimage(HObject m_img)
        {

            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HOperatorSet.GetImageSize(m_img, out hv_Width, out hv_Height);
            HOperatorSet.SetPart(Test_Classify.form.hWindow_img.HalconWindow, 0, 0, (hv_Height - 1), (hv_Width - 1));
            HOperatorSet.DispObj(m_img, Test_Classify.form.hWindow_img.HalconWindow);

        }   





        /// <summary>
        /// halcon的图像分类算法
        /// </summary>
        /// <param name="path">输入的文件夹地址</param>
        /// <param name="index">读取的文件编号</param>
        /// <param name="imgOut">输出的图像文件</param>
        /// <param name="type">输出的图像类型</param>
        public void ClassifyOfHimage(string path, double index, out HObject imgOut, out double type)
        {

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Image = null, ho_Image1 = null, ho_Image2 = null;
            HObject ho_Image3 = null, ho_ImageEmphasize = null, ho_Rectangle = null;
            HObject ho_Regions = null, ho_ConnectedRegions = null, ho_ConnectedRegions1 = null;
            HObject ho_SelectedRegions4 = null, ho_ImageScaleMax = null;
            HObject ho_SelectedRegions3 = null, ho_SelectedRegions = null;
            HObject ho_SelectedRegions2 = null, ho_SelectedRegions1 = null;

            // Local control variables 

            HTuple hv_WindowHandle = new HTuple(), hv_ImageFiles = null;
            HTuple hv_Index = null, hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Mean2 = new HTuple(), hv_Deviation2 = new HTuple();
            HTuple hv_Number5 = new HTuple(), hv_index1 = new HTuple();
            HTuple hv_Number4 = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Mean1 = new HTuple(), hv_Deviation1 = new HTuple();
            HTuple hv_Sorted = new HTuple(), hv_Mean = new HTuple();
            HTuple hv_Median = new HTuple(), hv_Deviation = new HTuple();
            HTuple hv_Max = new HTuple(), hv_Number2 = new HTuple();
            HTuple hv_Area1 = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Length = new HTuple();
            HTuple hv_Number = new HTuple(), hv_Max1 = new HTuple();
            HTuple hv_Number1 = new HTuple(), hv_Number3 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Image2);
            HOperatorSet.GenEmptyObj(out ho_Image3);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_ImageScaleMax);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);

            try
            {

                string NewstaticPath = path.Replace("\\", "/");
                HOperatorSet.ListFiles(NewstaticPath, ((new HTuple("files")).TupleConcat(
                   "follow_links")).TupleConcat("recursive"), out hv_ImageFiles);
                HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out hv_ImageFiles);



                //a.图像进行预处理并统计产品大概的面积平均值等参数
                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(index));
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                ho_Image1.Dispose(); ho_Image2.Dispose(); ho_Image3.Dispose();
                HOperatorSet.Decompose3(ho_Image, out ho_Image1, out ho_Image2, out ho_Image3
                    );

                HOperatorSet.Emphasize(ho_Image1, out ho_ImageEmphasize, 100, 100, 1);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 0, hv_Height, hv_Width);
                HOperatorSet.Intensity(ho_Rectangle, ho_Image1, out hv_Mean2, out hv_Deviation2);
                ho_Regions.Dispose();
                HOperatorSet.Threshold(ho_ImageEmphasize, out ho_Regions, 0, 50);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                //判断是否为条形状得产品
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_ConnectedRegions1, "area",
                    "and", (hv_Width * hv_Height) * 0.02, hv_Width * hv_Height);
                ho_SelectedRegions4.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions4, "rectangularity",
                    "and", 0.65, 1);
                HOperatorSet.CountObj(ho_SelectedRegions4, out hv_Number5);
                //设个标志位
                hv_index1 = 0;
                if ((int)(new HTuple(hv_Number5.TupleGreaterEqual(1))) != 0)
                {
                    hv_index1 = 1;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "area",
                        "and", 2, 1000000000);
                    ho_ConnectedRegions.Dispose();
                    ho_ConnectedRegions = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "anisometry",
                        "and", 0, 8.5);
                    ho_ConnectedRegions.Dispose();
                    ho_ConnectedRegions = ExpTmpOutVar_0;
                }
                HOperatorSet.CountObj(ho_ConnectedRegions, out hv_Number4);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                if ((int)(new HTuple(hv_Number4.TupleEqual(0))) != 0)
                {
                    ho_ImageScaleMax.Dispose();
                    HOperatorSet.ScaleImageMax(ho_Image1, out ho_ImageScaleMax);
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 0, hv_Height, hv_Width);
                    HOperatorSet.Intensity(ho_Rectangle, ho_ImageScaleMax, out hv_Mean1, out hv_Deviation1);
                    ho_Regions.Dispose();
                    HOperatorSet.Threshold(ho_ImageScaleMax, out ho_Regions, 0, 50);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                }
                else
                {
                    HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                    HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                    HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                    HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                    if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_Deviation.TupleGreaterEqual(
                        5500))).TupleAnd(new HTuple(hv_Deviation.TupleLessEqual(8000))))).TupleAnd(
                        new HTuple(hv_Mean.TupleGreater(100))))).TupleAnd(new HTuple(hv_Mean.TupleLess(
                        750))))).TupleAnd(new HTuple(hv_Deviation2.TupleGreaterEqual(50))))).TupleAnd(
                        new HTuple(hv_Median.TupleLessEqual(45)))) != 0)
                    {
                        ho_ImageScaleMax.Dispose();
                        HOperatorSet.ScaleImageMax(ho_Image1, out ho_ImageScaleMax);
                        ho_Regions.Dispose();
                        HOperatorSet.Threshold(ho_ImageScaleMax, out ho_Regions, 0, 6);
                        ho_ConnectedRegions.Dispose();
                        HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                    }
                    else if ((int)((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_Deviation.TupleGreater(
                        8000))).TupleAnd(new HTuple(hv_Deviation.TupleLessEqual(12000))))).TupleAnd(
                        new HTuple(hv_Mean.TupleGreater(400))))).TupleAnd(new HTuple(hv_Mean.TupleLess(
                        1300))))).TupleAnd(new HTuple(hv_Deviation2.TupleGreaterEqual(50))))).TupleAnd(
                        new HTuple(hv_Median.TupleLessEqual(45)))) != 0)
                    {
                        ho_ImageScaleMax.Dispose();
                        HOperatorSet.ScaleImageMax(ho_Image1, out ho_ImageScaleMax);
                        ho_Regions.Dispose();
                        HOperatorSet.Threshold(ho_ImageScaleMax, out ho_Regions, 0, 6);
                        ho_ConnectedRegions.Dispose();
                        HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                    }
                }
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "area",
                        "and", 1.2, hv_Mean * 1000);
                    ho_ConnectedRegions.Dispose();
                    ho_ConnectedRegions = ExpTmpOutVar_0;
                }
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                HOperatorSet.TupleMax(hv_Sorted, out hv_Max);
                //b.根据标准差进行筛选(多次筛选）
                if ((int)(new HTuple(hv_Deviation.TupleLess(500))) != 0)
                {
                    ho_SelectedRegions3.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions3,
                        "area", "and", 2, 5000000000);
                }
                else
                {
                    ho_SelectedRegions3.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions3,
                        "area", "and", 200, 5000000000);
                }
                HOperatorSet.CountObj(ho_SelectedRegions3, out hv_Number2);
                HOperatorSet.AreaCenter(ho_SelectedRegions3, out hv_Area1, out hv_Row1, out hv_Column1);
                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                HOperatorSet.TupleMax(hv_Sorted, out hv_Max);

                if ((int)((new HTuple(hv_Number2.TupleLessEqual(20))).TupleAnd(new HTuple(hv_Max.TupleGreaterEqual(
                    hv_Mean * 3)))) != 0)
                {
                }
                else if ((int)(new HTuple(hv_Number2.TupleLessEqual(6))) != 0)
                {
                }
                else if ((int)(new HTuple(hv_Max.TupleGreaterEqual(hv_Mean * 2000))) != 0)
                {
                }
                else if ((int)(new HTuple(hv_Deviation.TupleLessEqual(80))) != 0)
                {
                }
                else
                {
                    HOperatorSet.TupleLength(hv_Sorted, out hv_Length);
                    HOperatorSet.TupleRemove(hv_Sorted, hv_Length - 1, out hv_Sorted);
                    HOperatorSet.TupleRemove(hv_Sorted, hv_Length - 2, out hv_Sorted);
                    HOperatorSet.TupleRemove(hv_Sorted, hv_Length - 3, out hv_Sorted);
                    HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                    HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                    HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                    HOperatorSet.TupleMax(hv_Sorted, out hv_Max);
                }

                if ((int)((new HTuple((new HTuple(hv_Deviation.TupleGreater(5))).TupleAnd(
                    new HTuple(hv_Mean.TupleGreaterEqual(6.5))))).TupleAnd(new HTuple(hv_Mean.TupleLessEqual(
                    10)))) != 0)
                {
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 3, hv_Mean * 8);
                    HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                            out hv_Column);
                        HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                        HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                        HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                        HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    }
                }

                if ((int)((new HTuple(hv_Deviation.TupleGreater(20))).TupleAnd(new HTuple(hv_Mean.TupleLessEqual(
                    100)))) != 0)
                {
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 12, hv_Mean * 10);
                    HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                    HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                    HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                    HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    if ((int)(new HTuple(hv_Deviation.TupleLess(30))) != 0)
                    {
                    }
                    else if ((int)(new HTuple(hv_Deviation.TupleGreaterEqual(30))) != 0)
                    {
                        ho_SelectedRegions2.Dispose();
                        HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions2,
                            "area", "and", 150, hv_Mean * 30);
                        HOperatorSet.CountObj(ho_SelectedRegions2, out hv_Number1);
                        if ((int)(new HTuple(hv_Number1.TupleGreaterEqual(1))) != 0)
                        {
                            HOperatorSet.AreaCenter(ho_SelectedRegions2, out hv_Area, out hv_Row,
                                out hv_Column);
                            HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                            HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                            HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                            HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                        }
                        if ((int)(new HTuple(hv_Deviation.TupleGreaterEqual(30))) != 0)
                        {
                            ho_SelectedRegions3.Dispose();
                            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions3,
                                "area", "and", 22, hv_Mean * 5);
                            HOperatorSet.CountObj(ho_SelectedRegions3, out hv_Number);
                            if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                            {
                                HOperatorSet.AreaCenter(ho_SelectedRegions3, out hv_Area, out hv_Row,
                                    out hv_Column);
                                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                                HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                                HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                                HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                            }
                        }
                        if ((int)(new HTuple(hv_Deviation.TupleGreaterEqual(100))) != 0)
                        {
                            ho_SelectedRegions3.Dispose();
                            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions3,
                                "area", "and", 300, hv_Mean * 25);
                            HOperatorSet.CountObj(ho_SelectedRegions3, out hv_Number3);
                            if ((int)(new HTuple(hv_Number3.TupleGreaterEqual(1))) != 0)
                            {
                                HOperatorSet.AreaCenter(ho_SelectedRegions3, out hv_Area, out hv_Row,
                                    out hv_Column);
                                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                                HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                                HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                                HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                            }
                        }
                    }
                }

                if ((int)((new HTuple((new HTuple(hv_Deviation.TupleGreater(100))).TupleAnd(
                    new HTuple(hv_Mean.TupleGreater(100))))).TupleAnd(new HTuple(hv_Mean.TupleLess(
                    108)))) != 0)
                {
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 10, hv_Mean * 4);
                    HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number1);
                    if ((int)(new HTuple(hv_Number1.TupleGreaterEqual(1))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                            out hv_Column);
                        HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                        HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                        HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                        HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    }
                }

                if ((int)((new HTuple((new HTuple(hv_Deviation.TupleGreater(110))).TupleAnd(
                    new HTuple(hv_Mean.TupleGreaterEqual(108))))).TupleAnd(new HTuple(hv_Max.TupleLess(
                    hv_Mean * 10)))) != 0)
                {
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", 150, hv_Mean * 10);
                    HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                            out hv_Column);
                        HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                        HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                        HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                        HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    }
                    if ((int)((new HTuple(hv_Deviation.TupleGreater(110))).TupleAnd(new HTuple(hv_Number.TupleGreaterEqual(
                        10)))) != 0)
                    {
                        ho_SelectedRegions1.Dispose();
                        HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions1,
                            "area", "and", 200, hv_Mean * 5);
                        HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                        if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                        {
                            HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                                out hv_Column);
                            HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                            HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                            HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                            HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                        }
                        if ((int)((new HTuple(hv_Deviation.TupleLess(150))).TupleAnd(new HTuple(hv_Number.TupleGreaterEqual(
                            10)))) != 0)
                        {
                        }
                        else
                        {
                            ho_SelectedRegions2.Dispose();
                            HOperatorSet.SelectShape(ho_SelectedRegions1, out ho_SelectedRegions2,
                                "area", "and", 120, hv_Mean * 2);
                            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                            if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                            {
                                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                                    out hv_Column);
                                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                                HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                                HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                                HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                            }
                        }
                    }
                }
                else if ((int)((new HTuple(hv_Max.TupleGreaterEqual(hv_Mean * 10))).TupleAnd(
                    new HTuple(hv_Mean.TupleGreater(100)))) != 0)
                {
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", hv_Mean * 3, hv_Mean * 2000);
                    HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row,
                            out hv_Column);
                        HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                        HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                        HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                        HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    }
                }

                if ((int)((new HTuple(hv_Median.TupleLessEqual(6))).TupleAnd(new HTuple(hv_Deviation.TupleGreater(
                    15)))) != 0)
                {
                    ho_ImageScaleMax.Dispose();
                    HOperatorSet.ScaleImageMax(ho_Image1, out ho_ImageScaleMax);
                    ho_Regions.Dispose();
                    HOperatorSet.Threshold(ho_ImageScaleMax, out ho_Regions, 0, 120);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "area",
                            "and", 2, 1000000000);
                        ho_ConnectedRegions.Dispose();
                        ho_ConnectedRegions = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "anisometry",
                            "and", 0, 8.5);
                        ho_ConnectedRegions.Dispose();
                        ho_ConnectedRegions = ExpTmpOutVar_0;
                    }
                    HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                    HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                    HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                    HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                    HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    if ((int)(new HTuple(hv_Deviation.TupleGreaterEqual(15))) != 0)
                    {
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.SelectShape(ho_ConnectedRegions, out ExpTmpOutVar_0, "area",
                                "and", 0.1 * hv_Mean, 10 * hv_Mean);
                            ho_ConnectedRegions.Dispose();
                            ho_ConnectedRegions = ExpTmpOutVar_0;
                        }
                        HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row,
                            out hv_Column);
                        HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                        HOperatorSet.TupleMean(hv_Sorted, out hv_Mean);
                        HOperatorSet.TupleMedian(hv_Sorted, out hv_Median);
                        HOperatorSet.TupleDeviation(hv_Sorted, out hv_Deviation);
                        HOperatorSet.TupleMax(hv_Sorted, out hv_Max1);
                    }
                }


                imgOut = ho_Image;
                type = 0;
                //c.根据最终筛选出来的平均值进行分类
                if ((int)((new HTuple(hv_Median.TupleGreater(31))).TupleOr(new HTuple(hv_index1.TupleEqual(
                1)))) != 0)
                {
                    type = 1;
                    // HOperatorSet.DispObj(imgOut, hWindow_img.HalconWindow);
                }
                else if ((int)((new HTuple(hv_Median.TupleLessEqual(31))).TupleAnd(new HTuple(hv_Median.TupleGreater(6)))) != 0)
                {
                    type = 1.2;
                }
                else if ((int)((new HTuple(hv_Median.TupleLessEqual(6))).TupleAnd(new HTuple(hv_Median.TupleGreaterEqual(3)))) != 0)
                {
                    type = 1.5;
                }
                else if ((int)(new HTuple(hv_Median.TupleLess(3))) != 0)
                {
                    type = 1.6;
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                //  ho_Image.Dispose();
                ho_Image1.Dispose();
                ho_Image2.Dispose();
                ho_Image3.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_Rectangle.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_ImageScaleMax.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_SelectedRegions1.Dispose();

                throw HDevExpDefaultException;
            }
            // ho_Image.Dispose();
            ho_Image1.Dispose();
            ho_Image2.Dispose();
            ho_Image3.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_Rectangle.Dispose();
            ho_Regions.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions4.Dispose();
            ho_ImageScaleMax.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_SelectedRegions.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_SelectedRegions1.Dispose();

        }


    }
}
