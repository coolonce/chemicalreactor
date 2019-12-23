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
            return (param.M / 1) * CalcTay();
        }
        public List<List<Dictionary<double, double>>> Calc()
        {
            Dictionary<int, Dictionary<double[], double[]>> ca = new Dictionary<int, Dictionary<double[], double[]>>();
            
            double maxTime = Math.Round(CalcTime());
            double tay = CalcTay();

            Dictionary<double, double> CA1 = new Dictionary<double, double>();
            CA1.Add(0,param.ca10);
            Dictionary<double, double> CB1 = new Dictionary<double, double>();
            CB1.Add(0, param.cb10);
            Dictionary<double, double> CC1 = new Dictionary<double, double>();
            CC1.Add(0, param.cc10);
            double curStep = 0;
            for (double hstep = param.h; hstep <= maxTime; hstep += param.h)
            {
                curStep = Math.Round(hstep, 5);
                double prevStep = Math.Round(hstep - param.h, 5);
                CA1[curStep] = Math.Round(CA1[prevStep] + param.h * (((param.cabx2 - CA1[prevStep])/ tay) - (param.K2 * CA1[prevStep] * CB1[prevStep])) ,3);
                CB1[curStep] = Math.Round(CB1[prevStep] + param.h * (((param.cbbx - CB1[prevStep]) / tay) - (param.K2 * CA1[prevStep] * CB1[prevStep])), 3);
                CC1[curStep] = Math.Round(CC1[prevStep] + param.h * (((0 - CC1[prevStep]) / tay) + (2 * param.K2 * CA1[prevStep] * CB1[prevStep])), 3);
                
            }
           
            List<Dictionary<double, double>> react1 = new List<Dictionary<double, double>>();
            react1.Add(CA1);
            react1.Add(CB1);
            react1.Add(CC1);

            maxTime = Math.Round(CalcTime());
            Dictionary<double, double> CA2 = new Dictionary<double, double>();
            CA2.Add(0, CA1[curStep] + param.ca20);
            Dictionary<double, double> CB2 = new Dictionary<double, double>();
            CB2.Add(0, CB1[curStep] + param.cb20);
            Dictionary<double, double> CC2 = new Dictionary<double, double>();
            CC2.Add(0, CC1[curStep]+ param.cc20);
            curStep = 0;
            for (double hstep = param.h; hstep <= maxTime; hstep += param.h)
            {
                curStep = Math.Round(hstep, 5);
                double prevStep = Math.Round(hstep - param.h, 5);
                CA2[curStep] = CA2[prevStep] + param.h * (((CA1[prevStep] - CA2[prevStep])/1) - param.K2 * CA2[prevStep]  * CA2[prevStep]);
                CB2[curStep] = CB2[prevStep] + param.h * (((CB1[prevStep] - CB2[prevStep])/1) - param.K2 * CA2[prevStep] * CB2[prevStep]);
                CC2[curStep] = CC2[prevStep] + param.h * (((CC1[prevStep] - CC2[prevStep]) /1) + 2 * param.K2 * CA2[prevStep] * CB2[prevStep]);
            }

            List<Dictionary<double, double>> react2 = new List<Dictionary<double, double>>();
            react2.Add(CA2);
            react2.Add(CB2);
            react2.Add(CC2);

            List<List<Dictionary<double, double>>> result = new List<List<Dictionary<double, double>>>();

            result.Add(react1);
            result.Add(react2);
            return result;
        }
    }
}
