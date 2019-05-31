using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BoEngine.UI
{
	public static class UITool
	{

		public static T CreateUI<T>(GameObject _go) where T : UIContainer
		{
			UIContainer ui = null;
			
			if (typeof (T) == typeof(UIContainer))
			{
				ui = new UIContainer(_go);
			}
			else if (typeof(T) == typeof(UIButton))
			{
				ui = new UIButton(_go);
			}
			else if (typeof(T) == typeof(UIEventListener))
			{
				ui = new UIEventListener(_go);
			}
			else if (typeof(T) == typeof(UIImage))
			{
				ui = new UIImage(_go);
			}
			else if (typeof(T) == typeof(UIInput))
			{
				ui = new UIInput(_go);
			}
			else if (typeof(T) == typeof(UIText))
			{
				ui = new UIText(_go);
			}
			else if (typeof(T) == typeof(UITexture))
			{
				ui = new UITexture(_go);
			}
			
			return ui as T;
		}

		public static T CreateUI<T>(Object _obj) where T : UIContainer
		{
			UIContainer ui = null;

			if (typeof(T) == typeof(UIContainer))
			{
				ui = new UIContainer(_obj as GameObject);
			}
			else if (typeof(T) == typeof(UIButton))
			{
				ui = new UIButton(_obj as Button);
			}
			else if (typeof(T) == typeof(UIEventListener))
			{
				ui = new UIEventListener(_obj as GameObject);
			}
			else if (typeof(T) == typeof(UIImage))
			{
				ui = new UIImage(_obj as Image);
			}
			else if (typeof(T) == typeof(UIInput))
			{
				ui = new UIInput(_obj as InputField);
			}
			else if (typeof(T) == typeof(UIText))
			{
				ui = new UIText(_obj as Text);
			}
			else if (typeof(T) == typeof(UITexture))
			{
				ui = new UITexture(_obj as RawImage);
			}

			if (ui != null)
			{
				return ui as T;
			}

			return null;
		}

		public static UIContainer CreateUIContainer(Object _obj)
		{
			UIContainer ui = CreateUI<UIContainer>(_obj);

			return ui;
		}

		public static UIImage CreateUIImage(Object _obj)
		{
			UIImage ui = CreateUI<UIImage>(_obj);

			return ui;
		}

		public static UIText CreateUIText(Object _obj)
		{
			UIText ui = CreateUI<UIText>(_obj);

			return ui;
		}

		public static UITexture CreateUITexture(Object _obj)
		{
			UITexture ui = CreateUI<UITexture>(_obj);

			return ui;
		}

		public static UIButton CreateUIButton(Object _obj)
		{
			UIButton ui = CreateUI<UIButton>(_obj);

			return ui;
		}

		public static UIInput CreateUIInput(Object _obj)
		{
			UIInput ui = CreateUI<UIInput>(_obj);

			return ui;
		}

		public static UIEventListener CreateUIEventListener(Object _obj)
		{
			UIEventListener ui = CreateUI<UIEventListener>(_obj);

			return ui;
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

