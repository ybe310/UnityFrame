#region ģ����Ϣ
/*----------------------------------------------------------------
// Copyright (C) 2015 ����ò�
//
// ģ������LoggerHelper
// �����ߣ�Leon Xu
// �޸����б�
// �������ڣ�2015.12.7
// ģ����������־����ࡣ
//----------------------------------------------------------------*/
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;


/// <summary>
/// ��־�ȼ�������
/// </summary>
[Flags]
public enum eLogLevel
{
    eLOG_NONE = 0,           //Ĭ��
    eLOG_DEBUG = 1,          //����
    eLOG_INFO = 2,           //��Ϣ
    eLOG_WARNING = 4,        //����
    eLOG_ERROR = 8,          //����
    eLOG_EXCEPT = 16,        //�쳣
    eLOG_CRITICAL = 32,      //�ؼ�����
}

/// <summary>
/// ��־�����ࡣ
/// </summary>
/// 
static public class LoggerHelper
{
     //��־�ȼ���Ĭ��
    static public eLogLevel m_sCurrentLogLevels = eLogLevel.eLOG_DEBUG |
                                               eLogLevel.eLOG_INFO |
                                               eLogLevel.eLOG_WARNING |
                                               eLogLevel.eLOG_ERROR |
                                               eLogLevel.eLOG_EXCEPT |
                                               eLogLevel.eLOG_CRITICAL;

    //static public eLogLevel m_sCurrentLogLevels = eLogLevel.eLOG_ERROR |
    //                                           eLogLevel.eLOG_EXCEPT |
    //                                           eLogLevel.eLOG_CRITICAL;

    static private bool m_isLuaLog = true; //�Ƿ��ӡlua��־

    static private LogWriter m_logWriter;
    static private ulong index = 0;// debug����

    static LoggerHelper()
    {
        if (true)
        {
            m_isLuaLog = true;
            m_sCurrentLogLevels = eLogLevel.eLOG_DEBUG |
                                               eLogLevel.eLOG_INFO |
                                               eLogLevel.eLOG_WARNING |
                                               eLogLevel.eLOG_ERROR |
                                               eLogLevel.eLOG_EXCEPT |
                                               eLogLevel.eLOG_CRITICAL;
        }
        else
        {
            m_isLuaLog = false;
            m_sCurrentLogLevels = eLogLevel.eLOG_ERROR |
                                           eLogLevel.eLOG_EXCEPT |
                                           eLogLevel.eLOG_CRITICAL;
        }

        m_logWriter = new LogWriter();
        Application.logMessageReceived += new Application.LogCallback(ProcessExceptionReport);
    }

    static public void Release()
    {
        m_logWriter.Release();
    }

    /// <summary>
    /// ������־��
    /// </summary>
    /// <param name="message">��־����</param>
    /// <param name="isShowStack">�Ƿ���ʾ����ջ��Ϣ</param>
    static public void Debug(object message,bool isNeedStack = false)
    {
        if (eLogLevel.eLOG_DEBUG == (m_sCurrentLogLevels & eLogLevel.eLOG_DEBUG))
        {
            if(isNeedStack)
                Log(string.Concat(" [DEBUG]: ", message, " Index = ", index++, '\n', GetStacksInfo()), eLogLevel.eLOG_DEBUG);
            else
                Log(string.Concat(" [DEBUG]: ", message, " Index = ", index++), eLogLevel.eLOG_DEBUG);
        }
    }

    /// <summary>
    /// ��Ϣ��־��
    /// </summary>
    /// <param name="message">��־����</param>
    static public void Info(object message)
    {
        if (eLogLevel.eLOG_INFO == (m_sCurrentLogLevels & eLogLevel.eLOG_INFO))
            Log(string.Concat(" [INFO]: ", message), eLogLevel.eLOG_INFO);
    }

