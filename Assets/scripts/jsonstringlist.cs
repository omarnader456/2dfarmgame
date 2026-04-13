using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "jsonstringlist")]
public class jsonstringlist : ScriptableObject
{
    public List<string> strings = new List<string>();

    public void setstring(string jsonstring, int idinlist)
    {
        if (strings.Count <= idinlist)
        {
            int itemsToAdd = (idinlist + 1) - strings.Count;
            
            for (int i = 0; i < itemsToAdd; i++)
            {
                strings.Add("");
            }
        }
        
        strings[idinlist] = jsonstring;
    }

    public string getstring(int idinlist)
    {
        if (strings == null || strings.Count <= idinlist)
        {
            return "";
        }
        return strings[idinlist];
    }
}