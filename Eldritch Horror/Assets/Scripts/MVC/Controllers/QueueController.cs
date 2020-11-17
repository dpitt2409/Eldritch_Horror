using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MVC
{
    public void CreateCallBackQueue(QueueCallBack finished)
    {
        EventQueue queue = new EventQueue(finished);
        App.Model.queueModel.NewQueue(queue);
    }

    public void StartCallBackQueue()
    {
        EventQueue queue = App.Model.queueModel.queues[App.Model.queueModel.queues.Count-1];

        if (queue.queueCallBacks.Count == 0)
        {
            App.Model.queueModel.QueueFinished();
            queue.finishedCallBack();
        }
        else
        {
            // rearrange the list of callbacks
            NextCallBack();
        }
    }

    public void AddCallBack(EventAction e)
    {
        EventQueue queue = App.Model.queueModel.queues[App.Model.queueModel.queues.Count - 1];
        queue.queueCallBacks.Add(e);
    }

    // Call this when a callback is complete
    public void NextCallBack()
    {
        App.Model.queueModel.IncrementCallBackIndex();
        EventQueue queue = App.Model.queueModel.queues[App.Model.queueModel.queues.Count - 1];

        if (queue.callBackIndex == queue.queueCallBacks.Count) // queue done
        {
            App.Model.queueModel.QueueFinished();
            queue.finishedCallBack();
        }
        else // queue not done
        {
            queue.queueCallBacks[queue.callBackIndex].callback();
        }
    }
}
