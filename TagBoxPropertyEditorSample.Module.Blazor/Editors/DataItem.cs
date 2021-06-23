namespace TagBoxPropertyEditorSample.Module.Blazor.Editors {
    public class DataItem<T> {
        public DataItem(T value, string text) {
            Value = value;
            Text = text;
        }
        public T Value { get; }
        public string Text { get; }
    }
}
