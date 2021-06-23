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
        public XPCollection<Task> Tasks { get => GetCollection<Task>(nameof(Tasks)); }
    }

    [DefaultClassOptions]
    public class Task : BaseObject {
        public Task(Session session) : base(session) { }

        string subject;
        public string Subject { get => subject; set => SetPropertyValue(nameof(Subject), ref subject, value); }

        [Association("Employees-Tasks")]
        public XPCollection<Employee> Employees { get => GetCollection<Employee>(nameof(Employees)); }
    }

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
