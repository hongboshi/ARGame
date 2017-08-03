using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Net;

class LauguageData
{
    private string filepath;
    private LauguageType _type;
    public LauguageType curLauguage
    {
        get { return _type; }
    }
    private Dictionary<string, string> _dic;
    public LauguageData(string filePath, LauguageType defaultLau = LauguageType.China)
    {
        this.filepath = filePath;
        _type = LauguageType.NULL;
        _dic = new Dictionary<string, string>();
        SetLauguageType(defaultLau);
    }
    public void SetLauguageType(LauguageType type)
    {
        if (_type == type) return;
        _type = type;
        if (System.IO.File.Exists(filepath))
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filepath))
            {             
                string str = sr.ReadToEnd();
                writeToDic(str);
            }
        }
        else
        {
            TextAsset ta = Resources.Load<TextAsset>("lauguageConfig");
            if (ta != null)
                writeToDic(ta.text);
            else
                Debug.Log("not found");
        }
    }
    public string GetText(string key)
    {
        string value = "";
        bool flag = _dic.TryGetValue(key, out value);
        if (!flag)
        {
            value = key;
         //   Debug.LogWarning(string.Format("the lauguage key {0} is not exist", key));
            _dic.Add(key, value);
        }
        if (value == " ")
            value = key;
        return value;
    }
    public void SetBeizu(string key, string value)
    {
        if (_dic.ContainsKey(key))
            _dic[key] = value;
    }
    void writeToDic(string str)
    {
        TextData data = XMLHelp.XmlHelper.XmlDeserialize<TextData>(str,System.Text.Encoding.UTF8);
        if (data == null || data.data.Count == 0 || data.data[0].values.Count < (int)_type + 1)
        {
            Debug.LogError("fileConfig is not correct");
            return;
        }
        _dic.Clear();
        data.data.ForEach((x) =>
        {
            _dic.Add(x.key, x.values[(int)_type]);
        });
    }
}
[XmlRoot("TextData")]
public class TextData
{
    public List<TextItem> data;

    public TextData()
    {
        data = new List<TextItem>();
    }
    public void AddTextItem(TextItem item)
    {
        if (data.Exists((x) => x.key == item.key))
        {
            Debug.LogError(string.Format("textItem key = {0} is repeated", item.key));
        }
        else
            data.Add(item);
    }
}
public class TextItem
{
    [XmlElement("Key")]
    public string key;
    
    public List<string> values;

    public TextItem()
    {
        key = "";
        values = new List<string>();
    }
    public TextItem(string key, string[] value)
    {
        this.key = key;
        this.values = new List<string>();
        for (int i = 0; i < value.Length; i++)
        {
            values.Add(value[i]);
        }
    }
}

