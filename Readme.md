# XAF Blazor - How to implement a TagBox Property Editor

## Scenario

There are a couple of child records, and it is required to show all available records in a compact manner, and link and unlink them from the master object quickly with check boxes. When an item is checked, this means that this record is associated with the master object.

## Solution

We created a custom XAF Property Editor based on our [DxTagBox]() component. For more information, review the following concepts:
- [Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/concepts/ui-construction/view-items/property-editors);
- [How to: Implement a Property Editor Based on a Custom Component (Blazor)](https://docs.devexpress.com/eXpressAppFramework/402189/task-based-help/property-editors/how-to-implement-a-property-editor-based-on-custom-components-blazor?p=netstandard);
- [Application Solution Components](https://docs.devexpress.com/eXpressAppFramework/112569/concepts/application-solution-components).
<img src="./media/example.png" width="600">

## Implementation Steps

**Step 1.** Create a ComponentModelBase descendant and name it “TagBoxModel”. In this class, declare properties that describe your component and its interaction with a user.

```cs
using System.Collections.Generic;
using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
//...
public abstract class TagBoxModel : ComponentModelBase {
	public bool ReadOnly {
		get => GetPropertyValue<bool>();
		set => SetPropertyValue(value);
	}
	public string NullText {
		get => GetPropertyValue<string>();
		set => SetPropertyValue(value);
	}
	public DataEditorClearButtonDisplayMode ClearButtonDisplayMode {
		get => GetPropertyValue<DataEditorClearButtonDisplayMode>(DataEditorClearButtonDisplayMode.Never);
		set => SetPropertyValue(value);
	}
	public string ValueFieldName {
		get => GetPropertyValue<string>();
		set => SetPropertyValue(value);
	}
	public string TextFieldName {
		get => GetPropertyValue<string>();
		set => SetPropertyValue(value);
	}
}
public class TagBoxModel<TData, TValue> : TagBoxModel {
	public IEnumerable<TValue> Values {
		get => GetPropertyValue<IEnumerable<TValue>>();
		set => SetPropertyValue(value);
	}
	public EventCallback<IEnumerable<TValue>> ValuesChanged {
		get => GetPropertyValue<EventCallback<IEnumerable<TValue>>>();
		set => SetPropertyValue(value);
	}
	public IEnumerable<TData> Data {
		get => GetPropertyValue<IEnumerable<TData>>();
		set => SetPropertyValue(value);
	}
}
```

**Step 2.** Create a new Razor component﻿ and name it “TagBoxRenderer”. Declare the ComponentModel component parameter that binds the DxTagBox component with its model. Map the element parameters to the model properties and add the Create method that creates RenderFragment﻿.

```cs
@using Microsoft.AspNetCore.Components.Web
@using DevExpress.Blazor
@typeparam TData
@typeparam TValue

<DxTagBox Data="@ComponentModel.Data" 
          Values="ComponentModel.Values" 
          ValueFieldName="@ComponentModel.ValueFieldName"
          TextFieldName="@ComponentModel.TextFieldName"
          NullText="@ComponentModel.NullText"
          ReadOnly="@ComponentModel.ReadOnly"
          @attributes="@ComponentModel.Attributes"
          ValuesChanged="@ComponentModel.ValuesChanged"
          ClearButtonDisplayMode="@ComponentModel.ClearButtonDisplayMode"></DxTagBox>

@code {
    [Parameter]
    public TagBoxModel<TData, TValue> ComponentModel { get; set; }
    public static RenderFragment Create(TagBoxModel<TData, TValue> componentModel) =>@<TagBoxRenderer ComponentModel=@componentModel />;
}
```

**Step 3.** Create a ComponentAdapterBase descendant and name it “TagBoxAdapter”. Override its methods to implement Property Editor logic for your component model.

```cs
using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;
//...
public abstract class TagBoxAdapter : ComponentAdapterBase {
	public abstract TagBoxModel ComponentModel { get; }
}
public class TagBoxAdapter<TData, TValue> : TagBoxAdapter {
	public TagBoxAdapter(TagBoxModel<TData, TValue> componentModel) {
		if(componentModel is null) {
			throw new ArgumentNullException(nameof(componentModel));
		}
		ComponentModel = componentModel;
		ComponentModel.ValuesChanged = EventCallback.Factory.Create<IEnumerable<TValue>>(this, ComponentModel_ValuesChanged);
	}
	private void ComponentModel_ValuesChanged(IEnumerable<TValue> values) {
		ComponentModel.Values = values;
		RaiseValueChanged();
	}
	public override TagBoxModel<TData, TValue> ComponentModel { get; }
	public override object GetValue() => ComponentModel.Values;
	public override void SetAllowEdit(bool allowEdit) => ComponentModel.ReadOnly = !allowEdit;
	public override void SetAllowNull(bool allowNull) => ComponentModel.ClearButtonDisplayMode = allowNull ? DevExpress.Blazor.DataEditorClearButtonDisplayMode.Auto : DevExpress.Blazor.DataEditorClearButtonDisplayMode.Never;
	public override void SetDisplayFormat(string displayFormat) { }
	public override void SetEditMask(string editMask) { }
	public override void SetEditMaskType(EditMaskType editMaskType) { }
	public override void SetErrorIcon(ImageInfo errorIcon) { }
	public override void SetErrorMessage(string errorMessage) { }
	public override void SetIsPassword(bool isPassword) { }
	public override void SetMaxLength(int maxLength) { }
	public override void SetNullText(string nullText) => ComponentModel.NullText = nullText;
	public override void SetValue(object value) => ComponentModel.Values = (IEnumerable<TValue>)value;
	protected override RenderFragment CreateComponent() => ComponentModelObserver.Create(ComponentModel, TagBoxRenderer<TData, TValue>.Create(ComponentModel));
}
```

**Step 4.** Create a **BlazorPropertyEditorBase** descendant and name it “TagBoxPropertyEditor”. Apply PropertyEditorAttribute to it and set the first attribute parameter to **IList**, the second one to an aliased name, and the third one to false. With these values, you can choose this Property Editor in the Model Editor for any collection property, and this editor is not marked as default. Then, override the **ReadValueCore** and **WriteValueCore** methods to synchornize selected tags with your business object data.

```cs
using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
//...
[PropertyEditor(typeof(IList), nameof(TagBoxPropertyEditor), false)]
public class TagBoxPropertyEditor : BlazorPropertyEditorBase {
	public TagBoxPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
	protected override bool IsMemberSetterRequired() => false;
	protected override IComponentAdapter CreateComponentAdapter() {
		TagBoxModel<DataItem<string>, string> componentModel = new TagBoxModel<DataItem<string>, string>();
		List<DataItem<string>> data = new List<DataItem<string>>();
		IObjectSpace objectSpace = View.ObjectSpace;
		ITypeInfo itemTypeInfo = MemberInfo.ListElementTypeInfo;
		foreach(object item in objectSpace.GetObjects(itemTypeInfo.Type)) {
			string objectHandle = objectSpace.GetObjectHandle(item);
			string displayText = itemTypeInfo.DefaultMember.GetValue(item)?.ToString();
			data.Add(new DataItem<string>(objectHandle, displayText));
		}
		componentModel.Data = data;
		componentModel.ValueFieldName = nameof(DataItem<string>.Value);
		componentModel.TextFieldName = nameof(DataItem<string>.Text);
		return new TagBoxAdapter<DataItem<string>, string>(componentModel);
	}
	protected override void ReadValueCore() {
		if(Control is TagBoxAdapter<DataItem<string>, string> adapter && PropertyValue is IList propertyList) {
			List<string> values = new List<string>();
			IObjectSpace objectSpace = View.ObjectSpace;
			foreach(object obj in propertyList) {
				string objectHandle = objectSpace.GetObjectHandle(obj);
				values.Add(objectHandle);
			}
			adapter.ComponentModel.Values = values;
		}
	}
	protected override void WriteValueCore() {
		if(Control is TagBoxAdapter<DataItem<string>, string> adapter && PropertyValue is IList propertyList) {
			IObjectSpace objectSpace = View.ObjectSpace;
			List<object> actualObjects = new List<object>();
			if(adapter.ComponentModel.Values != null) {
				foreach(string objectHandle in adapter.ComponentModel.Values) {
					actualObjects.Add(objectSpace.GetObjectByHandle(objectHandle));
				}
			}
			List<object> objectsToLink = new List<object>(actualObjects);
			List<object> objectsToUnlink = new List<object>();
			foreach(object obj in propertyList) {
				objectsToLink.Remove(obj);
				if(!actualObjects.Contains(obj)) {
					objectsToUnlink.Add(obj);
				}
			}
			foreach(object obj in objectsToUnlink) {
				propertyList.Remove(obj);
			}
			foreach(object obj in objectsToLink) {
				propertyList.Add(obj);
			}
			objectSpace.SetModified(CurrentObject);
		}
	}
}
```

We declared the auxiliary "DataItem" class for data binding purposes:

```cs
public class DataItem<T> {
	public DataItem(T value, string text) {
		Value = value;
		Text = text;
	}
	public T Value { get; }
	public string Text { get; }
}
```

<!-- default file list -->  
*Files to look at*:

* [TagBoxModel.cs](./TagBoxPropertyEditorSample.Module.Blazor/Editors/TagBoxModel.cs)
* [TagBoxRenderer.razor](./TagBoxPropertyEditorSample.Module.Blazor/Editors/TagBoxRenderer.razor)
* [TagBoxAdapter.cs](./TagBoxPropertyEditorSample.Module.Blazor/Editors/TagBoxAdapter.cs)
* [TagBoxPropertyEditor.cs](./TagBoxPropertyEditorSample.Module.Blazor/Editors/TagBoxPropertyEditor.cs)
* [DataItem.cs](./TagBoxPropertyEditorSample.Module.Blazor/Editors/DataItem.cs)
<!-- default file list end -->