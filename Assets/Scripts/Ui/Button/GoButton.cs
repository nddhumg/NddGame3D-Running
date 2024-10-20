using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : ButtonBase {
	protected override void OnClick ()
	{
		GameManager.instance.Play ();
	}
}
