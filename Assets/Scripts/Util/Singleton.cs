using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }
	protected virtual void Awake() {
		if (instance != null) {
			Debug.LogWarning ("Dont single",gameObject);
		}
		instance = this as T;

	}

    protected virtual void OnApplicationQuit()
    {
        instance = null;
        Destroy(gameObject);
    }
}

public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        };
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}