    /// <summary>
    /// ������־��
    /// </summary>
    /// <param name="message">��־����</param>
    static public void Warning(object message,bool isNeedStatck = false)
    {
        if (eLogLevel.eLOG_WARNING == (m_sCurrentLogLevels & eLogLevel.eLOG_WARNING))
        {
            if(isNeedStatck)
                Log(string.Concat(" [WARNING]: ", message, '\n', GetStacksInfo()), eLogLevel.eLOG_WARNING);
            else
                Log(string.Concat(" [WARNING]: ", message), eLogLevel.eLOG_WARNING);
        }
    }

    /// <summary>
    /// �쳣��־��
    /// </summary>
    /// <param name="message">��־����</param>
    static public void Error(object message)
    {
        if (eLogLevel.eLOG_ERROR == (m_sCurrentLogLevels & eLogLevel.eLOG_ERROR))
            Log(string.Concat(" [ERROR]: ", message, '\n', GetStacksInfo()), eLogLevel.eLOG_ERROR);
        
        m_logWriter.UploadToServer((ushort)LogType.Error, message.ToString());
    }

    /// <summary>
    /// �ؼ���־��
    /// </summary>
    /// <param name="message">��־����</param>
    static public void Critical(object message)
    {
        if (eLogLevel.eLOG_CRITICAL == (m_sCurrentLogLevels & eLogLevel.eLOG_CRITICAL))
            Log(string.Concat(" [CRITICAL]: ", message, '\n', GetStacksInfo()), eLogLevel.eLOG_CRITICAL);
    }

    /// <summary>
    /// �쳣��־��
    /// </summary>
    /// <param name="ex">�쳣ʵ����</param>
    static public void Except(Exception ex, object message = null)
    {
        if (eLogLevel.eLOG_EXCEPT == (m_sCurrentLogLevels & eLogLevel.eLOG_EXCEPT))
        {
            Exception innerException = ex;
            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }
            Log(string.Concat(" [EXCEPT]: ", message == null ? "" : message + "\n", ex.Message, innerException.StackTrace), eLogLevel.eLOG_EXCEPT);
        }
    }

    /// <summary>
    /// ��ȡ��ջ��Ϣ��
    /// </summary>
    /// <returns></returns>
    private static String GetStacksInfo()
    {
        StringBuilder sb = new StringBuilder();
        StackTrace st = new StackTrace();
        var sf = st.GetFrames();
        for (int i = 2; i < sf.Length; i++)
        {
            sb.AppendLine(sf[i].ToString());
        }

        return sb.ToString();
    }

    /// <summary>
    /// д��־��
    /// </summary>
    /// <param name="message">��־����</param>
    private static void Log(string message, eLogLevel level, bool writeEditorLog = true)
    {
        var msg = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), message);
        m_logWriter.WriteLog(msg, level, writeEditorLog);
    }

    /// <summary>
    /// ��ȡ����ջ��Ϣ��
    /// </summary>
    /// <returns>����ջ��Ϣ</returns>
    private static String GetStackInfo()
    {
        StackTrace st = new StackTrace();
        StackFrame sf = st.GetFrame(2);//[0]Ϊ����ķ��� [1]Ϊ���÷���
        var method = sf.GetMethod();
        return String.Format("{0}.{1}(): ", method.ReflectedType.Name, method.Name);
    }

    static private void ProcessExceptionReport(string message, string stackTrace, LogType type)
    {
        var logLevel = eLogLevel.eLOG_DEBUG;
        switch (type)
        {
            case LogType.Assert:
                logLevel = eLogLevel.eLOG_DEBUG;
                break;
            case LogType.Error:
                logLevel = eLogLevel.eLOG_ERROR;
                break;
            case LogType.Exception:
                logLevel = eLogLevel.eLOG_EXCEPT;
                break;
            case LogType.Log:
                logLevel = eLogLevel.eLOG_DEBUG;
                break;
            case LogType.Warning:
                logLevel = eLogLevel.eLOG_WARNING;
                break;
            default:
                break;
        }

        if (logLevel == (m_sCurrentLogLevels & logLevel))
        {
            //���� �� debug������ӡ��ջ
            if (logLevel == eLogLevel.eLOG_WARNING || logLevel == eLogLevel.eLOG_DEBUG)
                Log(string.Concat(" [SYS_", logLevel, "]: ", message), logLevel, false);
            else
                Log(string.Concat(" [SYS_", logLevel, "]: ", message, '\n', stackTrace), logLevel, false);
        }

        //�ϴ����ش���
        if( logLevel == eLogLevel.eLOG_ERROR ||
            logLevel == eLogLevel.eLOG_EXCEPT || 
            logLevel == eLogLevel.eLOG_CRITICAL )
        {
            m_logWriter.UploadToServer((ushort)type, message);
        }
    }
}

