using System;
using System.Collections.Generic;
using System.Text;

public class SegmentTree<T>
{
    T[] buffer;
    int N;
    public int Length => N;
    T TI;
    Func<T, T, T> F;

    public SegmentTree(int length, Func<T, T, T> f, T ti)
    {
        N = length;
        buffer = new T[N * 2];
        for (int i = 0; i < buffer.Length; i++) buffer[i] = ti;
        F = f;
        TI = ti;
    }

    public SegmentTree(T[] init, Func<T, T, T> f, T ti)
    {
        N = init.Length;
        buffer = new T[N * 2];
        Array.Copy(init, 0, buffer, N, init.Length);
        for (int i = N - 1; i > 0; i--)
        {
            buffer[i] = f(buffer[i * 2], buffer[i * 2 + 1]);
        }
        F = f;
        TI = ti;
    }

    public void Update(int k, T v)
    {
        k += N;
        buffer[k] = v;
        while (k > 1)
        {
            buffer[k >> 1] = F(buffer[k], buffer[k ^ 1]);
            k >>= 1;
        }
    }

    public T this[int k]
    {
        set
        {
            Update(k, value);
        }
        get
        {
            return buffer[k + N];
        }
    }

    public T Query(int l, int r)
    {
        var lv = TI;
        var rv = TI;
        l += N;
        r += N;
        while (l < r)
        {
            if ((l & 1) == 1)
            {
                lv = F(lv, buffer[l++]);
            }
            if ((r & 1) == 1)
            {
                rv = F(buffer[--r], rv);
            }
            l >>= 1;
            r >>= 1;
        }
        return F(lv, rv);
    }
}
