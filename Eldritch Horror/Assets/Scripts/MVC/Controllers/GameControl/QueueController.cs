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

        if (queue.mandatoryCallBacks.Count == 0 && queue.optionalCallBacks.Count == 0) // No Events were added
        {
            App.Model.queueModel.QueueFinished();
            queue.finishedCallBack();
        }
        else
        {
            NextCallBack();
        }
    }

    public void AddCallBack(EventAction e)
    {
        App.Model.queueModel.AddEvent(e);
    }

    // Call this when a callback is complete
    public void NextCallBack()
    {
        EventQueue queue = App.Model.queueModel.queues[App.Model.queueModel.queues.Count - 1];

        if (queue.mandatoryCallBacks.Count == 0 && queue.optionalCallBacks.Count == 0) // queue done
        {
            App.Model.queueModel.QueueFinished();
            queue.finishedCallBack();
        }
        else // queue not done
        {
            if (queue.mandatoryCallBacks.Count > 0) // Need to execute more mandatory events
            {
                EventAction ea = queue.mandatoryCallBacks[0]; // Can add further sorting among mandatory events later
                queue.ExecuteEvent(ea);
                ea.callback();
            }
            else // Only optional events are left
            {
                // Create a list of still valid optional events
                // If there are none: App.Model.queueModel.QueueFinished(); queue.finishedCallBack();
                // If there is at least 1: Add a skip option and send to multiple option menu
                queue.RemoveInvalidOptionalEvents();
                if (queue.optionalCallBacks.Count == 0) // All remaining optional events were invalid
                {
                    App.Model.queueModel.QueueFinished();
                    queue.finishedCallBack();
                }
                else // Choose an optional event to execute
                {
                    List<MultipleOptionMenuObject> options = new List<MultipleOptionMenuObject>();
                    foreach (EventAction ea in queue.optionalCallBacks)
                    {
                        if (ea.asset != null)
                            options.Add(new MultipleOptionMenuObject(MultipleOptionType.AssetEvent, ea.asset));
                        else if (ea.spell != null)
                            options.Add(new MultipleOptionMenuObject(MultipleOptionType.SpellEvent, ea.spell));
                    }
                    options.Add(new MultipleOptionMenuObject(MultipleOptionType.Text, "None"));
                    App.Controller.multipleOptionController.StartMultipleOption(options, "Select an Action", OptionalEventSelected);
                }
            }
        }
    }

    public void OptionalEventSelected(int index)
    {
        EventQueue queue = App.Model.queueModel.queues[App.Model.queueModel.queues.Count - 1];
        if (index == queue.optionalCallBacks.Count) // The "None" option was chosen
        {
            App.Model.queueModel.QueueFinished();
            queue.finishedCallBack();
        }
        else // An optional event was selected
        {
            EventAction ea = queue.optionalCallBacks[index];
            queue.ExecuteEvent(ea);
            ea.callback();
        }
    }
}
