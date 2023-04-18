using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Employee : BaseObject {
        public Employee(Session session) : base(session) { }

        string fullName;
        public string FullName { get => fullName; set => SetPropertyValue(nameof(FullName), ref fullName, value); }

        Department department;
        [Association("Department-Employees")]
        public Department Department { get => department; set => SetPropertyValue(nameof(Department), ref department, value); }

        [Association("Employees-Tasks")]
        public XPCollection<MyTask> Tasks { get => GetCollection<MyTask>(nameof(Tasks)); }
    }
}
