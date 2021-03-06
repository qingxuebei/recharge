﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {
        private static ILog log;
        private static LogHelper logHelper = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static ILog GetInstance()
        {
            logHelper = new LogHelper(null);

            return log;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static ILog GetInstance(string configPath)
        {
            logHelper = new LogHelper(configPath);

            return log;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configPath"></param>
        private LogHelper(string configPath)
        {
            if (!string.IsNullOrEmpty(configPath))
            {
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(configPath));
            }
            else
            {
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

    }
}