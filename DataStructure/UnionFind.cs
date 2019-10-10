using System;
using System.Collections.Generic;
using System.Text;

public class UnionFind
{
    int[] parent;
    int count;
    public int Count => count;

    public UnionFind(int length)
    {
        parent = new int[length];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = -1;
        }
        count = length;
    }

    public int Root(int v) => parent[v] < 0 ? v : parent[v] = Root(parent[v]);

    public bool Same(int l, int r) => Root(l) == Root(r);

    void Swap<T>(ref T l, ref T r)
    {
        var t = l;
        l = r;
        r = t;
    }

    public bool Unite(int l, int r)
    {
        l = Root(l);
        r = Root(r);
        if (l == r) return false;
        count--;
        if (parent[l] > parent[r]) Swap(ref l, ref r);
        parent[l] += parent[r];
        parent[r] = l;
        return true;
    }

    public int Size(int v) => -parent[Root(v)];
}
