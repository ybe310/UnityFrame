using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UIImage : UIContainer
	{
		[SerializeField]
		protected Image image;

		public Image Image
		{
			get
			{
				return image; 
			}
		}

		public Sprite sprite
		{
			get
			{
				if (IsExist())
					return image.sprite;
				return null;
			}
			set
			{
				if (IsExist())
					image.sprite = value;
			}
		}
		

		public UIImage(GameObject _go) : base(_go)
		{
			image = _go.GetComponent<Image>();
		}

		public UIImage(Image _image)
		{
			image = _image;
			if (image!=null)
			{
				Init(image.gameObject);
			}
		}

		public override bool IsExist()
		{
			if (image != null)
				return true;
			LoggerHelper.Error("RawImage为空，请检查！");
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			image = null;
		}

	}
}

