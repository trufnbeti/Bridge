using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMember : MonoBehaviour {
	public PoolType poolType;
	public Transform tf;
	public virtual void OnInit(){}
	public virtual void OnDespawn(){}
}