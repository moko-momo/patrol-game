using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Publish
{
    void notify(ActorState state, int pos, GameObject actor);
 	void add(Observer observer);
   	void delete(Observer observer);
  
}

public interface Observer
{
    void notified(ActorState state, int pos, GameObject actor);

}

public enum ActorState { ENTER_AREA, DEATH }

public class Publisher : Publish {

    private delegate void ActionUpdate(ActorState state, int pos, GameObject actor);
    private ActionUpdate updatelist;

    private static Publish _instance;
    public static Publish getInstance()
    {
        if (_instance == null) _instance = new Publisher();
        return _instance;
    }

    public void notify(ActorState state, int pos, GameObject actor)
    {
        if (updatelist != null) updatelist(state, pos, actor);

    }

    public void add(Observer observer)
    {
        updatelist += observer.notified;
    }

    public void delete(Observer observer)
    {
        updatelist -= observer.notified;
    }
}
