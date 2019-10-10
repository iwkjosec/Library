using System;
using System.Collections.Generic;
using System.Text;

public static class MathEx
{
    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var c = a;
            a = b;
            b = c % b;
        }
        return a;
    }

    public static int ExtGCD(int a, int b, out int x, out int y)
    {
        int u = 1;
        int v = 0;
        x = 1;
        y = 0;
        while (b != 0)
        {
            var t = a / b;
            x -= t * v;
            Swap(ref x, ref v);
            y -= t * u;
            Swap(ref y, ref u);
            a -= t * b;
            Swap(ref a, ref b);
        }
        return a;
    }

    static void Swap<T>(ref T l, ref T r)
    {
        var t = l;
        l = r;
        r = t;
    }
}
