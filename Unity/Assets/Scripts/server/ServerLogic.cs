using System.Collections;

public class ServerLogic {
	private TimedEventManager timeManager = new TimedEventManager();
	public Character[] playerChars = new Character[6];
	public Character[] enemyChars = new Character[7];

	public void LogicStep() {
		timeManager.TimeStep();
	}
}
