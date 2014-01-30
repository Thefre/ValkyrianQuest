using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TimedEventManager {
	public static List<TimeEventItem> actions = new List<TimeEventItem>();
	
	public delegate void TimedAction(object sender, string action);
	public static event TimedAction OnTimedAction;

	private DateTime pauseTime;
	private bool isPaused;
	
	// Update is called once per frame
	public void TimeStep () {
		if (!isPaused) {
			if(actions.Count > 0) {
				if (DateTime.UtcNow >= actions[0].time) {
					if(OnTimedAction != null) //Trigger OnTimedAction
						OnTimedAction(actions[0].sender ,actions[0].action);
					actions.RemoveAt(0);
				}
			}
		}
	}
	
	public static void AddAction(object sender, string act, TimeSpan timePass) {
		TimeEventItem newAction = new TimeEventItem();
		newAction.action = act;
		newAction.sender = sender;
		newAction.time = DateTime.UtcNow.Add(timePass);
		actions.Add(newAction);
		actions = actions.OrderBy(x => x.time).ToList();
	}

	public void Pause() {
		if(!isPaused) {
			isPaused = true;
			pauseTime = DateTime.UtcNow;
		}
	}
	public void UnPause() {
		if(isPaused) {
			isPaused = false;
			foreach (TimeEventItem act in actions) {
				act.time.Add(DateTime.UtcNow - pauseTime);
			}
		}
	}
}
