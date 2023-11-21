using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMember : MonoBehaviour {
	public Transform tf;
	public virtual void OnInit(){}
	public virtual void OnDespawn(){}
}