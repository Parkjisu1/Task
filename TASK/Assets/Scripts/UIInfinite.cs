using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;


public class UIInfinite : UIBehaviour
{
	[SerializeField]
	private RectTransform ScrollListItem_Clone;

	[SerializeField, Range(0, 30)]
	int ScrollListItem_Cnt = 9;

	[SerializeField]
	private Direction direction;

	[System.NonSerialized]
	public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

	protected float diffPreFramePosition = 0;

	protected int currentItemNo = 0;

	public enum Direction
	{
		Vertical,
		Horizontal,
	}

	[Header("[Content]")]
	public RectTransform contentTr;
	[Header("[Scroll Rect]")]
	public ScrollRect scrollRect;

	private List<ScrollData> ScrollInfiniteDataList = new List<ScrollData>();
	
	private float anchoredPosition
	{
		get
		{
			return direction == Direction.Vertical ? -contentTr.anchoredPosition.y : contentTr.anchoredPosition.x;
		}
	}

	private float _itemScale = -1;
	public float itemScale
	{
		get
		{
			if (ScrollListItem_Clone != null && _itemScale == -1)
			{
				_itemScale = direction == Direction.Vertical ? ScrollListItem_Clone.sizeDelta.y : ScrollListItem_Clone.sizeDelta.x;
			}
			return _itemScale;
		}
	}

	public void Init()
	{
		ScrollInfiniteDataList.Clear();

		for(int i= 0; i< 50; i++)
        {
			ScrollInfiniteDataList.Add(new ScrollData(i.ToString(),i.ToString()));
        }

		scrollRect.horizontal = direction == Direction.Horizontal;
		scrollRect.vertical = direction == Direction.Vertical;
		scrollRect.content = contentTr;

		UIManager.instance.LoadPrefabAsset<GameObject>("UI/UIScrollRectInfiniteListItem", () => {
			GameObject _clone = UIManager.instance.GetLoadAsset() as GameObject;
			ScrollListItem_Clone = _clone.GetComponent<RectTransform>();

			for (int i = 0; i < ScrollListItem_Cnt; i++)
			{
				var item = GameObject.Instantiate(ScrollListItem_Clone) as RectTransform;
				item.SetParent(contentTr, false);
				item.name = $"Clone_{i}";
				item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
				itemList.AddLast(item);

				UIScrollRectInfiniteListItem _items = item.GetComponent<UIScrollRectInfiniteListItem>();
				_items.Init(ScrollInfiniteDataList[i]);

				item.gameObject.SetActive(true);
			}
		});
	}

	void Update()
	{
		if (itemList.First == null)
		{
			return;
		}

		while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
		{
			diffPreFramePosition -= itemScale;

			var item = itemList.First.Value;
			itemList.RemoveFirst();
			itemList.AddLast(item);

			var pos = itemScale * ScrollListItem_Cnt + itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
			
			UIScrollRectInfiniteListItem _items = item.GetComponent<UIScrollRectInfiniteListItem>();
			_items.Init(ScrollInfiniteDataList[Mathf.Abs(currentItemNo% ScrollInfiniteDataList.Count)]);

			currentItemNo++;
		}

		while (anchoredPosition - diffPreFramePosition > 0)
		{
			diffPreFramePosition += itemScale;

			var item = itemList.Last.Value;
			itemList.RemoveLast();
			itemList.AddFirst(item);

			UIScrollRectInfiniteListItem _items = item.GetComponent<UIScrollRectInfiniteListItem>();
			_items.Init(ScrollInfiniteDataList[Mathf.Abs(currentItemNo % ScrollInfiniteDataList.Count)]);

			currentItemNo--;

			var pos = itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

		}
	}
}
