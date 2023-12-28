using UnityEngine;
using Articy.Unity;
public class MyArticyPluginComponent : MonoBehaviour
{
 public ArticyRef myFirstArticyModel;
 void Start()
 {
 var techName = myFirstArticyModel.GetObject().TechnicalName;
 Debug.Log(techName);
 }
}
