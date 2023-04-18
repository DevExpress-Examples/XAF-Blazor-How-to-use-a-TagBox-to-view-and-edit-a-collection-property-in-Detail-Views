using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {

    [DefaultClassOptions]
    public class Department : BaseObject {

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Employee> EmployeeCollection { get; set; } = new ObservableCollection<Employee>();
    }
}
