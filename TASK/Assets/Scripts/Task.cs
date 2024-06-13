using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [Header("[Tr]")]
    public RectTransform ContentTr;

    [Header("[Btn]")]
    public Button BtnStart;

    public Toggle verticaltoggle;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        BtnStart.onClick.AddListener(OnClickStart);
    }

    #region OnClickEvent
    public void OnClickStart()
    {
        UIManager.instance.LoadPrefabAsset<GameObject>("UI/UIInfinite",
            ()=> { BtnStart.gameObject.SetActive(false);

                UIInfinite _scrollInfinite =Instantiate(UIManager.instance.GetLoadAsset() as GameObject, ContentTr).GetComponent<UIInfinite>();
                _scrollInfinite.Init();
            });
    }
    #endregion

}
