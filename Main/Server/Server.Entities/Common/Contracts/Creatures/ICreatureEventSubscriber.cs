﻿namespace Server.Entities.Common.Contracts.Creatures;

public interface ICreatureEventSubscriber
{
    public void Subscribe(ICreature creature);
    public void Unsubscribe(ICreature creature);
}