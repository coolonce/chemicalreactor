using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golceva1
{
    class Euler
    {
        private InputParametrs param;
        private Calculate calc;
        public Euler(InputParametrs param)
        {
            this.param = param;
            calc = new Calculate(param);
        }


        //private double CalcCa(double t, double ca)
        //{
        //    return (1 / calc.CalcTay())*(param.cabx - ca)- param.K*ca * param.cb;
        //}

        //private double CalcCc(double ccIn, double cc, double k)
        //{
        //    return (1 / calc.CalcTay()) * (ccIn - cc) - 2*k * param.ca * param.cb; 
        //}
        //private double Func(double ccInp, double y)
        //{
        //    return 0.0;
        //}Dictionary<int, Dictionary<double[], double[]>>


        public double CalcTay()
        {
            return (param.V2 / param.G2);
        }
        public double CalcTime()
        {
            return param.M * CalcTay();
        }
        public List<List<Dictionary<int, double>>> Calc()
        {
            Dictionary<int, Dictionary<double[], double[]>> ca = new Dictionary<int, Dictionary<double[], double[]>>();
            
            double maxTime = Math.Round(CalcTime());
            double tay = CalcTay();

            Dictionary<int, double> CA1 = new Dictionary<int, double>();
            CA1.Add(0,param.ca10);
            Dictionary<int, double> CB1 = new Dictionary<int, double>();
            CB1.Add(0, param.cb10);
            Dictionary<int, double> CC1 = new Dictionary<int, double>();
            CC1.Add(0, param.cc10);
            for (int i = 1; i < maxTime; i++)
            {
                CA1[i] = Math.Round(CA1[i - 1] + param.h * (((param.cabx2 - CA1[i - 1])/ tay) - (param.K2 * CA1[i - 1] * param.cb10)) ,3);
                CB1[i] = Math.Round(CB1[i - 1] + param.h * (((param.cbbx - CB1[i - 1]) / tay) - (param.K2 * CA1[i - 1] * param.cb10)), 3);
                CC1[i] = Math.Round(CC1[i - 1] + param.h * (((0 - CC1[i - 1]) / tay) + (2 * param.K2 * CA1[i - 1] * param.cb10)), 3);
            }

            List<Dictionary<int, double>> react1 = new List<Dictionary<int, double>>();
            react1.Add(CA1);
            react1.Add(CB1);
            react1.Add(CC1);

            maxTime = Convert.ToInt32(maxTime);
            Dictionary<int, double> CA2 = new Dictionary<int, double>();
            CA2.Add(0, CA1[CA1.Count-1]);
            Dictionary<int, double> CB2 = new Dictionary<int, double>();
            CB2.Add(0, CB1[CB1.Count - 1]);
            Dictionary<int, double> CC2 = new Dictionary<int, double>();
            CC2.Add(0, CC1[CC1.Count - 1]);

            for (int i = 1; i < maxTime; i++)
            {
                CA2[i] = CA2[i - 1] + param.h * (tay * (CA1[i - 1]- CA2[i-1]) - param.K * CB2[0]);
                CB2[i] = CB2[i - 1] + param.h * (tay * (CB1[i - 1]- CB2[0]) - param.K * CB2[0]);
                CC2[i] = CC2[i - 1] + param.h * (tay * (0 - CC2[i - 1]) + 2 * param.K * CB2[0]);
            }

            List<Dictionary<int, double>> react2 = new List<Dictionary<int, double>>();
            react2.Add(CA2);
            react2.Add(CB2);
            react2.Add(CC2);

            List<List<Dictionary<int, double>>> result = new List<List<Dictionary<int, double>>>();

            result.Add(react1);
            result.Add(react2);
            return result;
        }
    }
}
