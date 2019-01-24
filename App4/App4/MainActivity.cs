using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace App4
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/AppTheme", 
        MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText txtCampo1, txtCampo2, txtCampo3;
        private Button btnAceptar;
        private TextView lblInformacion;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Inicializar();
            Eventos();
        }

        public void Inicializar() {

            txtCampo1 = FindViewById<EditText>(Resource.Id.txtCampo1);
            txtCampo2 = FindViewById<EditText>(Resource.Id.txtCampo2);
            txtCampo3 = FindViewById<EditText>(Resource.Id.txtCampo3);

            btnAceptar = FindViewById<Button>(Resource.Id.btnAceptar);

            lblInformacion = FindViewById<TextView>(Resource.Id.lblInformacion);
        }

        public void Eventos() {

            btnAceptar.Click += BtnAceptar_Click;
        }

        private void BtnAceptar_Click(object sender, System.EventArgs e)
        {
            string informacion;

            informacion = $"El campo 1 fue capturado {txtCampo1.Text}  y despues el campo de password fue capturado {txtCampo2.Text}  y el tercer campo fue capturado {txtCampo3.Text}";

            lblInformacion.Text = informacion;
        }
    }
}