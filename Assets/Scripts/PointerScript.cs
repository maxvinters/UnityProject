using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PointerScript : MonoBehaviour {

    [SerializeField]
    LineRenderer LR;
    bool Started;
    List<string>Patterns = new List<string>();
    SavedPatt Saving = new SavedPatt();
    List<string> Dots = new List<string>();
    List<Vector3> DotsPos = new List<Vector3>();
    string path = "c:\\save.json";

    void Start()
    {
        string json = File.ReadAllText(path);
        Saving = JsonUtility.FromJson<SavedPatt>(json);
        Patterns = Saving.Patterns;
    }

    void Awake()
    {
        Started = false;
        Dots.Clear();
        DotsPos.Clear();
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (Patterns.Contains(List2string(Dots)) && Started)
                print("Yes");
            else
                print("No");
        }
    }

   void FixedUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        if (Input.GetMouseButtonDown(0))
        {
            LR.positionCount = 0;
            Started = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Dot") && Input.GetMouseButton(0))
        {
            if (Started)
            {
                if (!Dots.Contains(col.gameObject.name))
                {  
                    Dots.Add(col.gameObject.name);
                    DotsPos.Add(col.transform.position);
                    LR.positionCount = DotsPos.Count;
                    LR.SetPositions(DotsPos.ToArray());
                    /////////
                    /*Saving.Patterns.Add(Dots2string(Dots));
                    Saving.Patterns.Add(Dots2string(Dots));
                    string json = JsonUtility.ToJson(Saving);
                    StreamWriter sw = File.CreateText(path);
                    sw.Close();
                    File.WriteAllText(path, json);*/
                    
                }
            }
            else
            {
                Dots.Clear();
                DotsPos.Clear();
                Dots.Add(col.gameObject.name);
                DotsPos.Add(col.transform.position);
                Started = true;
            }
        }
    }

    [System.Serializable]
    public class SavedPatt
    {
        public List<string> Patterns = new List<string>();
    }

    string List2string(List<string> Dots)
    {
        string ans =string.Empty;
        var tmp = Dots.ToArray();
        foreach (string s in tmp)
            ans += s + ";";

        return ans;
    }

    List<string> string2List(string Dots)
    {
       
        List<string> ans = new List<string>();
        var tmp=Dots.Split(';');
        foreach (string s in tmp)
            ans.Add(s);
        return ans;
    }


}
