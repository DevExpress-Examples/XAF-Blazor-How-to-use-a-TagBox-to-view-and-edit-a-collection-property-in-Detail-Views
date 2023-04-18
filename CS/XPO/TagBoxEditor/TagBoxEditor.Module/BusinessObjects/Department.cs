using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {

    [DefaultClassOptions]
    public class Department : BaseObject {
        public Department(Session session) : base(session) { }

        string name;
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }

        string description;
        [Size(4096)]
        public string Description { get => description; set => SetPropertyValue(nameof(Description), ref description, value); }

        [Association("Department-Employees")]
        public XPCollection<Employee> Employees { get => GetCollection<Employee>(nameof(Employees)); }
    }
}
