using System;
using System.Collections.Generic;
using System.Text;

public class SegmentTree<T>
{
    T[] buffer;
    Func<T, T, T> F;
    T TI;
    int N;
    public int Length => N;

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
            buffer[i] = f(buffer[i * 2 + 0], buffer[i * 2 + 1]);
        }
        F = f;
        TI = ti;
    }

    public void Update(int i, T v)
    {
        i += N;
        buffer[i] = v;
        while ((i >>= 1) != 0)
        {
            buffer[i] = F(buffer[i * 2 + 0], buffer[i * 2 + 1]);
        }
    }

    public T Query(int l, int r)
    {
        var lv = TI;
        var rv = TI;
        for (l += N, r += N; l < r; l >>= 1, r >>= 1)
        {
            if ((l & 1) == 1) lv = F(lv, buffer[l++]);
            if ((r & 1) == 1) rv = F(buffer[--r], rv);
        }
        return F(lv, rv);
    }

    public T this[int i]
    {
        set { Update(i, value); }
        get { return buffer[i + N]; }
    }
}
