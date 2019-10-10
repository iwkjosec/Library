﻿using System;
using System.Collections.Generic;
using System.Text;

class ModBinom
{
    long[] fac;
    long[] finv;
    int mod;

    public ModBinom(int N, int p = (int)1e9 + 7)//N < p  O(N)
    {
        N++;

        fac = new long[N];
        long[] inv = new long[N];
        finv = new long[N];

        fac[0] = fac[1] = 1;
        inv[1] = 1;
        finv[0] = finv[1] = 1;

        for (int i = 2; i < fac.Length; i++)
        {
            fac[i] = fac[i - 1] * i % p;
            inv[i] = p - inv[p % i] * (p / i) % p;
            finv[i] = finv[i - 1] * inv[i] % p;
        }

        mod = p;
    }

    public long Binom(int n, int k)
    {
        return fac[n] * finv[k] % mod * finv[n - k] % mod;
    }
}
