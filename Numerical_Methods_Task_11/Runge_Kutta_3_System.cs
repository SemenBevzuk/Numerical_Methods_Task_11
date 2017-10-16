using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class Runge_Kutta_3_System
    {
        private Func<Point, double> f1; // первая функция системы
        private Func<Point, double> f2; // вторая функция системы
        private Func<Point, double> f3; // третья функция системы
        private double h; // шаг
        private double eps; // контроль шага
        private Point currentPoint; // текущая точка 
        private double borderAccuracy; // точность выхода на границу

        private bool flagStepControl; // включить или отключить контроль шага
        private int maxSteps;         // ограничение по числу шагов
        private readonly List<Point> Points = new List<Point>(); // список точек для графика
        //private readonly List<MetodInfo> listIfMetodInfo = new List<MetodInfo>(); // список для таблиц
        private int countPlusH = 0; // счётчик увеличения шага
        private int countMinusH = 0; // счётчик уменьшения шага
        private int steps = 0; // число шагов в данный момент
        public Runge_Kutta_3_System(double _x0, double _u_0_1, double _u_0_2, double _u_0_3, double _h, double _eps,
            double _borderAccuracy, int _maxSteps, bool _flagIsHControl, 
            Func<Point, double> _u1, Func<Point, double> _u2, Func<Point, double> _u3)
        {
            currentPoint = new Point(_x0, _u_0_1, _u_0_2, _u_0_3);
            h = _h;
            eps = _eps;
            borderAccuracy = _borderAccuracy;
            maxSteps = _maxSteps;
            flagStepControl = _flagIsHControl;
            f1 = _u1;
            f2 = _u2;
            f3 = _u3;
            Points.Add(currentPoint);
            //MetodInfos.Add(new MetodInfo(steps, _h, _x0, 0, 0, 0, 0, 0));
        }

        public void Run()
        {
            while (!NeedStop())
            {
                var oldH = h;

                var newPoint = MakeStep(currentPoint, h);

                var halfPoint = GetHalfPoint(currentPoint, h);

                var s = Math.Abs(GetS(halfPoint, newPoint));

                var e = Math.Abs(Math.Pow(2.0, 3.0) * s);

                var uCorr = GetVCorr(newPoint, s);

                if (flagStepControl)
                {
                    if (s > eps )
                    {
                        h = h/2.0;
                        countMinusH++;
                        continue;
                    }

                    if (s < eps/(Math.Pow(2.0, 4.0)))
                    {
                        h = h*2;
                        countPlusH++;
                    }
                }
                currentPoint = newPoint;
                Points.Add(newPoint);
                steps++;
                //MetodInfos.Add(new MetodInfo(steps, oldH, fCurrentPoint.X, s, e, fCurrentPoint.V, countMinusH, countPlusH));
            }
        }

        private Point GetVCorr(Point nextPoint, double s)
        {
            return nextPoint + (Math.Pow(2.0, 3.0) * s);
        }
        private double GetS(Point _halfPoint, Point _newPoint)
        {
            Point a = (_halfPoint - _newPoint) / (Math.Pow(2.0, 3.0) - 1.0);
            double res = Math.Max(a.V1, a.V2);
            res = Math.Max(res, a.V3);
            return res;
        }
        private Point GetHalfPoint(Point _currentPoint, double h)
        {
            var p1 = MakeStep(_currentPoint, h / 2.0);
            return MakeStep(p1, h / 2.0);
        }

        private Point MakeStep(Point curPoint, double h)
        {
            var x = GetNextX(curPoint.X, h);
            Point v = GetNextV(curPoint, h);
            return new Point(x, v.V1, v.V2, v.V3);
        }
        private double GetNextX(double x, double h)
        {
            return x + h;
        }

        private Point GetNextV(Point point, double h)
        {
            Point k1 = GetK1(point);
            Point k2 = GetK2(point, h, k1);
            Point k3 = GetK3(point, h, k1, k2);
            Point res = point + (h/6.0)*(k1 + 4.0 * k2 + k3);
            return res;
        }

        private Point GetK1(Point point)
        {
            return new Point(0, f1(point), f2(point), f3(point));
        }
        private Point GetK2(Point point, double h, Point k1)
        {
            return new Point(0, f1(point+(h/2.0)*k1), f2(point+(h/2.0)*k1), f3(point+(h/2.0)*k1));
        }
        private Point GetK3(Point point, double h, Point k1, Point k2)
        {
            return new Point(0, f1(point+(h)*(-k1+2.0*k2)), f2(point+(h)*(-k1+2.0*k2)), f3(point+(h)*(-k1+2.0*k2)));
        }
        private bool NeedStop()
        {
            if (steps > maxSteps)
            {
                return true;
            }
            if (currentPoint.V1 <= borderAccuracy || currentPoint.V2 <= borderAccuracy || currentPoint.V3 <= borderAccuracy)
            {
                return true;
            }
            return false;
        }

        //public List<MetodInfo> GetMetodInfos()
        //{
        //    return listIfMetodInfo;
        //}
        public List<Point> GetPoints()
        {
            return Points;
        }
        public double GetResultTime()
        {
            return currentPoint.X;
        }
    }
}
