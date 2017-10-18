using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class SystemFunctions
    {
        private double alfa1; // темпы прироста в популяциях 1 и 2
        private double alfa2;
        private double omega1; // интенсивность межвидовой конкуренции в популяциях 1 и 2
        private double omega2;
        private double betta1; // интенсивность взаимодействий популяций 1 и 2 с популяцией 3
        private double betta2;
        private double gamma1; // ёффективность усвоения популяцией 3 популяций 1 и 2
        private double gamma2;
        public SystemFunctions(double alfa1, double alfa2, double omega1, double omega2, double betta1, double betta2, double gamma1, double gamma2)
        {
            this.alfa1 = alfa1;
            this.alfa2 = alfa2;
            this.omega1 = omega1;
            this.omega2 = omega2;
            this.betta1 = betta1;
            this.betta2 = betta2;
            this.gamma1 = gamma1;
            this.gamma2 = gamma2;
        }

        public double FunctionV1(Point point)
        {
            double res = point.V1 * (alfa1 - point.V1 - omega1 * point.V2 - betta1 * point.V3);
            return res;
        }
        public double FunctionV2(Point point)
        {
            double res = point.V2 * (alfa2 - omega2 * point.V1 - point.V2 - betta2 * point.V3);
            return res;
        }
        public double FunctionV3(Point point)
        {
            double res = point.V3 * (-1 + gamma1 * point.V1 - gamma2 * point.V2); // тут был -1
            return res;
        }
    }
}
