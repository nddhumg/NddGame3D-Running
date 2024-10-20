using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : TextBase {
	private string textCoin = "Coin: ";

	void Update(){
		text.text = textCoin + GameController.instance.Coin;
	}


}
