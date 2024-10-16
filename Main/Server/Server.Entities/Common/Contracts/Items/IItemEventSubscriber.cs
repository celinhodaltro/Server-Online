﻿namespace Server.Entities.Common.Contracts.Items;

public interface IItemEventSubscriber
{
    public void Subscribe(IItem item);
    public void Unsubscribe(IItem item);
}