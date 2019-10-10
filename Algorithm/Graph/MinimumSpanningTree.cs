using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public static class MinimumSpanningTree
{
    public static long Kruskal(Edge[] edges, int v)
    {
        var total = 0L;
        var uf = new UnionFind(v);
        foreach (var e in edges.OrderBy(x => x.cost))
        {
            if (uf.Unite(e.src, e.to))
            {
                total += e.cost;
            }
        }
        return total;
    }
}
