using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class MetodInfo
    {
        public int Iteration { get; private set; }
        public double H { get; private set; }
        public double X { get; private set; }
        public double V1 { get; private set; }
        public double V2 { get; private set; }
        public double V3 { get; private set; }
        public double VHalf { get; private set; }
        public double DeltaV { get; private set; }
        public double S { get; private set; }
        public double e { get; private set; }
        public double VCorr { get; private set; }
        public int CountPlusH { get; private set; }
        public int CountMinusH { get; private set; }
        public MetodInfo(int iteration, double h, double x, double v1, double v2, double v3, 
            double vHalf, double deltaV, double s, double e, double vCorr, int countPlusH, int countMinusH)
        {
            Iteration = iteration;
            H = h;
            X = x;
            V1 = v1;
            V2 = v2;
            V3 = v3;
            VHalf = vHalf;
            DeltaV = deltaV;
            S = s;
            this.e = e;
            VCorr = vCorr;
            CountPlusH = countPlusH;
            CountMinusH = countMinusH;
        }
    }
}
