using UnityEngine;
using UnityEngine.UI; 

public class InfiniteRoadMenu : MonoBehaviour
{
    public GameObject[] roadSegments;  
    public float speed = 5f;           
    public float segmentLength = 2.95f;  

    public Transform player;            
    public bool isGameStarted = false;  

    private Vector3[] initialPositions; 

    public GameObject menuPanel;        
    public GameObject startButton;      
    public GameObject optionsButton;    
    public GameObject levelsButton;    

    private void Start()
    {
        initialPositions = new Vector3[roadSegments.Length];
        for (int i = 0; i < roadSegments.Length; i++)
        {
            initialPositions[i] = roadSegments[i].transform.position;
        }
    }

    private void Update()
    {
        if (!isGameStarted) 
        {
            MoveRoadSegments(); 
        }
    }

    private void MoveRoadSegments()
    {
        foreach (GameObject segment in roadSegments)
        {
            segment.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (segment.transform.position.x < initialPositions[0].x - segmentLength)
            {
                float newX = initialPositions[0].x + (segmentLength * (roadSegments.Length - 1)); 

                segment.transform.position = new Vector3(newX, initialPositions[0].y, initialPositions[0].z);
            }
        }
    }

    public void OnStartButtonPressed()
    {
        isGameStarted = true;  
        ResetRoadSegments();    

        startButton.SetActive(false); 
        optionsButton.SetActive(false); 
        levelsButton.SetActive(false); 

        this.enabled = false;  
    }

    private void ResetRoadSegments()
    {
        for (int i = 0; i < roadSegments.Length; i++)
        {
            roadSegments[i].transform.position = initialPositions[i];
        }
    }
}
