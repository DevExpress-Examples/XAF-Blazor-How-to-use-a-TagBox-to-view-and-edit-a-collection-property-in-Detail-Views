using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;

namespace TagBoxPropertyEditorSample.Module.Blazor.Editors {
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
}
