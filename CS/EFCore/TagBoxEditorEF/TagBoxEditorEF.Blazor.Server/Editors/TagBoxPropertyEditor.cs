using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using System.Collections;

namespace TagBoxEditorEF.Blazor.Server.Editors;

[PropertyEditor(typeof(IEnumerable), nameof(TagBoxPropertyEditor), false)]
public class TagBoxPropertyEditor : BlazorPropertyEditorBase, IComplexViewItem {
    private IObjectSpace objectSpace;
    public TagBoxPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
    public override DxTagBoxModel<DataItem<string>, string> ComponentModel => (DxTagBoxModel<DataItem<string>, string>)base.ComponentModel;
    protected override bool IsMemberSetterRequired() => false;
    protected override IComponentModel CreateComponentModel() {
        var componentModel = new DxTagBoxModel<DataItem<string>, string>();
        componentModel.Data = GetData();
        componentModel.ValueFieldName = nameof(DataItem<string>.Value);
        componentModel.TextFieldName = nameof(DataItem<string>.Text);
        return componentModel;
    }
    protected override void OnCurrentObjectChanged() {
        if(ComponentModel is not null) {
            ComponentModel.Data = GetData();
        }
        base.OnCurrentObjectChanged();
    }
    private List<DataItem<string>> GetData() {
        var itemTypeInfo = MemberInfo.ListElementTypeInfo;
        var items = objectSpace.GetObjects(itemTypeInfo.Type);
        var data = new List<DataItem<string>>();
        foreach(var item in items) {
            string objectHandle = objectSpace.GetObjectHandle(item);
            string displayText = itemTypeInfo.DefaultMember.GetValue(item)?.ToString();
            data.Add(new DataItem<string>(objectHandle, displayText));
        }
        return data;
    }
    protected override void ReadValueCore() {
        if(PropertyValue is not IList propertyList) {
            return;
        }
        List<string> values = new List<string>();
        foreach(var obj in propertyList) {
            string objectHandle = objectSpace.GetObjectHandle(obj);
            values.Add(objectHandle);
        }
        ComponentModel.Values = values;
    }
    protected override void WriteValueCore() {
        if(PropertyValue is not IList propertyList) {
            return;
        }
        var actualObjects = ComponentModel.Values?.Select(objectSpace.GetObjectByHandle);
        var objectsToLink = new HashSet<object>(actualObjects);
        var objectsToUnlink = new HashSet<object>();
        foreach(var obj in propertyList) {
            objectsToLink.Remove(obj);
            if(!actualObjects.Contains(obj)) {
                objectsToUnlink.Add(obj);
            }
        }
        foreach(var obj in objectsToUnlink) {
            propertyList.Remove(obj);
        }
        foreach(var obj in objectsToLink) {
            propertyList.Add(obj);
        }
        objectSpace.SetModified(CurrentObject);
    }
    public void Setup(IObjectSpace objectSpace, XafApplication application) {
        this.objectSpace = objectSpace;
    }
}
