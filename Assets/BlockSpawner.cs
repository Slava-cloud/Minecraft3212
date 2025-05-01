using UnityEngine;
using UnityEngine.Events;


public class BlockSpawner : MonoBehaviour
{
    public float rayDistance = 10f;
    public GameObject blockPrefab;




    public KeyCode destroyBlock;
    public KeyCode spawnBlock;
    


    public UnityEvent onBlockSpawn;
    public UnityEvent onBlockDestroy;




    void ReadInput_Update()
    {
        if (Input.GetKeyDown(spawnBlock))onBlockSpawn.Invoke();
        if (Input.GetKeyDown(destroyBlock))onBlockDestroy.Invoke();
        
    }
    
    public void DestroyBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (
         Physics.Raycast(ray,
         out hit,
         rayDistance)
         )
        {
            if (hit.collider.CompareTag("Block"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        
    }


    public void SpawnBlock()
    {
        Vector3 hitNormal;
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray,out hit,rayDistance))
        {
            if (hit.collider.CompareTag("Block"))
            {
                hitNormal = hit.normal;
                Vector3 spawnPosition = hit.collider.transform.position + hitNormal;
                GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);      
            }   
        }
    }
    
 





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput_Update();
        
    }
}
