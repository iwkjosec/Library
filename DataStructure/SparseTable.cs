using System;
using System.Collections.Generic;
using System.Text;

public class SparseTable<T>
{
    T[][] buffer;
    Func<T, T, T> F;
    int[] log2;

    public SparseTable(T[] init, Func<T, T, T> f)
    {
        F = f;

        var n = init.Length;

        log2 = new int[n + 1];
        log2[0] = -1;
        for (int i = 2; i < log2.Length; i++)
        {
            log2[i] = log2[i >> 1] + 1;
        }

        buffer = new T[log2[n] + 1][];
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = new T[n - (1 << i) + 1];
        }

        Array.Copy(init, 0, buffer[0], 0, init.Length);
        for (int i = 1, p = 1; i < buffer.Length; i++, p <<= 1)
        {
            for (int j = 0; j < buffer[i].Length; j++)
            {
                buffer[i][j] = F(buffer[i - 1][j], buffer[i - 1][j + p]);
            }
        }
    }

    public T Query(int l, int r)
    {
        var h = log2[r - l];
        return F(buffer[h][l], buffer[h][r - (1 << h)]);
    }
}
