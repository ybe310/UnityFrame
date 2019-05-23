using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UIButton : UIContainer
	{
		[SerializeField]
		protected Button button;

		public Button Button
		{
			get { return button; }
		}

		public event UnityAction<object> onClick;

		public object parameter;

		

		public UIButton(GameObject _go) : base(_go)
		{
			button = _go.GetComponent<Button>();
			if (button != null)
			{
				button.AddClick(OnClickListener);
			}
			onClick = null;
		}


		public void AddListener(UnityAction<object> _func)
		{
			if (!IsExist())
				return;

			onClick += _func;
		}


		public void RemoveListener(UnityAction<object> _func)
		{
			if (!IsExist())
				return;

			onClick -= _func;
		}

		public void ClearListener()
		{
			onClick = null;
		}


		private void OnClickListener()
		{
			if (onClick != null)
			{
				onClick(parameter);
			}
		}

		public override bool IsExist()
		{
			if (button != null)
				return true;
			LoggerHelper.Error("Button为空，请检测！");
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			button = null;
		}
	}
}