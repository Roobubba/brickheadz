using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

    void OnMouseDown()
    {
        Destroy(this.transform.root.gameObject);
    }

}
