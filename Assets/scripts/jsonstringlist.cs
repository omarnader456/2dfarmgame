using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "jsonstringlist")]
public class jsonstringlist : ScriptableObject
{
    public List<string> strings;

    public void setstring(string jsonstring, int idinlist)
    {
        if (strings.Count <= idinlist)
        {
            int count = strings.Count - idinlist + 1;
            while (count > 0)
            {
                strings.Add("");
                count--;
            }
        }
        strings[idinlist] = jsonstring;
    }

    public string getstring(int idinlist)
    {
        if (strings.Count <= idinlist)
        {
            return "";
        }
        return strings[idinlist];
    }
}
