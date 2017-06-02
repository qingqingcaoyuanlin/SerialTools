using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace SerialTool
{
    class PyFunction
    {
        static ScriptRuntime pyRumtime = Python.CreateRuntime();

        static dynamic pyFile;

        public void RunPythonFile(string file)
        {
            pyFile = pyRumtime.UseFile(file);
        }
        

        /// <summary>
        /// 获取脚本的用途
        /// </summary>
        /// <returns></returns>
        public string GetScriptFunctionDescription()
        {
            return pyFile.FunctionDescription();
        }

        /// <summary>
        /// 获取脚本解析返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetFunctionDissector(string data)
        {
            return pyFile.FunctionDissector(data);
        }

    }
}
