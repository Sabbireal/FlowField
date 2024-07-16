using UnityEngine;

public class FlowFieldController : MonoBehaviour
{
	private void Update()                                                                
	{                                                                                    
		if (Input.GetMouseButtonDown(0))                                                 
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
			mousePos.y = 0;                                                              
			FlowField.Instance.SetDestination(mousePos);                                                       
		}     
		
		if (Input.GetKeyDown(KeyCode.D))                                                 
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
			mousePos.y = 0;                                                              
			FlowField.Instance.SetDestination(mousePos, false);                                                       
		}    
	}                                                                                                                                                                      
}