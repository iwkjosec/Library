using System;
using System.Collections.Generic;
using System.Text;

public class RollingHash
{
    ulong[] hash;
    ulong[] pow;
    public int Length => hash.Length - 1;
    const ulong @base = 131;
    const ulong mod = (1UL << 61) - 1;

    public RollingHash(string s)
    {
        hash = new ulong[s.Length + 1];
        pow = new ulong[s.Length + 1];
        pow[0] = 1;
        for (int i = 1; i < pow.Length; i++)
        {
            hash[i] = (Mul(hash[i - 1], @base) + s[i - 1]) % mod;
            pow[i] = Mul(pow[i - 1], @base) % mod;
        }
    }

    public ulong this[int i]
    {
        get
        {
            return hash[i];
        }
    }

    public ulong Hash(int l, int r)
    {
        var a = hash[r] + mod * 3;
        var b = Mul(hash[l], pow[r - l]);
        return (a - b) % mod;
    }

    public static ulong ComputeHash(string s)
    {
        var h = 0UL;
        foreach (var i in s)
        {
            h = (Mul(h, @base) + i) % mod;
        }
        return h;
    }

    static ulong Mul(ulong a, ulong b)
    {
        const ulong MASK30 = (1UL << 30) - 1;
        const ulong MASK31 = (1UL << 31) - 1;
        var ah = a >> 31;
        var al = a & MASK31;
        var bh = b >> 31;
        var bl = b & MASK31;
        var m = al * bh + ah * bl;
        var mh = m >> 30;
        var ml = m & MASK30;
        return (ah * bh * 2 + mh + (ml << 31) + al * bl);
    }
}
