using UnityEngine.UI;
using UnityEngine;

public class SliderBase : NddBehaviour {
	[SerializeField] protected Slider slider;

	protected override void LoadComponent ()
	{
		base.LoadComponent ();
		LoadSlider ();
	}

	protected virtual void LoadSlider(){
		if (slider != null)
			return;
		slider = gameObject.GetComponent<Slider> ();
		DebugLoadComponent ("Slider");
	}
}
