using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EsHeichSample.Forms.Theme
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class _Templates : ResourceDictionary
    {
        public _Templates()
        {
            InitializeComponent();
        }
    }
}