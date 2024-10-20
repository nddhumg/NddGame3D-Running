using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBase : NddBehaviour {
	[SerializeField] protected Text text;

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadText ();
	}

	protected virtual void LoadText(){
		if (text != null)
			return;
		text = GetComponent<Text> ();
		DebugLoadComponent ("Text");
	}
}
