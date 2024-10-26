using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotsPrefeb;
    [SerializeField] float dotspacing;
    [SerializeField] [Range(0.01f, 0.3f) ]float dotminscale;
    [SerializeField] [Range(0.3f, 1f) ]float dotmaxscale;
    

    Transform[] dotList;

    Vector2 pos;

    float timeStamp;

    void Start(){
        PrepareDots();
        Hide();
    }

    void PrepareDots(){
        dotList = new Transform[dotsNumber];
        dotsPrefeb.transform.localScale = Vector3.one * dotmaxscale;

        float scale = dotmaxscale;
        float scaleFactor = scale/dotsNumber;

        for(int i=0; i < dotsNumber; i++){
            dotList [i] = Instantiate (dotsPrefeb, null).transform;
			dotList [i].parent = dotsParent.transform;

            dotList[i].localScale = Vector3.one * scale;
            if(scale>dotminscale){
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots(Vector3 ballpos, Vector2 forceApplied){
        timeStamp = dotspacing;
        for(int i=0; i< dotsNumber; i++){
            pos.x = (ballpos.x + forceApplied.x * timeStamp);
            pos.y = (ballpos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp);

            dotList[i].position = pos;
            timeStamp += dotspacing;
        }
    }

    public void Show(){
        dotsParent.SetActive (true);
    }

    public void Hide(){
        dotsParent.SetActive (false);

    }   
}
