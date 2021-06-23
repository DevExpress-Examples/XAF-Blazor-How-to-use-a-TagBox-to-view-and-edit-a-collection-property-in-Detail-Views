namespace TagBoxPropertyEditorSample.Blazor.Server {
    partial class TagBoxPropertyEditorSampleBlazorApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule();
            this.module3 = new TagBoxPropertyEditorSample.Module.TagBoxPropertyEditorSampleModule();
            this.module4 = new TagBoxPropertyEditorSample.Module.Blazor.TagBoxPropertyEditorSampleBlazorModule();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            // 
            // TagBoxPropertyEditorSampleBlazorApplication
            // 
            this.ApplicationName = "TagBoxPropertyEditorSample";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TagBoxPropertyEditorSampleBlazorApplication_DatabaseVersionMismatch);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Blazor.SystemModule.SystemBlazorModule module2;
        private TagBoxPropertyEditorSample.Module.TagBoxPropertyEditorSampleModule module3;
        private TagBoxPropertyEditorSample.Module.Blazor.TagBoxPropertyEditorSampleBlazorModule module4;
    }
}
