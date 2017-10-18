using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class TaskInfo
    {
        public int Number { get; private set; }
        public double Alfa1 { get; private set; }
        public double Alfa2 { get; private set; }
        public double Omega1 { get; private set; }
        public double Omega2 { get; private set; }
        public double Betta1 { get; private set; }
        public double Betta2 { get; private set; }
        public double Gamma1 { get; private set; }
        public double Gamma2 { get; private set; }
        public double X0 { get; private set; }
        public double U_1_0 { get; private set; }
        public double U_2_0 { get; private set; }
        public double U_3_0 { get; private set; }
        public double h0 { get; private set; }
        public double e { get; private set; }
        public double Border_eps { get; private set; }
        public int Max_iteration { get; private set; }

        public TaskInfo(int number, 
            double alfa1, double alfa2, double omega1, double omega2, 
            double betta1, double betta2, double gamma1, double gamma2, 
            double x0, double u10, double u20, double u30, 
            double h0, double e, double borderEps, int maxIteration)
        {
            Number = number;
            Alfa1 = alfa1;
            Alfa2 = alfa2;
            Omega1 = omega1;
            Omega2 = omega2;
            Betta1 = betta1;
            Betta2 = betta2;
            Gamma1 = gamma1;
            Gamma2 = gamma2;
            X0 = x0;
            U_1_0 = u10;
            U_2_0 = u20;
            U_3_0 = u30;
            this.h0 = h0;
            this.e = e;
            Border_eps = borderEps;
            Max_iteration = maxIteration;
        }
    }
}
