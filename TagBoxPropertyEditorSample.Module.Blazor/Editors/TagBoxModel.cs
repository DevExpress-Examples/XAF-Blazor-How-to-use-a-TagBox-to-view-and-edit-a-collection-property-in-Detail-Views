using System.Collections.Generic;
using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;

namespace TagBoxPropertyEditorSample.Module.Blazor.Editors {
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
}
