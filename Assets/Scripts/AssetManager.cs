using UnityEngine;
using System.Collections;

public class AssetManager : MonoBehaviour {

    public static AssetManager instance = null;

    public PumaController pumaMaleYoung;
    public PumaController pumaMaleAdult;
    public PumaController pumaMaleOld;
    public PumaController pumaFemaleYoung;
    public PumaController pumaFemaleAdult;
    public PumaController pumaFemaleOld;

    public static PumaController puma;
    private static bool pumaUpdated = false;
    private static int selectedPuma = -1;


    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
        puma = pumaMaleYoung;
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pumaUpdated)
        {
            switch (selectedPuma)
            {
                case 0:
                    puma = pumaMaleYoung;
                    break;
                case 1:
                    puma = pumaFemaleYoung;
                    break;
                case 2:
                    puma = pumaMaleAdult;
                    break;
                case 3:
                    puma = pumaFemaleAdult;
                    break;
                case 4:
                    puma = pumaMaleOld;
                    break;
                case 5:
                    puma = pumaFemaleOld;
                    break;
            }

            pumaUpdated = false;
        }
    }

    public static void SetPuma(int num)
    {
        selectedPuma = num;
        pumaUpdated = true;
    }
}
