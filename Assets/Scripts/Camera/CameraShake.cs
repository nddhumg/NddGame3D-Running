﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class CameraShake : Singleton<CameraShake>
{
	float _shakeDuration = 0f;
	private float shakeDuration = 0f;
	Vector3 originalPos;
	[SerializeField] float shakeAmount = 0.7f, decreaseFactor = 1.0f;

	public float ShakeDuration{
		get{
			return shakeDuration;
		}

	}

	void OnEnable()
	{
		originalPos = transform.localPosition;
	}

	void Update()
	{
		if (_shakeDuration > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;	
			_shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			_shakeDuration = 0f;
			transform.localPosition = originalPos;
		}
	}

	public void Shake(float timeDuration){
		_shakeDuration = timeDuration;
	}

	public void Shake(float timeDuration, float shakeAmount){
		_shakeDuration = timeDuration;
		this.shakeAmount = shakeAmount;
	}

}