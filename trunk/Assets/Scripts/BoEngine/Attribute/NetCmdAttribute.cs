using System;

[AttributeUsage(AttributeTargets.Class)]
public class NetCmdHandlerAttribute : Attribute
{
	public int gsCmd;


	public NetCmdHandlerAttribute(int _gsCmd)
	{
		gsCmd = _gsCmd;
	}

}

[AttributeUsage(AttributeTargets.Class)]
public class NetCmdAttribute : Attribute
{
	public Type gsCmdType;
	public int gsParam;


	public NetCmdAttribute(Type _gsCmdType, int _gsParam)
	{
		gsCmdType = _gsCmdType;
		gsParam = _gsParam;
	}

}
