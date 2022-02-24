//Shady
using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;
	
	void FixedUpdate ()
    {
    	if(Mat && SpotLight)
    	{
    		Mat.SetVector("MyLightPosition",  SpotLight.transform.position);
        	Mat.SetVector("MyLightDirection", -SpotLight.transform.forward );
        	Mat.SetFloat ("MyLightAngle", SpotLight.spotAngle);
    	}
        
    }
}