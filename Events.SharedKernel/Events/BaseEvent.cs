﻿using Events.SharedKernel.Command;
using Events.SharedKernel.Messages;

namespace Events.SharedKernel.Events;

public abstract class BaseEvent : Message
{
    public int Version { get; set; }
    
    public string Type { get; set; }


    protected BaseEvent(string type)
    {
        this.Type = type;
    }
}