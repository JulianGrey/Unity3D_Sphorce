using UnityEngine;
using System.Collections;

// This script prevents the object it is attached to from being destroyed upon changing level.

public class MusicControl : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
