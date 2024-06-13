using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public delegate void AssetLoadGameObjDel(); 

public class UIManager : Singletone<UIManager>
{
    private object LoadAsset ;

    public void LoadPrefabAsset<T>(string _pathName, AssetLoadGameObjDel _loadDel = null)
    {
        LoadAsset = null;
        try
        {
            LoadAssest<T>(_pathName, _loadDel);
        }
        catch (Exception e) { };
    }

    public void LoadAssest<T>(string _pathName, AssetLoadGameObjDel _loadDel = null)
    {
        
        Addressables.LoadAssetAsync<T>(_pathName).Completed +=
            (AsyncOperationHandle<T> _obj) =>
            {
                Debug.Log("<color=cyan>=====AsyncOperationStatus : </color>" + _obj.Status);
                if(_obj.Status == AsyncOperationStatus.Succeeded)
                {
                    LoadAsset = _obj.Result;

                    if (_loadDel != null)
                        _loadDel();
                }
            };
    }

    public object GetLoadAsset()
    {
        return LoadAsset;
    }
}
