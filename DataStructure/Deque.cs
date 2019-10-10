using System;
using System.Collections.Generic;
using System.Text;

public class Deque<T>
{
    T[] buffer;
    int front;
    int back;
    int mask;

    public Deque() : this(1024)
    {

    }

    public Deque(int capacity)
    {
        var c = 1024;
        while (c < capacity) c <<= 1;
        buffer = new T[c];
        mask = c - 1;
    }

    public int Count
    {
        get
        {
            int count = back - front;
            if (count < 0) count += buffer.Length;
            return count;
        }
    }

    public void Extend()
    {
        var nb = new T[buffer.Length * 2];
        for (int i = 0; i < buffer.Length - 1; i++)
        {
            nb[i] = buffer[(front + i) & mask];
        }
        front = 0;
        back = buffer.Length - 1;
        buffer = nb;
        mask = mask << 1 | 1;
    }

    public void PushFront(T value)
    {
        if (Count == buffer.Length - 1) Extend();
        front = (front - 1) & mask;
        buffer[front] = value;
    }

    public void PushBack(T value)
    {
        if (Count == buffer.Length - 1) Extend();
        var e = back;
        back = (back + 1) & mask;
        buffer[e] = value;
    }

    public T PopFront()
    {
        var f = front;
        front = (front + 1) & mask;
        return buffer[f & mask];
    }

    public T PopBack()
    {
        back = (back - 1) & mask;
        return buffer[back];
    }

    public T PeekFront()
    {
        return buffer[front];
    }

    public T PeekBack()
    {
        return buffer[(back - 1) & mask];
    }

    public void Clear()
    {
        front = 0;
        back = 0;
    }
}
