using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class LogRecord : MonoBehaviour
{   
    // 使用StringBuilder来优化字符串的重复构造
    StringBuilder m_logStr = new StringBuilder();
    // 日志文件存储位置
    string m_logFileSavePath;
    string m_errorPath;
    private void Awake() {
        var t = System.DateTime.Now.ToString("yyyy-MM-dd");
        m_logFileSavePath = string.Format("{0}/output_{1}.log",Application.streamingAssetsPath+"/Logs",t);
        m_errorPath = string.Format("{0}/outputError_{1}.log",Application.streamingAssetsPath+"/Logs/Error",t);
        Application.logMessageReceived += OnLogCallBack;
        
    }

    private void OnLogCallBack(string condition, string stackTrace, LogType type)
    {
        var t = System.DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
        m_logStr.Append("\n").Append(t).Append(":").Append(condition).Append("\n").Append(stackTrace).Append("\n");
        if(m_logStr.Length <= 0)
        {
            return;
        }

        if(!File.Exists(m_logFileSavePath))
        {
            var f = File.Create(m_logFileSavePath);
            f.Close();
        }else
        {
            var sw = File.AppendText(m_logFileSavePath);
            sw.WriteLine(m_logStr.ToString());
            sw.Close();
        }

        if(type == LogType.Exception)
        {
            if(!File.Exists(m_errorPath))
            {
                var f = File.Create(m_errorPath);
                f.Close();
            }else
            {
                var sw = File.AppendText(m_errorPath);
                sw.WriteLine(m_logStr.ToString());
                sw.Close();
            }
        }

        m_logStr.Remove(0, m_logStr.Length);


    }

}
