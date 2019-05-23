using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoEngine.UI
{
	public static class UITool
	{

		public static T CreateUI<T>(GameObject _go) where T : UIContainer
		{
			UIContainer ui = null;
			
			if (typeof (T) is UIContainer)
			{
				ui = new UIContainer(_go);
			}
			else if (typeof(T) is UIButton)
			{
				ui = new UIButton(_go);
			}
			else if (typeof(T) is UIEventListener)
			{
				ui = new UIEventListener(_go);
			}
			else if (typeof(T) is UIImage)
			{
				ui = new UIImage(_go);
			}
			else if (typeof(T) is UIInput)
			{
				ui = new UIInput(_go);
			}
			else if (typeof(T) is UIText)
			{
				ui = new UIText(_go);
			}
			else if (typeof(T) is UITexture)
			{
				ui = new UITexture(_go);
			}
			
			return ui as T;
		}


		public static void AddClick(this Button btn, UnityAction<object> callback, object param = null)
		{
			btn.onClick.AddListener(delegate()
			{
				callback(param);
			});
		}

		public static void AddClick(this Button btn, UnityAction callback)
		{
			btn.onClick.AddListener(callback);
		}
	}
}

