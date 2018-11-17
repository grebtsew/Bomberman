using UnityEngine;
using System.Collections;

public class CustomizablePowerUp : MonoBehaviour {
	
	#region Settings
	public string powerUpName;
	public bool isTakeable = false;
	public AudioClip pickUpSound;

	public GameObject externHull;
	private GameObject _externHull;
	public float externHullRotSpeed = 0f;
	public bool externHullIsReverse = false;
	public Material externHullMaterial;

	public GameObject internHull;
	private GameObject _internHull;
	public float internHullRotSpeed = 0f;
	public bool internHullIsReverse = false;
	public Material internHullMaterial;

	public GameObject effect;
	private GameObject _effect;
	public float effectRotSpeed = 0f;
	public bool effectIsReverse = false;

	public GameObject item;
	private GameObject _item;
	public float itemRotSpeed = 0f;
	public bool itemIsReverse = false;
	public Material itemMaterial;

	private GameObject _light;
	public Color lightColor = Color.white;
	public float lightIntensity = 3.0f;
	public float lightRange = 4.0f;
	#endregion
	
	void Start () {
		if(externHull != null)
		{
			this._externHull = (GameObject)GameObject.Instantiate(externHull, transform.position, transform.rotation);
			this._externHull.transform.parent = transform;
			this._externHull.name = "Extern Hull";
			this._externHull.GetComponent<Renderer>().sharedMaterial = this.externHullMaterial;
			if(externHullRotSpeed > 0.0f)
			{
				this._externHull.AddComponent(typeof(PowerUpRotation));
				PowerUpRotation rotScript = (PowerUpRotation)this._externHull.GetComponent(typeof(PowerUpRotation));
				rotScript.SetRotationSpeed(this.externHullRotSpeed);
				rotScript.SetReverse(this.externHullIsReverse);
			}
			if(this.isTakeable)
				this._externHull.AddComponent(typeof(TakeablePowerUp));
		}
		if(internHull != null)
		{
			this._internHull = (GameObject)GameObject.Instantiate(internHull, transform.position, transform.rotation);
			this._internHull.transform.parent = transform;
			this._internHull.name = "Intern Hull";
			this._internHull.GetComponent<Renderer>().sharedMaterial = this.internHullMaterial;
			if(internHullRotSpeed > 0.0f)
			{
				this._internHull.AddComponent(typeof(PowerUpRotation));
				PowerUpRotation rotScript = (PowerUpRotation)this._internHull.GetComponent(typeof(PowerUpRotation));
				rotScript.SetRotationSpeed(this.internHullRotSpeed);
				rotScript.SetReverse(this.internHullIsReverse);
			}
		}
		if(item != null)
		{
			this._item = (GameObject)GameObject.Instantiate(item, transform.position, Quaternion.identity);
			this._item.transform.rotation = this._item.transform.rotation * Quaternion.Euler(90,0,0);
			this._item.transform.parent = transform;
			this._item.name = "Item";
			this._item.GetComponent<Renderer>().sharedMaterial = this.itemMaterial;
			if(itemRotSpeed > 0.0f)
			{
				this._item.AddComponent(typeof(PowerUpRotation));
				PowerUpRotation rotScript = (PowerUpRotation)this._item.GetComponent(typeof(PowerUpRotation));
				rotScript.SetRotationSpeed(this.itemRotSpeed);
				rotScript.SetReverse(this.itemIsReverse);
			}
		}
		if(effect != null)
		{
			this._effect = (GameObject)GameObject.Instantiate(effect, transform.position, transform.rotation);
			/*if(item != null)
			{
				this._effect.transform.parent = _item.transform;
			}
			else 
			{*/
				this._effect.transform.parent = transform;
			//}
			this._effect.name = "Effect";
			if(effectRotSpeed > 0.0f)
			{
				this._effect.AddComponent(typeof(PowerUpRotation));
				PowerUpRotation rotScript = (PowerUpRotation)this._effect.GetComponent(typeof(PowerUpRotation));
				rotScript.SetRotationSpeed(this.effectRotSpeed);
				rotScript.SetReverse(this.effectIsReverse);
			}
		}

		this._light = new GameObject("Light");
		//this._light = (GameObject)GameObject.Instantiate(this._light);
		this._light.transform.parent = transform;
		this._light.transform.position = transform.position;
		//this._internHull.GetComponent<Renderer>().sharedMaterial = this.internHullMaterial;
		Light tmp = (Light)this._light.AddComponent(typeof(Light));
		tmp.color = this.lightColor;
		tmp.intensity = this.lightIntensity;
		tmp.range = this.lightRange;
		tmp.type = LightType.Point;
		tmp.shadows = LightShadows.Hard;
	}
	
	
	void Update () {
	
	}
}
