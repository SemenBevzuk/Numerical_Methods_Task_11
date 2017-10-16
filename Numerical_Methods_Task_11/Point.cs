using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class Point
    {
        public double X { get; private set; }
        public double V1 { get; private set; }
        public double V2 { get; private set; }
        public double V3 { get; private set; }

        public Point(double x, double v1, double v2, double v3)
        {
            X = x;
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public static Point operator + (Point obj1, Point obj2)
        {
            Point res = new Point(
                0,
                obj1.V1 + obj2.V1,
                obj1.V2 + obj2.V2,
                obj1.V3 + obj2.V3
                );
            return res;
        }

        public static Point operator + (Point obj1, double obj2)
        {
            Point res = new Point(
                obj1.X,
                obj1.V1 + obj2,
                obj1.V2 + obj2,
                obj1.V3 + obj2
                );
            return res;
        }
        public static Point operator - (Point obj1, Point obj2)
        {
            Point res = new Point(
                0,
                obj1.V1 - obj2.V1,
                obj1.V2 - obj2.V2,
                obj1.V3 - obj2.V3
                );
            return res;
        }
        public static Point operator * (Point obj1, Point obj2)
        {
            Point res = new Point(
                0,
                obj1.V1 * obj2.V1,
                obj1.V2 * obj2.V2,
                obj1.V3 * obj2.V3
                );
            return res;
        }
        public static Point operator * (double obj1, Point obj2)
        {
            Point res = new Point(
                obj2.X,
                obj1 * obj2.V1,
                obj1 * obj2.V2,
                obj1 * obj2.V3
                );
            return res;
        }

        public static Point operator / (double obj1, Point obj2)
        {
            Point res = new Point(
                obj2.X,
                obj2.V1/obj1,
                obj2.V2/obj1,
                obj2.V3/obj1
                );
            return res;
        }
        public static Point operator / (Point obj2, double obj1)
        {
            Point res = new Point(
                obj2.X,
                obj2.V1/obj1,
                obj2.V2/obj1,
                obj2.V3/obj1
                );
            return res;
        }

        public static Point operator -(Point obj1)
        {
            Point res = new Point(
                obj1.X,
                obj1.V1 * (-1.0),
                obj1.V2 * (-1.0),
                obj1.V3 * (-1.0)
                );
            return res;
        }
    }
}
