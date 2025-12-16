using UnityEngine;

public class Siedzenie : MonoBehaviour{

    public GameObject gracz;
    public Transform miejsceSiedzenia;
    public bool czySiedzi = false;
    public float dystansOdSiedzenia = 2f;
    public float offsetSiedzenia = 0.5f;
    void Update(){
        if(gracz != null && !czySiedzi){
            float dystans = Vector3.Distance(gracz.transform.position, transform.position);

            if(dystans <= dystansOdSiedzenia && Input.GetKeyDown(KeyCode.F)){
                Siedz();
            }
        }
        else if(czySiedzi && Input.GetKeyDown(KeyCode.F)){
            nieSiedz();
        }
    }

    void Siedz(){
        czySiedzi = true;
        //newplayer => skrypt z ruchem
        NewPlayer ruch = gracz.GetComponent<NewPlayer>();
        //rigidbody
        Rigidbody rb = gracz.GetComponent<Rigidbody>();

        if(ruch != null){
            ruch.enabled = false;
            rb.isKinematic = true;
        }

        gracz.transform.position = miejsceSiedzenia.position + Vector3.up * offsetSiedzenia;
        gracz.transform.rotation = miejsceSiedzenia.rotation;
    }

    void nieSiedz(){
        czySiedzi = false;
        NewPlayer ruch = gracz.GetComponent<NewPlayer>();
        Rigidbody rb = gracz.GetComponent<Rigidbody>();
        if(ruch != null){
            ruch.enabled = true;
            rb.isKinematic = false;
        }

        gracz.transform.position += gracz.transform.forward * 1f;
    }
}
