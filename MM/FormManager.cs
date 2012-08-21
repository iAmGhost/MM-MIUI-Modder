using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM
{
    class FormManager : Singleton<FormManager>
    {
        private Form_Main mainForm = null;

        public void SetMainForm(Form_Main form)
        {
            mainForm = form;
        }

        public Form_Main GetMainForm()
        {
            return mainForm;
        }
    }
}
