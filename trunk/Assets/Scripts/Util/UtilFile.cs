using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
//using ICSharpCode.SharpZipLib.Zip;
//using SevenZip.Compression.LZMA;

/// <summary>
/// 文件处理工具类
/// </summary>
public class UtilFile
{
    public const string PassWord = "nice";//密码


#if UNITY_EDITOR_OSX
	public const string fileChar = "/";
    public const string oppChar = "\\";
#else
    public const string fileChar = "\\";
    public const string oppChar = "/";
#endif

    /// <summary>
    /// 根据平台替换文件内符号
    /// </summary>
    /// <param name="_str"></param>
    /// <returns></returns>
    public static string ReplaceFilePathChar(string _str)
    {
        return _str.Replace(oppChar, fileChar);
    }


    //计算MD5
    public static string GetFileHash(string filePath)
    {
        try
        {

            FileStream fs = new FileStream(filePath, FileMode.Open);
            int len = (int)fs.Length;
            byte[] data = new byte[len];
            fs.Read(data, 0, len);
            fs.Close();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string fileMD5 = "";
            foreach (byte b in result)
            {
                fileMD5 += System.Convert.ToString(b, 16);
            }
            return fileMD5;
        }
        catch (FileNotFoundException e)
        {
            Debug.Log(e.Message);
            return "";
        }
    }

    //查找文件夹下子文件夹
    public static List<DirectoryInfo> FindChildDirectory(string _path, SearchOption _searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(_path))
        {
            Debug.Log("文件夹不存在：" + _path);
            return null;
        }
        DirectoryInfo tDirect = new DirectoryInfo(_path);
        DirectoryInfo[] tChildDirectArray = tDirect.GetDirectories("*", _searchOption);
        List<DirectoryInfo> mDirectList = new List<DirectoryInfo>();
        for (int i = 0; i < tChildDirectArray.Length; i++)
        {
            mDirectList.Add(tChildDirectArray[i]);
        }

