//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class BoEngineUtil 
//{
//	private static Dictionary<int, Type> HandlerDic = new Dictionary<int, Type>();
//	private static Dictionary<int, Dictionary<int, Type>> CmdDic = new Dictionary<int, Dictionary<int, Type>>();
//	private static Dictionary<int, Type> DataModelDic = new Dictionary<int, Type>(); 

//	static BoEngineUtil()
//	{
//		Type[] types = typeof(BoEngineUtil).Assembly.GetTypes();
//		for (int i = 0; i < types.Length; i++)
//		{
//			Type type = types[i];

//			if (Attribute.IsDefined(type, typeof(NetCmdHandlerAttribute)))
//			{
//				NetCmdHandlerAttribute attr = (NetCmdHandlerAttribute)Attribute.GetCustomAttribute(type, typeof(NetCmdHandlerAttribute));

//				if (!HandlerDic.ContainsKey(attr.gsCmd))
//				{
//					HandlerDic.Add(attr.gsCmd, type);
//				}
//				else
//				{
//					LoggerHelper.Error("NetCmdHandler注册重复ID：" + type.Name);
//				}
//			}
//			else if (Attribute.IsDefined(type, typeof (NetCmdAttribute)))
//			{
//				NetCmdAttribute attr = (NetCmdAttribute)Attribute.GetCustomAttribute(type, typeof(NetCmdAttribute));
//				stNullClientCmd cmdBase = (stNullClientCmd)Activator.CreateInstance(attr.gsCmdType);

//				if (!CmdDic.ContainsKey(cmdBase.gsCmd))
//				{
//					CmdDic.Add(cmdBase.gsCmd, new Dictionary<int, Type>());
//				}
//				else
//				{
//					Dictionary<int, Type> dic = CmdDic[cmdBase.gsCmd];

//					if (!dic.ContainsKey(attr.gsParam))
//					{
//						dic.Add(attr.gsParam, type);
//					}
//					else
//					{
//						LoggerHelper.Error("NetCmd注册重复 gsCmd ：" + cmdBase.gsCmd + ", gsParam = " + attr.gsParam);
//					}
//				}
//			}
//			else if (Attribute.IsDefined(type, typeof(DataModelAttribute)))
//			{
//				DataModelAttribute attr = (DataModelAttribute)Attribute.GetCustomAttribute(type, typeof(DataModelAttribute));

//				if (!DataModelDic.ContainsKey(attr.dataType))
//				{
//					DataModelDic.Add(attr.dataType, type);
//				}
//				else
//				{
//					LoggerHelper.Error("DataModel注册重复 dataType ：" + attr.dataType);
//				}
//			}
//		}
//	}



//	public static IHandler GetNetCmdHandler(int _gsCmd)
//	{
//		IHandler nch = null;
//		Type type;
//		if (HandlerDic.TryGetValue(_gsCmd, out type))
//		{
//			 nch = (IHandler)Activator.CreateInstance(type);

//		}
//		if (nch == null)
//		{
//			LoggerHelper.Error("创建NetCmdHandler失败： gsCmd =" + _gsCmd);
//		}

//		return nch;
//	}


//	public static stNullClientCmd GetNetCmdMsg(int _gsCmd, int _gsParam)
//	{
//		stNullClientCmd cmdBase = null;
//		Dictionary<int, Type> dic;
//		Type type;
//		if (CmdDic.TryGetValue(_gsCmd, out dic))
//		{
//			if (CmdDic[_gsCmd].TryGetValue(_gsParam, out type))
//			{
//				cmdBase = (stNullClientCmd)Activator.CreateInstance(type);
//			}
//		}

//		if (cmdBase == null)
//		{
//			LoggerHelper.Error("创建NetCmd失败 gsCmd ：" + _gsCmd + ", gsParam = " + _gsParam);
//		}

//		return cmdBase;
//	}


//	public static List<int> GetCmdTypeList()
//	{
//		return new List<int>(CmdDic.Keys);
//	}


//	public static IDataModel GetDataModel(int _dataType)
//	{
//		IDataModel dataModel = null;
//		Type type;
//		if (DataModelDic.TryGetValue(_dataType, out type))
//		{
//			dataModel = (IDataModel)Activator.CreateInstance(type);
//			dataModel.Type = _dataType;
//		}

//		if (dataModel == null)
//		{
//			LoggerHelper.Error("创建DataModel失败 _dataType ：" + _dataType);
//		}

//		return dataModel;
//	}

//	public static List<int> GetDataModelTypeList()
//	{
//		return new List<int>(DataModelDic.Keys);
//	} 

//}
