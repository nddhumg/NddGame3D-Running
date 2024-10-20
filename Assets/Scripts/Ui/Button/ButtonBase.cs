using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : NddBehaviour {
	[SerializeField] private Button button;

	protected virtual void Start(){
		button.onClick.AddListener(OnClick);
	}

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadButton ();
	}

	protected virtual void LoadButton(){
		if (button != null)
			return;
		button = GetComponent<Button> ();
		DebugLoadComponent ("Button");
				
	}

	protected abstract void OnClick ();
}
