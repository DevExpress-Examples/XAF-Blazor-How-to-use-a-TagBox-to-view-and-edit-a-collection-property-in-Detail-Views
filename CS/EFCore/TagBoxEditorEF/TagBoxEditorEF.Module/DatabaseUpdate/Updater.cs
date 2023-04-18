using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using TagBoxPropertyEditorSample.Module.BusinessObjects;

namespace TagBoxEditorEF.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        Employee employee = ObjectSpace.FindObject<Employee>(null);
        if (employee == null) {
            employee = ObjectSpace.CreateObject<Employee>();
            employee.FullName = "Karl Jablonski";

            var department = ObjectSpace.CreateObject<Department>();
            department.Name = "Development Department";
            department.Description = "The Information Technology Department manages the company's information infrastructure and online assets.";
            employee.Department = department;

            employee = ObjectSpace.CreateObject<Employee>();
            employee.FullName = "Beverly Oneil";

            department = ObjectSpace.CreateObject<Department>();
            department.Name = "Finance and Accounting";
            department.Description = "The Finance and Accounting Department manages corporate money. The department plans, organizes, controls and accounts company finances.";
            employee.Department = department;

            var task = ObjectSpace.CreateObject<MyTask>();
            task.Subject = "Submit Customer Follow Up Plan Feedback";

            task = ObjectSpace.CreateObject<MyTask>();
            task.Subject = "Training Events";
        }
        ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
