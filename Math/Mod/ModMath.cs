using System;
using System.Collections.Generic;
using System.Text;

public static class ModMath
{
    public static long ModPow(long x, long n, int m)
    {
        var res = 1L;
        while (n != 0)
        {
            if ((n & 1) == 1) res = res * x % m;
            x = x * x % m;
            n >>= 1;
        }
        return res;
    }

    public static int ModInv(int a, int m)
    {
        var b = m;
        int u = 1;
        int v = 0;
        while (b != 0)
        {
            var t = a / b;
            a -= t * b;
            Swap(ref a, ref b);
            u -= t * v;
            Swap(ref u, ref v);
        }
        u %= m;
        if (u < 0) u += m;
        return u;
    }

    static void Swap<T>(ref T l, ref T r)
    {
        var t = l;
        l = r;
        r = t;
    }
}
