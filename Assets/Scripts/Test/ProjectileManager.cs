using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProjectileManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    Vector2 prjleSpawnPoint;
    Vector2 shootDir;
    FireBarScript IfReadyToShoot;


    bool loading;
    bool spawnPointAssigned;

    // Use this for initialization
    void Start()
    {


        IfReadyToShoot = FindObjectOfType<FireBarScript>();

        //THIS Probably does something good relating to pixel drad threshold
        //here is the source 
        //http://ilkinulas.github.io/programming/unity/2016/03/18/unity_ui_drag_threshold.html

        //int defaultValue = EventSystem.current.pixelDragThreshold;
        //EventSystem.current.pixelDragThreshold =
        //        Mathf.Max(
        //             defaultValue,
        //             (int)(defaultValue * Screen.dpi / 160f));


    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        
        
        
        checkPositionDirection(eventData);
        
        loading = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        


        if (loading == true)
        {
            checkPositionDirection(eventData);

        }
       
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (loading != false && spawnPointAssigned == true)
        {
            
            Shoot(shootDir);
            loading = false;
            spawnPointAssigned = false;
        }
   
    }

    private void Shoot(Vector2 shootDirection)
    {


        if (IfReadyToShoot.ready)
        {
            GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject("BigLine");

            if (projectile != null)
            {

                projectile.transform.position = prjleSpawnPoint;

                projectile.transform.up = shootDirection;
                projectile.transform.rotation = Quaternion.FromToRotation(new Vector3(0, -1), shootDirection);

                projectile.SetActive(true);

                IfReadyToShoot.shooted = true;

            }
        }
    }


    private void checkPositionDirection(PointerEventData eventData)
    {


        if (eventData.pointerCurrentRaycast.isValid)
        {

            prjleSpawnPoint = eventData.pointerCurrentRaycast.worldPosition;

            shootDir = eventData.delta.normalized;
            spawnPointAssigned = true;


        }
        
    }
}