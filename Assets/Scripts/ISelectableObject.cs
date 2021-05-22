using UnityEngine;

public interface ISelectableObject
{
    void SetParent(Transform parent, bool isKeepParentTransform = false);

    Vector3 GetDir();

    Vector3 GetRotDir();
}

public interface ILivingObject
{
    void TakeDamage();
}
