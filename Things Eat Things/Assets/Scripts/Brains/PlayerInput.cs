using UnityEngine;
using System.Collections;

public class PlayerInput : Brain
{

    public float power;

    Vector3 targetCamOffset;


    void Start()
    {
        Globals.Init();

    }

    public override void GetInput()
    {
        Globals.gCamera.GetComponent<GameCam>().LookAtThis(transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Globals.gCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                switch (hit.collider.gameObject.layer)
                {
                    case Globals.kGroundLayer:
                        ClickedGround(hit.point);
                        break;

                    case Globals.kCreaturesLayer:
                        ClickedCreature(hit.collider.gameObject);
                        break;

                    case Globals.kSceneryLayer:
                        ClickedScenery(hit.collider.gameObject);
                        break;

                    default:
                        Debug.Log("Clicked something unexpected...");
                        break;
                }
            }
        }



    }




    void ClickedCreature(GameObject creature)
    {
        //todo: Interact with the creature if the creature is able to be interacted by you. If not, just move towards it
        Debug.Log("Click " + creature.name);

    }


    void ClickedScenery(GameObject scenery)
    {
        //todo: ??
        Debug.Log("Click Scenery");
    }

    void ClickedGround(Vector3 position)
    {
        if (isValidTerrainPoint(position))
        {
            Debug.Log("Click Ground");
            Creature.Locomotor.TargetPosition = position;
        }
    }

    bool isValidTerrainPoint(Vector3 zPosition)
    {
        //todo: differentiate whether you have clicked a valid position
        return true;
    }
}
