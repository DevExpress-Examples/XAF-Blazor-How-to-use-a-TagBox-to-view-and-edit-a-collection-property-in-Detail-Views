# XAF Blazor - How to implement a TagBox Property Editor

## Scenario

There are a couple of child records, and it is required to show all available records in a compact manner, and link and unlink them from the master object quickly with check boxes. When an item is checked, this means that this record is associated with the master object.

## Solution

We created a custom XAF Property Editor based on our [DxTagBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2) component. For more information, review the following concepts:
- [Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/concepts/ui-construction/view-items/property-editors);
- [How to: Implement a Property Editor Based on a Custom Component (Blazor)](https://docs.devexpress.com/eXpressAppFramework/402189/task-based-help/property-editors/how-to-implement-a-property-editor-based-on-custom-components-blazor?p=netstandard);
- [Application Solution Components](https://docs.devexpress.com/eXpressAppFramework/112569/concepts/application-solution-components).
<img src="./media/example.png" width="600">

To simplify this task, we used our built-in Component Model (DxTagBoxModel), Component Adapter (DxTagBoxAdapter), and Component Renderer (DxTagBoxRenderer).

## Implementation Steps

Create a **BlazorPropertyEditorBase** class descendant and follow the steps listed below:

**Step 1.** Override the **CreateComponentAdapter** method. In this method, create a component model, create a DataItem<string> collection, fill it with object handles using the [IObjectSpace.GetObjectHandle](https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.IObjectSpace.GetObjectHandle%28System.Object%29) method, and then assign this collection to the component model's ***Data*** property. Then, specify the component model's ***ValueFieldName*** and ***TextFieldName*** properties. They are used to bind the ***DxTagBox*** component properly.

**Step 2.** Override the **ReadValueCore** method. This method is required to obtain a collection of values from a PropertyValue, loop through this collection, and update your component model's data.

**Step 3.** Override the **WriteValueCore** method. Use this method to obtain data from the component model and then update the collection stored in the ***PropertyValue*** property.

**Step 4.** Return **false** in the overridden **IsMemberSetterRequired** method to specify that the editor should not be read-only.

<!-- default file list -->  
*Files to look at*: 

* [TagBoxPropertyEditor.cs](./TagBoxPropertyEditorSample.Module.Blazor/Editors/TagBoxPropertyEditor.cs)
<!-- default file list end -->