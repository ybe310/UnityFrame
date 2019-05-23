using System;

[AttributeUsage(AttributeTargets.Class)]
public class DataModelAttribute : Attribute
{
	public int dataType;


	public DataModelAttribute(int _dataType)
	{
		dataType = _dataType;
	}
}
