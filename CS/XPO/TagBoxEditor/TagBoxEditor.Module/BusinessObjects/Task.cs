using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class MyTask : BaseObject {
        public MyTask(Session session) : base(session) { }

        string subject;
        public string Subject { get => subject; set => SetPropertyValue(nameof(Subject), ref subject, value); }

        [Association("Employees-Tasks")]
        public XPCollection<Employee> Employees { get => GetCollection<Employee>(nameof(Employees)); }
    }
}
