using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pc{
	public class InputManager : Singleton<InputManager> {
		public bool left;
		public bool right;
		public bool up;
		public bool down;

		void Update(){
			left = Input.GetKeyDown (KeyCode.A);
			right = Input.GetKeyDown (KeyCode.D);
			up = Input.GetKeyDown (KeyCode.W);
			down = Input.GetKeyDown (KeyCode.S);
		}
	}
}
