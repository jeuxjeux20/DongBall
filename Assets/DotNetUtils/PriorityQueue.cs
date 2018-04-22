using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PriorityQueue<T> : List<T> where T : IPriorityHolder
{
    public T ConsumeNextPriority()
    {
        var value = this.OrderByDescending(k => k.Priority).First();
        Remove(value);
        return value;
    }
    public T GetNextPriority()
    {
        var value = this.OrderByDescending(k => k.Priority).First();
        return value;
    }
}

