using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
	private PowerUpManager() {}

	private Queue<CustomizablePowerUp> powerUps;
	private Queue<CustomizablePowerUp> powerUpsLogs;
	private ushort powerUpLogLimit = 3;
	
	public int Count {
		get {
			return powerUps.Count;
		}
	}
	
	void Awake() {
		this.powerUps = new Queue<CustomizablePowerUp>();
		this.powerUpsLogs = new Queue<CustomizablePowerUp>();
	}	

	public void Add(CustomizablePowerUp powerUp)
	{
		this.powerUps.Enqueue(powerUp);
		this.powerUpsLogs.Enqueue(powerUp);
		while (this.powerUpsLogs.Count > this.powerUpLogLimit && this.powerUpsLogs.Dequeue()) ;
	}

	private string RGBToHex(Color color)
	{
		return string.Format("#{0}{1}{2}", 
                     ((int)(color.r * 255)).ToString("X2"), 
                     ((int)(color.g * 255)).ToString("X2"), 
                     ((int)(color.b * 255)).ToString("X2"));
	}

	void OnGUI() {
		foreach(CustomizablePowerUp pu in powerUpsLogs) {
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			GUILayout.Label("You picked up <color=" + RGBToHex(pu.lightColor) + ">" + pu.powerUpName + "</color>");
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		GUI.Label(new Rect(Screen.width - 180, 0, 180, 20), "PowerUp count: <color=yellow>" + this.powerUps.Count + "</color>");
	}
}

