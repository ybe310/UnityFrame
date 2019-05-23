using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
	[Serializable]
	public class UIContainer : IUIBase
	{
		[SerializeField]
		protected Transform tf;
		[SerializeField]
		protected GameObject go;


		public Transform transform
		{
			get
			{
				return tf;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return go;
			}
		}


		public Vector3 localPostion
		{
			get
			{
				if (IsExist())
					return tf.localPosition;
				return Vector3.zero;
			}
			set
			{
				if (IsExist())
					tf.localPosition = value;
			}
		}


		public Vector3 postion
		{
			get
			{
				if (IsExist())
					return tf.position;
				return Vector3.zero;
			}
			set
			{
				if (IsExist())
					tf.position = value;
			}
		}


		public Vector3 localEulerAngles
		{
			get
			{
				if (IsExist())
					return tf.localEulerAngles;
				return Vector3.zero;
			}
			set
			{
				if (IsExist())
					tf.localEulerAngles = value;
			}
		}

		public Vector3 eulerAngles
		{
			get
			{
				if (IsExist())
					return tf.eulerAngles;
				return Vector3.zero;
			}
			set
			{
				if (IsExist())
					tf.eulerAngles = value;
			}
		}


		public Vector3 localScale
		{
			get
			{
				if (IsExist())
					return tf.localScale;
				return Vector3.zero;
			}
			set
			{
				if (IsExist())
					tf.localScale = value;
			}
		}



		protected UIContainer()
		{
		}

		public UIContainer(GameObject _go)
		{
			Init(_go);
		}

		protected void Init(GameObject _go)
		{
			go = _go;
			if (go != null)
			{
				tf = _go.transform;
			}
		}

		public void SetActive(bool _active)
		{
			if (go != null)
				go.SetActive(_active);
		}

		public virtual bool IsExist()
		{
			if (tf != null)
				return true;
			
			return false;
		}

		public virtual void Dispose()
		{
			tf = null;
			go = null;
		}
	}
}