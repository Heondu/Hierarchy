using UnityEngine;

public interface ISelectableObject
{
    void SetParent(Transform parent);

    Vector3 GetDir();

    Vector3 GetRotDir();
}
