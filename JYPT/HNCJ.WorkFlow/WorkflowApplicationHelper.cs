using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Configuration;
using System.Threading;
using System.Activities.DurableInstancing;
namespace HNCJ.WorkFlow
{

    public class WorkflowApplicationHelper 
    {
        private static readonly string StrSql;
        static WorkflowApplicationHelper()
        {
            StrSql = ConfigurationManager.ConnectionStrings["wfsql"].ConnectionString;
            //StrSql = "server=A55-K55;database=WF;user=sa;pwd=123456";
        }
        public static WorkflowApplication CreateWorkflowApp(Activity activity, Dictionary<string, object> dicParam)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp;
            if (dicParam == null)
            {
                wfApp = new WorkflowApplication(activity);
            }
            else {
                wfApp = new WorkflowApplication(activity,dicParam);
            }
            wfApp.Idle += a => {
                Console.WriteLine("工作流停下来了");
            };
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e3)
            {
                Console.WriteLine("工作卸载进行持久化");
                return PersistableIdleAction.Unload;
            };
            wfApp.Unloaded += a =>
            {

                autoResetEvent.Set();
                Console.WriteLine("工作流被卸载");
            };
            wfApp.OnUnhandledException += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("出现了异常...");

                return UnhandledExceptionAction.Terminate;
            };
            wfApp.Aborted += a =>
            {
                autoResetEvent.Set();
                Console.WriteLine("Aborted");
            };
            var store = new SqlWorkflowInstanceStore(StrSql);
            wfApp.InstanceStore = store;
            wfApp.Run();
            return wfApp;
        }
        public static WorkflowApplication ResumeBookMark(Activity activity, Guid instanceId,string bookmarkName,object value)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp = new WorkflowApplication(activity);
            wfApp.Idle += a => { Console.WriteLine("工作流停下来了"); };
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e3)
            {
                Console.WriteLine("工作卸载进行持久化");
                return PersistableIdleAction.Unload;
            };
            wfApp.Unloaded += a => {
                
                autoResetEvent.Set();
                Console.WriteLine("工作流被卸载");
            };
            wfApp.OnUnhandledException += a => {
                autoResetEvent.Set();
                Console.WriteLine("出现了异常...");

                return UnhandledExceptionAction.Terminate;
            };
            wfApp.Aborted += a => {
                autoResetEvent.Set();
                Console.WriteLine("Aborted");
            };
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(StrSql);
            wfApp.InstanceStore = store;
            wfApp.Load(instanceId);
            wfApp.ResumeBookmark(bookmarkName,value);
            return wfApp;
        }
    }
}
