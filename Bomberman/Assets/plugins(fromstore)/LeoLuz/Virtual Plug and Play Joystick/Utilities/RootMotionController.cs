using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;


public class RootMotionController : MonoBehaviour {
	[Tooltip("Toogle this to enable or disable root movement")]
	//[ExtendLabelAttribute(138f,32f)]
    public bool EnableRootMovement;
    public bool defaultEnableRootMovement;
    public configuration Configuration;
	public status Status;
    public UnityEngine.Events.UnityEvent _ev;
    public void teste(GameObject obj)
    {

    }
    public void teste2(GameObject obj, float ints)
    {

    }
    [System.Serializable]
    public class configuration {
    	[ExtendLabelAttribute(64f,32f)]
		public Animator animator;
		[ExtendLabelAttribute(122f,32f)]
		public Transform transformAffected;
	    public enum TranslationType {  transform, rigidbody }
	    [ExtendLabelAttribute(107f,32f)]
	    public TranslationType translationType = TranslationType.rigidbody;
	    [ReadOnly]
	    public Rigidbody2D rigidbody;
        public bool relativeByHorizontalFacing;
    }
    [System.Serializable]
    public class status {
		[ReadOnly]
	    public Vector3 DeltaPosition;
	    [ReadOnly]
	    public Quaternion DeltaRotation;
	    [ReadOnly]
	    public Vector3 TotalDisplacement;
	    [HideInInspector]
	    public bool _animatorSimulatedBkp;
    }
    void Start()
    {
        defaultEnableRootMovement = EnableRootMovement;
    }
  //  void OnDrawGizmosSelected() {
		//if(Configuration.animator==null)
  //      {
		//	Configuration. animator = GetComponent<Animator>();
  //      }
		//if (Configuration.transformAffected == null)
  //      {
		//	Configuration.transformAffected = transform;
		//	Configuration.rigidbody = Configuration.transformAffected.GetComponent<Rigidbody2D>();
  //      }
  //      else
  //      {
		//	Configuration.rigidbody = Configuration.transformAffected.GetComponent<Rigidbody2D>();
  //      }

  //      Renderer rend = GetComponent<SpriteRenderer>();
  //      if (rend != null)
  //          rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
  //  }
    void OnAnimatorMove()
    {
        if (EnableRootMovement)
        {
            Vector3 delta = Configuration.animator.deltaPosition;
            if (Configuration.transformAffected.transform.localScale.x < 0f)
            {
                delta.x = -delta.x;
            }
            Status.DeltaPosition += delta;
            Status.DeltaRotation *= Configuration.animator.deltaRotation;
            Status.TotalDisplacement += delta;
        }
    }
    void FixedUpdate()    {
		if (Configuration.translationType ==configuration.TranslationType.rigidbody)
        {
            if (EnableRootMovement)
            {
                Configuration.rigidbody.simulated = true;
                Configuration.rigidbody.MovePosition(Configuration.rigidbody.position + (Configuration.relativeByHorizontalFacing && Configuration.animator.transform.localScale.x<0f? new Vector2(-Status.DeltaPosition.x, Status.DeltaPosition.y):(Vector2)Status.DeltaPosition));
                //float convertTo2D = (Status.DeltaRotation * Vector3.forward).z;
                // Rb.MoveRotation((Rb.rotation* DeltaRotation)*Vector3.right);
                Status.DeltaPosition = Vector3.zero;
                Status.DeltaRotation = Quaternion.identity;
            }
        }
    }
    void Update()    {
		if (Configuration.translationType == configuration.TranslationType.transform)
        {
            if (EnableRootMovement)
            {
                if (Configuration.rigidbody != null)
                {
                    Configuration.rigidbody.simulated = false;
                }
                Configuration.transformAffected.transform.position += (Vector3)Status.DeltaPosition;
                //float convertTo2D = (Status.DeltaRotation * Vector3.forward).z;
                // Rb.MoveRotation((Rb.rotation* DeltaRotation)*Vector3.right);
                Status.DeltaPosition = Vector3.zero;
                Status.DeltaRotation = Quaternion.identity;
            }
        }
    }
    public void enableRootMovement()    {
        EnableRootMovement = true;
		Status.TotalDisplacement = Vector3.zero;
		//if (Configuration.rigidbody != null)
  //      {
		//	Status._animatorSimulatedBkp = Configuration.rigidbody.simulated;
  //      }    
        
  //      ResetVelocity();
    }
    public void DisableRootMovement()    {
        EnableRootMovement = false;
		//if (Configuration.rigidbody != null)
  //      {
		//	Configuration.rigidbody.simulated =Status. _animatorSimulatedBkp;
  //      }
    }

    public void ResetVelocity()    {
		if(Configuration.rigidbody!=null)
			Configuration.rigidbody.velocity = Vector2.zero;
    }
}
