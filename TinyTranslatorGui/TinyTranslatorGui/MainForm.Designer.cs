namespace TinyTranslatorGui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeListBundles = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridControlTranslations = new DevExpress.XtraGrid.GridControl();
            this.gridViewTranslations = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemExit = new DevExpress.XtraBars.BarButtonItem();
            this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.homeRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.skinsRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.exitRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.importExportRibbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupImportTranslations = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItemExportCsv = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImportCsv = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemImportAssembly = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemProject = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxProject = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.barButtonItemStatistics = new DevExpress.XtraBars.BarButtonItem();
            this.treeListColumnStatus = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListBundles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTranslations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTranslations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProject)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 143);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Padding = new System.Windows.Forms.Padding(6);
            this.splitContainerControl.Panel1.Controls.Add(this.treeListBundles);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.gridControlTranslations);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(1100, 526);
            this.splitContainerControl.SplitterPosition = 300;
            this.splitContainerControl.TabIndex = 0;
            this.splitContainerControl.Text = "splitContainerControl1";
            // 
            // treeListBundles
            // 
            this.treeListBundles.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnStatus});
            this.treeListBundles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListBundles.Location = new System.Drawing.Point(0, 0);
            this.treeListBundles.Name = "treeListBundles";
            this.treeListBundles.BeginUnboundLoad();
            this.treeListBundles.AppendNode(new object[] {
            "Assembly",
            "red"}, -1);
            this.treeListBundles.AppendNode(new object[] {
            "Bundle",
            "green"}, 0);
            this.treeListBundles.EndUnboundLoad();
            this.treeListBundles.OptionsBehavior.Editable = false;
            this.treeListBundles.OptionsView.ShowIndicator = false;
            this.treeListBundles.Size = new System.Drawing.Size(300, 514);
            this.treeListBundles.TabIndex = 0;
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "Name";
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            this.treeListColumnName.Width = 92;
            // 
            // gridControlTranslations
            // 
            this.gridControlTranslations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTranslations.Location = new System.Drawing.Point(0, 0);
            this.gridControlTranslations.MainView = this.gridViewTranslations;
            this.gridControlTranslations.Name = "gridControlTranslations";
            this.gridControlTranslations.Size = new System.Drawing.Size(783, 514);
            this.gridControlTranslations.TabIndex = 0;
            this.gridControlTranslations.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTranslations});
            // 
            // gridViewTranslations
            // 
            this.gridViewTranslations.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridViewTranslations.GridControl = this.gridControlTranslations;
            this.gridViewTranslations.Name = "gridViewTranslations";
            this.gridViewTranslations.OptionsView.ShowGroupPanel = false;
            this.gridViewTranslations.OptionsView.ShowIndicator = false;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.barButtonItemExit,
            this.rgbiSkins,
            this.barButtonItemExportCsv,
            this.barButtonItemImportCsv,
            this.barButtonItemImportAssembly,
            this.barEditItemProject,
            this.barButtonItemStatistics});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 2;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageHeaderItemLinks.Add(this.barEditItemProject, true);
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.homeRibbonPage,
            this.importExportRibbonPage});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxProject});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(1100, 143);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barButtonItemExit
            // 
            this.barButtonItemExit.Caption = "Exit";
            this.barButtonItemExit.Description = "Closes this program after prompting you to save unsaved data.";
            this.barButtonItemExit.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExit.Glyph")));
            this.barButtonItemExit.Hint = "Closes this program after prompting you to save unsaved data";
            this.barButtonItemExit.Id = 20;
            this.barButtonItemExit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExit.LargeGlyph")));
            this.barButtonItemExit.Name = "barButtonItemExit";
            // 
            // rgbiSkins
            // 
            this.rgbiSkins.Caption = "Skins";
            // 
            // 
            // 
            this.rgbiSkins.Gallery.AllowHoverImages = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseFont = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
            this.rgbiSkins.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.rgbiSkins.Gallery.ColumnCount = 4;
            this.rgbiSkins.Gallery.FixedHoverImageSize = false;
            this.rgbiSkins.Gallery.ImageSize = new System.Drawing.Size(32, 17);
            this.rgbiSkins.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Top;
            this.rgbiSkins.Gallery.RowCount = 4;
            this.rgbiSkins.Id = 60;
            this.rgbiSkins.Name = "rgbiSkins";
            // 
            // homeRibbonPage
            // 
            this.homeRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3,
            this.skinsRibbonPageGroup,
            this.exitRibbonPageGroup});
            this.homeRibbonPage.Name = "homeRibbonPage";
            this.homeRibbonPage.Text = "Home";
            // 
            // skinsRibbonPageGroup
            // 
            this.skinsRibbonPageGroup.ItemLinks.Add(this.rgbiSkins);
            this.skinsRibbonPageGroup.Name = "skinsRibbonPageGroup";
            this.skinsRibbonPageGroup.ShowCaptionButton = false;
            this.skinsRibbonPageGroup.Text = "Skins";
            // 
            // exitRibbonPageGroup
            // 
            this.exitRibbonPageGroup.ItemLinks.Add(this.barButtonItemExit);
            this.exitRibbonPageGroup.Name = "exitRibbonPageGroup";
            this.exitRibbonPageGroup.Text = "Exit";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 669);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1100, 31);
            // 
            // importExportRibbonPage
            // 
            this.importExportRibbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupImportTranslations,
            this.ribbonPageGroup2});
            this.importExportRibbonPage.Name = "importExportRibbonPage";
            this.importExportRibbonPage.Text = "Import/Export";
            // 
            // ribbonPageGroupImportTranslations
            // 
            this.ribbonPageGroupImportTranslations.ItemLinks.Add(this.barButtonItemImportCsv);
            this.ribbonPageGroupImportTranslations.ItemLinks.Add(this.barButtonItemImportAssembly);
            this.ribbonPageGroupImportTranslations.Name = "ribbonPageGroupImportTranslations";
            this.ribbonPageGroupImportTranslations.Text = "Import translations";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemExportCsv);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Export translations";
            // 
            // barButtonItemExportCsv
            // 
            this.barButtonItemExportCsv.Caption = "CSV format";
            this.barButtonItemExportCsv.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExportCsv.Glyph")));
            this.barButtonItemExportCsv.Id = 62;
            this.barButtonItemExportCsv.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemExportCsv.LargeGlyph")));
            this.barButtonItemExportCsv.Name = "barButtonItemExportCsv";
            // 
            // barButtonItemImportCsv
            // 
            this.barButtonItemImportCsv.Caption = "CSV format";
            this.barButtonItemImportCsv.Id = 63;
            this.barButtonItemImportCsv.Name = "barButtonItemImportCsv";
            // 
            // barButtonItemImportAssembly
            // 
            this.barButtonItemImportAssembly.Caption = ".NET assembly";
            this.barButtonItemImportAssembly.Id = 64;
            this.barButtonItemImportAssembly.Name = "barButtonItemImportAssembly";
            // 
            // barEditItemProject
            // 
            this.barEditItemProject.Caption = "Project";
            this.barEditItemProject.Edit = this.repositoryItemComboBoxProject;
            this.barEditItemProject.Id = 66;
            this.barEditItemProject.Name = "barEditItemProject";
            this.barEditItemProject.Width = 150;
            // 
            // repositoryItemComboBoxProject
            // 
            this.repositoryItemComboBoxProject.AutoHeight = false;
            this.repositoryItemComboBoxProject.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxProject.Name = "repositoryItemComboBoxProject";
            this.repositoryItemComboBoxProject.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItemStatistics);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Statistics";
            // 
            // barButtonItemStatistics
            // 
            this.barButtonItemStatistics.Caption = "Statistics";
            this.barButtonItemStatistics.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemStatistics.Glyph")));
            this.barButtonItemStatistics.Id = 1;
            this.barButtonItemStatistics.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItemStatistics.LargeGlyph")));
            this.barButtonItemStatistics.Name = "barButtonItemStatistics";
            // 
            // treeListColumnStatus
            // 
            this.treeListColumnStatus.Caption = "Status";
            this.treeListColumnStatus.FieldName = "Status";
            this.treeListColumnStatus.Name = "treeListColumnStatus";
            this.treeListColumnStatus.Visible = true;
            this.treeListColumnStatus.VisibleIndex = 1;
            // 
            // MainForm
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.splitContainerControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "TinyTranslator";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListBundles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTranslations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTranslations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExit;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.Ribbon.RibbonPage homeRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup skinsRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup exitRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraTreeList.TreeList treeListBundles;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraGrid.GridControl gridControlTranslations;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTranslations;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExportCsv;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImportCsv;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImportAssembly;
        private DevExpress.XtraBars.Ribbon.RibbonPage importExportRibbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupImportTranslations;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarEditItem barEditItemProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxProject;
        private DevExpress.XtraBars.BarButtonItem barButtonItemStatistics;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnStatus;

    }
}
