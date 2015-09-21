using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using TinyTranslatorGui.Controllers;

namespace TinyTranslatorGui
{
    public partial class MainForm : RibbonForm
    {

        private ProjectSelectController projectSelectController;
        private BundleListController bundleListController;
        private TranslationsGridController translationsGridController;

        public MainForm()
        {
            InitializeComponent();

            SkinHelper.InitSkinGallery(rgbiSkins, true);

            projectSelectController = new ProjectSelectController(barEditItemProject);
            bundleListController = new BundleListController(treeListBundles);
            translationsGridController = new TranslationsGridController(gridControlTranslations);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            projectSelectController.LoadProjects();
        }
        

    }
}