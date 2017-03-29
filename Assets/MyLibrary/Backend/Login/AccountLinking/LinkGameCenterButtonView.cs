using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LinkGameCenterButtonView : MonoBehaviour {

    [Inject]
    LinkGameCenterButton Test;

    public void OnClick() {
        Test.OnClick();
    }
}
