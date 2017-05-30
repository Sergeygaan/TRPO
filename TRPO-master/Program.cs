using ActivForm;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace MyPaint {
	static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            MainForm MainForm = new MainForm();

            UnityContainer UnityContainerInit = new UnityContainer();

            InitializationData InitializatioForm = UnityContainerInit.Resolve<InitializationData>(new OrderedParametersOverride(new object[] { MainForm.Width, MainForm.Height }));

            MainForm.InitializatioForm = InitializatioForm;

            Application.Run(MainForm);
        }
    }
}
