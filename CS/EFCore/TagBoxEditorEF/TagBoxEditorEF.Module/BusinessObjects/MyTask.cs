using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class MyTask : BaseObject {
        public virtual string Subject { get; set; }
        public virtual IList<Employee> EmployeeCollection { get; set; } = new ObservableCollection<Employee>();
    }
}