/// <summary>
/// ��־д���ļ������ࡣ
/// </summary>
public class LogWriter
{
	private string m_logPath = "";//ResourcePath.sGetLogCachePath();
    private string m_logFileName = "log_{0}.txt";
    private string m_logFilePath;
    private FileStream m_fs;
    private StreamWriter m_sw;
    private Action<String, eLogLevel, bool> m_logWriter;
    private readonly static object m_locker = new object();

    /// <summary>
    /// Ĭ�Ϲ��캯����
    /// </summary>
    public LogWriter()
    {
		m_logWriter = Write;
		//if (!Directory.Exists(m_logPath))
		//    Directory.CreateDirectory(m_logPath);

		//m_logFilePath = String.Concat(m_logPath, String.Format(m_logFileName, DateTime.Today.ToString("yyyyMMdd")));
		//try
		//{
		//    m_logWriter = Write;
		//    m_fs = new FileStream(m_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
		//    m_sw = new StreamWriter(m_fs);
		//}
		//catch (Exception ex)
		//{
		//    UnityEngine.Debug.LogError(ex.Message);
		//}
	}

    /// <summary>
    /// �ͷ���Դ��
    /// </summary>
    public void Release()
    {
        lock (m_locker)
        {
            if (m_sw != null)
            {
                m_sw.Close();
                m_sw.Dispose();
                m_sw = null;
            }
            if (m_fs != null)
            {
                m_fs.Close();
                m_fs.Dispose();
                m_fs = null;
            }
        }
    }

    //�ϴ���־
    public void UploadToServer(ushort type,string logs)
    {
#if !UNITY_EDITOR
		/*
        CRCClientCmd.stUserBugCommitClientCmd stCmd = new CRCClientCmd.stUserBugCommitClientCmd();
        stCmd.mBug_type = type;
        stCmd.mUser_id = NetAutoConnect.Instance.GetUserId();

        //CUtility.StringToTdrBytesDefault(stCmd.mBug_content, logs);
        stCmd.mBug_size = (ushort)stCmd.mBug_content.Length;

        NetSys.Instance.SendCmd(stCmd);
		*/
#endif
    }

    /// <summary>
    /// д��־��
    /// </summary>
    /// <param name="msg">��־����</param>
    public void WriteLog(string msg, eLogLevel level, bool writeEditorLog)
    {
        m_logWriter(msg, level, writeEditorLog);
    }

    private void Write(string msg, eLogLevel level, bool writeEditorLog)
    {
        lock (m_locker)
            try
            {
//#if UNITY_EDITOR
                if (writeEditorLog)
                {
                    switch (level)
                    {
                        case eLogLevel.eLOG_DEBUG:
                        case eLogLevel.eLOG_INFO:
                            UnityEngine.Debug.Log(msg);
                            break;
                        case eLogLevel.eLOG_WARNING:
                            UnityEngine.Debug.LogWarning(msg);
                            break;
                        case eLogLevel.eLOG_ERROR:
                        case eLogLevel.eLOG_EXCEPT:
                        case eLogLevel.eLOG_CRITICAL:
                            UnityEngine.Debug.LogError(msg);
                            break;
                        default:
                            break;
                    }
                }
//#endif
                if (m_sw != null)
                {
                    m_sw.WriteLine(msg);
                    m_sw.Flush();
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError(ex.Message);
            }
    }
}
