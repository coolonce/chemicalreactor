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
        private void countCell_Scroll(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputParametrs param = new InputParametrs();
            param.N = countCell.Value;
            param.cabx = inputComp.Value;
            param.K = speedReact.Value*60;
            param.ca = ca.Value;
            param.cb = cb.Value;
            param.cc = cc.Value;
            param.V = V.Value ;
            param.G = G.Value ;
            param.M = Convert.ToDouble(M.Text);

            results = (new Calculate(param)).Get();
            //double cn = (new Calculate(param)).Get();
            //MessageBox.Show("Концентрация:" + cn.ToString()+ " Кмоль /м3");
            CreateGraphs();
            tabControl1.Show();
        }
        
        private void CreateGraphs()
        {
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
            maxTime.Text = (results[0].Count /60).ToString();
        }
        
    }
}
