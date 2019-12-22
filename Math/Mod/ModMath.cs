using System;
using System.Collections.Generic;
using System.Text;

public static class ModMath
{
    public static long ModPow(long x, long n, long m)
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

    public static long ModLog(long x, long b, long p)
    {
        var s = (int)Math.Sqrt(p) + 1;
        var m = new Dictionary<long, int>();
        var t = 1L;
        for (int i = 0; i < s; i++)
        {
            if (!m.ContainsKey(t)) m[t] = i;
            t = t * b % p;
        }
        var r = ModInv(t, p);
        var k = x;
        for (int i = 0; i < s; i++)
        {
            if (m.ContainsKey(k)) return i * s + m[k];
            k = k * r % p;
        }
        return -1;
    }

    public static long ModInv(long a, long m)
    {
        var b = m;
        long u = 1;
        long v = 0;
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
