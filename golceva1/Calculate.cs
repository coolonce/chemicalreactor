using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golceva1
{
    class Calculate
    {
        private InputParametrs param;
        public Calculate(InputParametrs param)
        {
            this.param = param;
        }

        public List<Dictionary<double, double>> Get()
        {
            //double cn = 0;
            List<Dictionary<double, double>> cnn = new List<Dictionary<double, double>>();
            double tay = CalcTay();
            double time = CalcTime();
            for (int i = 1; i < param.N+1; i++)
            {
                cnn.Add(Calc(i));
                //cn += (1 / Fuct(i - 1)) * Math.Pow((CalcTime() / CalcTay()), i - 1);
                //cnn.Add(cn * (Math.Pow(Math.E, (CalcTime() / CalcTay())) * param.cabx));
            }
            return cnn;
            //return  cn * (Math.Pow(Math.E, (CalcTime() / CalcTay())) * param.cabx);
            /*
             * смотрим сколько реакторов. 
             * вызываем кальк в него передаем номер ячейки
             * получаем по времени значения для данного реактора в массиве время:значение 
             */
        }

        public Dictionary<double, double> Calc(int numberCell)
        {
            Dictionary<double, double> result = new Dictionary<double, double>();
            double maxTime = Math.Round(400+ CalcTime() * 60);
            double tay = CalcTay()*60;
            for (int t = 0; t < maxTime; t++)
            {
                double cn = 0;
                double tmp = 0;
                for (int i = 1; i < numberCell + 1; i++)
                {
                    var two = Math.Pow(Convert.ToDouble(t) / tay, Convert.ToDouble(i - 1));
                    tmp += two / Fuct(i - 1);
                }
                cn = param.cabx - (tmp * Math.Pow(Math.E, Convert.ToDouble(-t / tay)) * param.cabx);
                result[t] = Math.Round(cn,5);
            }
            for (double t = maxTime; t < maxTime + maxTime; t++)
            {
                double cn = 0;
                double tmp = 0;
                for (int i = 1; i < numberCell + 1; i++)
                {
                    var two = Math.Pow(Convert.ToDouble(t-maxTime) / tay, Convert.ToDouble(i - 1));
                    tmp +=  two/ Fuct(i - 1);
                }
                var cc = result[maxTime - 1];
                cn = (tmp * Math.Pow(Math.E, Convert.ToDouble(-(t - maxTime)) / tay)) * cc;
                //cn = cn > cc ? 1 : cn;
                result[t] = Math.Round(cn,5);
            }
            return result;
        }



        private double CalcTay()
        {
            return param.V/param.G;
        }
        private double CalcTime()
        {
            return param.M * CalcTay();
        }

        private int Fuct(int x)
        {
            if (x == 0)
            {
                return 1;
            }else if (x < 0)
            {
                return 0;
            }
            else
            {
                return x * Fuct(x - 1);
            }
        }
    }
}
