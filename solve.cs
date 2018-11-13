using System;

public class Solve
{
    public static double solve1(double a, double b)
    {
        checked{
            if (a == 0) return 0;
            return -b / a;
        }
    }

    public static double solve2(double a, double b, double c)
    {
        checked
        {
            if (a == 0) return solve1(b, c);
            double delta = b * b - 4 * a * c;
            if (delta < 0) throw new Exception("delta = " + delta.ToString() + " < 0");
            return (-b + Math.Sqrt(delta)) / (2 * a);
        }
    }

    public static double solve3(double a, double b, double c, double d)
    {
        checked
        {
            if (a == 0) return solve2(b, c, d);
            if (a != 1) return solve3(1, b / a, c / a, d / a);
            double p;
            double q;
            if (b != 0)
            {
                double del = b / 3;
                p = b * b / 3 - 2 * b * b / 3 + c;
                q = -b * b * b / 27 + b * b * b / 9 - c * b / 3 + d;
                return solve3(1, 0, p, q) - b / 3;
            }
            p = c;
            q = d;
            double delta = p * p * p / 27 + q * q / 4;
            //Console.WriteLine(p + " " + q + " " + delta);
            if (delta >= 0)
            {
                double m1 = (-q / 2 + Math.Sqrt(delta));
                double m2 = (-q / 2 - Math.Sqrt(delta));
                double x = 0;
                if (m1 >= 0) x += Math.Pow(m1, 1.0 / 3);
                else x -= Math.Pow(-m1, 1.0 / 3);
                if (m2 >= 0) x += Math.Pow(m2, 1.0 / 3);
                else x -= Math.Pow(-m2, 1.0 / 3);
                //Console.WriteLine(m1 + " " + m2);
                return x;
            }
            else
            {
                double r3 = Math.Sqrt(-p / 3);
                double theta = Math.Acos(-q / (2 * r3 * r3 * r3)) / 3;
                double x1 = 2 * r3 * Math.Cos(theta);
                double x2 = 2 * r3 * Math.Cos(theta + Math.PI / 3);
                double x3 = 2 * r3 * Math.Cos(theta + 2 * Math.PI / 3);
                double x = x1;
                if (x2 >= x) x = x2;
                if (x3 >= x) x = x3;
                //Console.WriteLine(r3 + " " + theta + " " + x1 + " " + x2 + " " + x3);
                return x;
            }
        }
    }

    static void Main()
    {
        Random r = new Random();
        double a, b, c, d;
        int i = 0;
        int j = 0;
        double error = 0;
        while (true)
        {
            a = r.Next() + r.NextDouble() ;
            b = r.Next() + r.NextDouble();
            c = r.Next() + r.NextDouble();
            d = r.Next() + r.NextDouble();
            //Console.WriteLine(a + " " + b + " " + c + " " + d);
            double x = Solve.solve3(a, b, c, d);
            //Console.WriteLine(x);
            double result = a * x * x * x + b * x * x + c * x + d;
            //Console.WriteLine(result);
            if (Math.Abs(result)/a < 1E-6) j++;
            //if (Math.Abs(result) > 1) //Console.ReadKey();
            else if (error <= Math.Abs(result)/a) error = Math.Abs(result);
            ////Console.ReadKey();
            i++;
            //Console.WriteLine(j + "/" + i);
            if (i >= 10000) break;
            ////Console.Clear();
            //System.Threading.Thread.Sleep(10);
        }
        //Console.WriteLine(error);
        //Console.ReadKey();
    }

}