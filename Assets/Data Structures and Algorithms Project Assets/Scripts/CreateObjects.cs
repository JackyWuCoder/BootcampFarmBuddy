using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CreateObjects : MonoBehaviour
{

    public Vector2 _from;
    public Vector2 _to;
    public int _zValue;
    public GameObject _objectToCreate;


    GameObject _tempObject;

    public void GenerateObjects()
    {
        for (int i = Mathf.RoundToInt(_from.x); i < _to.x; i++)
        {
            for (int j = Mathf.RoundToInt(_from.y); j < _to.y;j++)
            {
                _tempObject = PrefabUtility.InstantiatePrefab(_objectToCreate, transform) as GameObject;
                _tempObject.transform.position = new Vector3(i,j,_zValue);
            }
        }
    }
}
