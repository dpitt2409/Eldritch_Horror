using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void QueueCallBack();

public class QueueModel : MVC
{
    public List<EventQueue> queues;

    // Start is called before the first frame update
    void Start()
    {
        queues = new List<EventQueue>();
    }

    public void AddEvent(EventAction e)
    {
        EventQueue queue = queues[queues.Count - 1];
        queue.AddEvent(e);
    }

    public void NewQueue(EventQueue queue)
    {
        queues.Add(queue);
    }

    public void QueueFinished()
    {
        queues.RemoveAt(queues.Count-1);
    }
}
