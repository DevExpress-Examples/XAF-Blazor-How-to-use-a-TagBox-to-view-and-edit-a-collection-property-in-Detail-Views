using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace TagBoxPropertyEditorSample.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Employee : BaseObject {
   

        public virtual string FullName { get; set; }

        public virtual Department Department { get; set; }
        public virtual IList<MyTask> MyTaskCollection { get; set; } = new ObservableCollection<MyTask>();

      
    }
}