        return mDirectList;
    }
    /// <summary>
    /// 查找文件夹下的所有子文件夹
    /// </summary>
    /// <param name="_path"></param>
    /// <param name="_searchOption"></param>
    /// <returns></returns>
    public static List<DirectoryInfo> FindChildDirectorys(string _path, SearchOption _searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(_path))
        {
            return null;
        }
        DirectoryInfo tDirectory = new DirectoryInfo(_path);
        DirectoryInfo[] tChildDirectoryArray = tDirectory.GetDirectories("*", _searchOption);
        List<DirectoryInfo> mDirectory = new List<DirectoryInfo>();
        for (int i = 0; i < tChildDirectoryArray.Length; ++i)
        {
            mDirectory.Add(tChildDirectoryArray[i]);
        }
        return mDirectory;
    }

    //查找文件下最后面的文件夹
    public static List<DirectoryInfo> FindLastChildDirectory(string _path)
    {
        List<DirectoryInfo> mDirectList = new List<DirectoryInfo>();

        List<DirectoryInfo> tDirectList = FindChildDirectory(_path, SearchOption.AllDirectories);
        for (int i = 0; i < tDirectList.Count; i++)
        {
            if (tDirectList[i].GetDirectories().Length == 0)
            {
                mDirectList.Add(tDirectList[i]);
            }
        }

        return mDirectList;
    }

    //查找文件夹下所有文件
    public static List<FileInfo> FindDirectoryFiles(string _path, params string[] _extension)
    {
        List<FileInfo> tFileList = new List<FileInfo>();
        DirectoryInfo tDir = new DirectoryInfo(_path);
        if (tDir.Exists)
        {
            FileInfo[] tFiles = tDir.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < tFiles.Length; i++)
            {
                FileInfo file = tFiles[i];
                for (int j = 0; j < _extension.Length; j++)
                {
                    if (file.Extension.Equals(_extension[j]))
                    {
                        tFileList.Add(file);
                    }
                }
            }
        }

        return tFileList;
    }

    /// <summary>
    /// 查找文件夹下所有文件
    /// </summary>
    /// <param name="_path"></param>
    /// <param name="_extension"></param>
    /// <returns></returns>
    public static List<FileInfo> FindDirectoryFiles00(string _path, params string[] _extension)
    {
        List<FileInfo> mFileList = new List<FileInfo>();
        DirectoryInfo mDirectory = new DirectoryInfo(_path);
        if (mDirectory.Exists)
        {
            FileInfo[] tFiles = mDirectory.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < tFiles.Length; ++i)
            {
                FileInfo mFileInfo = tFiles[i];
                for (int j = 0; j < _extension.Length; ++j)
                {
                    if (mFileInfo.Extension.Equals(_extension[j]))
                    {
                        mFileList.Add(mFileInfo);
                    }
                }
            }
        }
        return mFileList;
    }

    //查找文件夹下所有文件
    public static List<FileSystemInfo> FindDirectoryFilesByStream(string _path, params string[] _extension)
    {
        List<FileSystemInfo> tFileList = new List<FileSystemInfo>();
        DirectoryInfo tDir = new DirectoryInfo(_path);
        if (tDir.Exists)
        {
            FileInfo[] tFiles = tDir.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < tFiles.Length; i++)
            {
                FileInfo file = tFiles[i];
                for (int j = 0; j < _extension.Length; j++)
                {
                    if (file.Extension.Equals(_extension[j]))
                    {
                        tFileList.Add(file);
                    }
                }
            }
        }

        return tFileList;
    }

    //查找文件夹下所有文件
    public static List<FileInfo> FindDirectoryFiles(DirectoryInfo _dire, params string[] _extension)
    {
        List<FileInfo> tFileList = new List<FileInfo>();
        FileInfo[] tFiles = _dire.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < tFiles.Length; i++)
        {
            FileInfo file = tFiles[i];
            for (int j = 0; j < _extension.Length; j++)
            {
                if (file.Extension.Equals(_extension[j]))
                {
                    tFileList.Add(file);
                }
            }
        }

        return tFileList;
    }

    //写Byte文件
    public static void WriteCreatePresentFile(string _path, byte[] data)
    {
        //IOTool.CreateFileBytes(ResourcePath.sGetResourceCachePath() + _path + ".text", data);
    }
    public static byte[] ReadPresentFileBytes(string _path)
    {
        //_path = ResourcePath.sGetResourceCachePath() + _path + ".text";
        byte[] bytes = null;
        //if (File.Exists(_path))
        //{
        //    bytes = File.ReadAllBytes(_path);
        //}

        return bytes;
    }

    //写文件
    public static void WriteCreateFile(string _path, string _text)
    {
        string dir = Path.GetDirectoryName(_path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllText(_path, _text);
    }

    //写文件
    public static void WriteCreateFile(string _path, string[] _texts)
    {
        string dir = Path.GetDirectoryName(_path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        List<byte> byteList = new List<byte>();

        for (int i = 0; i < _texts.Length; i++)
        {
            string str = _texts[i];
            if (!string.IsNullOrEmpty(str))
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(_texts[i]);

                byteList.AddRange(bytes);
            }
        }

        File.WriteAllBytes(_path, byteList.ToArray());
    }

    //写文件
    public static void WriteCreateFile(string _path, byte[] _bytes)
    {
        if (_bytes == null)
            return;

        string dir = Path.GetDirectoryName(_path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        //FileStream fs = new FileStream(_path, FileMode.Create);
        //BinaryWriter bw = new BinaryWriter(fs);
        //bw.Write(_bytes);

        //fs.Close();

        File.WriteAllBytes(_path, _bytes);
    }

    /// <summary>
    /// 写文件
    /// </summary>
    /// <param name="_path"></param>
    /// <param name="_bytes"></param>
    public static void WriteCreateFile00(string _path, byte[] _bytes)
    {
        if (_bytes == null) return;
        string dir = Path.GetDirectoryName(_path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        File.WriteAllBytes(_path, _bytes);
    }

    /// <summary>
    /// 读文件
    /// </summary>
    /// <param name="_path"></param>
    /// <returns></returns>
    public static string ReadFile00(string _path)
    {
        string text = string.Empty;
        if (!File.Exists(_path))
        {
            return text;
        }
        text = File.ReadAllText(_path);
        return text;
    }

    //读文件
    public static string ReadFile(string _path)
    {
        string text = string.Empty;
        if (!File.Exists(_path))
        {
            Debug.Log("文件不存在... " + _path);
            return text;
        }

        text = File.ReadAllText(_path);

        return text;
    }

    public static byte[] ReadFileBytes(string _path)
    {
        byte[] bytes = null;
        if (File.Exists(_path))
        {
            bytes = File.ReadAllBytes(_path);
        }

        return bytes;
    }

    //替换文件内容 文件必须存在
    public static bool ReplaceFileContent(string _path, string _old, string _new)
    {
        string oldStr = string.Empty; ;
        using (StreamReader sr = new StreamReader(_path))
        {
            oldStr = sr.ReadToEnd();
        }
        if (!string.IsNullOrEmpty(oldStr))
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string newStr = oldStr.Replace(_old, _new);
                sw.Write(newStr);
            }
            return true;
        }

        return false;
    }
    /// <summary>
    /// 替换文件内容(文件必须存在)
    /// </summary>
    /// <param name="_path"></param>
    /// <param name="_old"></param>
    /// <param name="_new"></param>
    /// <returns></returns>
    public static bool ReplaceFileConten00(string _path, string _old, string _new)
    {
        string oldStr = string.Empty;
        using (StringReader sr = new StringReader(_path))
        {
            oldStr = sr.ReadToEnd();
        }
        if (!string.IsNullOrEmpty(oldStr))
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string newStr = oldStr.Replace(_old, _new);
                sw.Write(newStr);
            }
            return true;
        }
        return false;
    }


    //添加到文件最后
    public static void WriteToEnd(string _path, string _str)
    {
        using (FileStream fs = new FileStream(_path, FileMode.Append))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.Write(_str);
        }
    }

    //替换文件内容 文件必须存在
    public static bool ReplaceFileContentOrToEnd(string _path, string _old, string _new)
    {
        string oldStr = string.Empty;
        using (StreamReader sr = new StreamReader(_path))
        {
            oldStr = sr.ReadToEnd();
        }

        if (!string.IsNullOrEmpty(_old) && oldStr.Contains(_old))
        {
            using (StreamWriter sw = new StreamWriter(_path))
            {
                string newStr = oldStr.Replace(_old, _new);
                sw.Write(newStr);
            }
        }
        else
        {
            using (FileStream fs = new FileStream(_path, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(_new);
            }
        }

        return true;
    }

	/*
    #region 文件压缩

    /// <summary>
    /// 压缩文件
    /// </summary>
    public static void CompressFileBy7Zip(string _filePath, string _newPath)
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogError("文件不存在：" + _filePath);
            return;
        }

        string newPath = UtilFile.ReplaceFilePathChar(_newPath);
        int lastIndex = _newPath.LastIndexOf(UtilFile.fileChar);
        string dirPath = newPath.Substring(0, lastIndex);

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        SevenZipHelper.Zip(_filePath, _newPath);

        Debug.Log("压缩成功：" + _newPath);
    }


    public static void CompressFileByFastZip(string _needCompressDirectoryPath, string _newPath, string _fileFilter)
    {
        if (!Directory.Exists(_needCompressDirectoryPath))
        {
            Debug.LogError("文件夹不存在：" + _needCompressDirectoryPath);
            return;
        }

        FastZipEvents zipEvent = new FastZipEvents();
        zipEvent.CompletedFile = (o, e) =>
        {
            Debug.Log("压缩成功：" + e.Name);
        };
        zipEvent.FileFailure = (o, e) =>
        {
            Debug.Log("压缩失败：" + e.Name);
        };

        FastZip fastZip = new FastZip(zipEvent);

        fastZip.CreateZip(_newPath, _needCompressDirectoryPath, false, _fileFilter);
    }


    public static void UnCompressFileBy7Zip(string _filePath, string _newPath)
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogError("文件不存在：" + _filePath);
            return;
        }

        SevenZipHelper.Unzip(_filePath, _newPath);

        Debug.Log("解压成功：" + _newPath);
    }


    public static void UnCompressFileByFastZip(string _filePath, string _targetDirectory, string _fileFilter, DelegateEvent.CallBack _completeCallBack = null)
    {
        FastZipEvents zipEvent = new FastZipEvents();
        zipEvent.CompletedFile = (o, e) =>
        {
            if (_completeCallBack != null)
            {
                _completeCallBack();
            }

            LoggerHelper.Debug("解缩成功：" + e.Name);
        };
        zipEvent.FileFailure = (o, e) =>
        {
            LoggerHelper.Debug("解缩失败：" + e.Name + ", error = " + e.Exception.Message);
        };

        FastZip fastZip = new FastZip(zipEvent);
        fastZip.ExtractZip(_filePath, _targetDirectory, _fileFilter);

    }

    #endregion
	*/

    #region 加密解密

    /// <summary>
    /// 文件加密
    /// </summary>
    /// <param name="_filePath"></param>
    /// <param name="_password"></param>
    public static void DoEncryptToFile(string _filePath, string _password = PassWord)
    {
        if (!File.Exists(_filePath))
        {
            return;
        }
        byte[] bytes = FileEncrypt(_filePath, _password);
        FileStream fs = new FileStream(_filePath, FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
    }

    public static void DoEncryptToOtherFile(string _filePath, string _outputPath, string _password = PassWord)
    {
        if (!File.Exists(_filePath))
        {
            return;
        }
        byte[] bytes = FileEncrypt(_filePath, _password);

        using (FileStream fs = new FileStream(_outputPath, FileMode.Create))
        {
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }
    }

    public static byte[] FileEncrypt(string _filePath, string _password = PassWord)
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }
        byte[] bytes = File.ReadAllBytes(_filePath);

        return PackEncrypt(bytes, bytes.Length, _password);
    }

    /// <summary>
    /// 加密解密
    /// </summary>
    /// <param name="_bytes"></param>二进制数组
    /// <param name="_len"></param>数组长度
    /// <param name="_password"></param>密码
    /// <returns></returns>
    public static byte[] PackEncrypt(byte[] _bytes, int _len, string _password = PassWord)
    {
        int strIndex = 0;
        for (int i = 0; i < _len; i++)
        {
            if (strIndex >= _password.Length)
                strIndex = 0;
            _bytes[i] ^= (byte)_password[strIndex++];
        }

        return _bytes;
    }

    #endregion
}
