using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OxyPlot;
using OxyPlot.Series;

namespace golceva1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Dictionary<double, double>> results;
        List<List<Dictionary<double, double>>> arrrca;
        private InputParametrs param;
        private void countCell_Scroll(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            param = new InputParametrs();
            param.N = countCell.Value;
            param.cabx = inputComp.Value;
            param.K = speedReact.Value*60;
            param.ca10 = ca10.Value;
            param.cb10 = cb10.Value;
            param.cc10 = cc10.Value;
            param.ca20 = ca20.Value;
            param.cb20 = cb20.Value;
            param.cc20 = cc20.Value;

            param.cabx2 = Convert.ToDouble(cabx.Value);
            param.cbbx = Convert.ToDouble(cbbx.Value);
            param.h = Convert.ToDouble(h.Text);

            param.V = V.Value ;
            param.G = G.Value ;
            param.M = Convert.ToDouble(M.Text) / 10;

            param.V2 = Convert.ToDouble(V2.Text);
            param.G2 = Convert.ToDouble(G2.Text);
            param.K2 = Convert.ToDouble(K2.Text);
            arrrca = new List<List<Dictionary<double, double>>>();

            arrrca =  (new Euler(param)).Calc();
            results = (new Calculate(param)).Get();
            //double cn = (new Calculate(param)).Get();
            //MessageBox.Show("Концентрация:" + cn.ToString()+ " Кмоль /м3");
            CreateGraphs();
            tabControl1.Show();
        }
        
        private void CreateGraphs()
        {
            chart2.Titles.Clear();
            chart2.Series.Clear();
            chart2.Titles.Add("График изменения концентрации");
            System.Windows.Forms.DataVisualization.Charting.Series series1 = chart2.Series.Add("Ca1");
            System.Windows.Forms.DataVisualization.Charting.Series series2 = chart2.Series.Add("Cb1");
            System.Windows.Forms.DataVisualization.Charting.Series series3 = chart2.Series.Add("Cc1");

            chart3.Titles.Clear();
            chart3.Series.Clear();
            chart3.Titles.Add("График изменения концентрации");

            System.Windows.Forms.DataVisualization.Charting.Series series4 = chart3.Series.Add("Ca2");
            System.Windows.Forms.DataVisualization.Charting.Series series5 = chart3.Series.Add("Cb2");
            System.Windows.Forms.DataVisualization.Charting.Series series6 = chart3.Series.Add("Cc2");

            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 2;
            series2.ChartType = SeriesChartType.Line;
            series2.BorderWidth = 2;
            series3.ChartType = SeriesChartType.Line;
            series3.BorderWidth = 2;

            series4.ChartType = SeriesChartType.Line;
            series4.BorderWidth = 2;
            series5.ChartType = SeriesChartType.Line;
            series5.BorderWidth = 2;
            series6.ChartType = SeriesChartType.Line;
            series6.BorderWidth = 2;
            //List<List<Dictionary<int, double>>>


            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("Шаг", typeof(double)));
            dt1.Columns.Add(new DataColumn("Ca1", typeof(double)));
            dt1.Columns.Add(new DataColumn("Cb1", typeof(double)));
            dt1.Columns.Add(new DataColumn("Cc1", typeof(double)));
            dt1.Columns.Add(new DataColumn("Ca2", typeof(double)));
            dt1.Columns.Add(new DataColumn("Cb2", typeof(double)));
            dt1.Columns.Add(new DataColumn("Cc2", typeof(double)));





            Dictionary<double, double> react1 = arrrca[0][0];
            var numberReaction = 1;
            foreach (var result in arrrca[0])
            {
                int i = 0;
                foreach (var k in result)
                {
                    if (numberReaction == 1)
                    {

                        series1.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();
                        dt1.Rows[i]["Шаг"] = k.Key;
                        dt1.Rows[i]["Ca1"] = k.Value;
                    }
                    if (numberReaction == 2)
                    {
                        series2.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();
                        //dt1.Rows[i]["минута"] = k.Key;
                        dt1.Rows[i]["Cb1"] = k.Value;
                    }
                    if (numberReaction == 3)
                    {
                        series3.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();
                        //dt1.Rows[k.Key]["минута"] = k.Key;
                        dt1.Rows[i]["Cc1"] = k.Value;
                    }
                    i++;
                }
                numberReaction++;
            }
            //Вывод С2
            numberReaction = 1;
            foreach (var result in arrrca[1])
            {
                int i = 0;
                foreach (var k in result)
                {
                    if (numberReaction == 1)
                    {

                        series4.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();                        
                        dt1.Rows[i]["Ca2"] = k.Value;
                    }
                    if (numberReaction == 2)
                    {
                        series5.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();
                        dt1.Rows[i]["Cb2"] = k.Value;
                    }
                    if (numberReaction == 3)
                    {
                        series6.Points.AddXY(k.Key, k.Value);
                        dt1.Rows.Add();
                        dt1.Rows[i]["Cc2"] = k.Value;
                    }
                    i++;
                }
                numberReaction++;
            }


            dataGridView2.DataSource = dt1;

            chart2.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart2.ChartAreas[0].AxisX.Interval = param.h < 0.1? param.h*10: param.h;
            //chart1.ChartAreas[0].AxisY.Interval = 0.1;
            chart2.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            //chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Title = "Время, час";
            chart2.ChartAreas[0].AxisY.Title = "Концентрация, Кмоль/м^3";


            chart3.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart3.ChartAreas[0].AxisX.Interval = param.h < 0.1 ? param.h * 10 : param.h;
            //chart1.ChartAreas[0].AxisY.Interval = 0.1;
            chart3.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            //chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Title = "Время, час";
            chart3.ChartAreas[0].AxisY.Title = "Концентрация, Кмоль/м^3";











            Dictionary<int, string> colors = new Dictionary<int, string>() {
                {1, "White"},
                {2, "White"},
                {3, "Red"}
            };
            chart1.Titles.Clear();
            chart1.Series.Clear();
            chart1.Titles.Add("График изменения концентрации");
            var myModel = new PlotModel { Title = "Example 1" };
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("минута", typeof(Int32)));
            dt.Columns.Add(new DataColumn("конц яч 1", typeof(double)));
            dt.Columns.Add(new DataColumn("конц яч 2", typeof(double)));
            dt.Columns.Add(new DataColumn("конц яч 3", typeof(double)));
            for (int i = 0; i < results.Count; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series = this.chart1.Series.Add((i+1).ToString() + " реактор");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3-i;
                for (int j = 0; j < results[i].Count; j++)
                {
                    dt.Rows.Add();
                    series.Points.AddXY(j, results[i][j]);
                    dt.Rows[j]["минута"] = j + 1.05 * i;
                    dt.Rows[j]["конц яч " + (i+1)] = results[i][j];
                }
                                
            }
            dataGridView1.DataSource = dt;
            //dataGridView1.DataSource = results[i].ToArray();
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.Interval = (results[0].Count / 5);
            //chart1.ChartAreas[0].AxisY.Interval = 0.1;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Время, мин";
            chart1.ChartAreas[0].AxisY.Title = "Концентрация, Кмоль/м^3";






        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
