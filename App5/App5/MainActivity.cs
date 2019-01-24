using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;

namespace App5
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnAceptar;
        private TextView lblInformacion;
        private TextInputEditText txtCampo1, txtCampo2;

        private RadioButton rd1, rd2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Inicializar();
            Eventos();
        }

        public void Inicializar()
        {

            btnAceptar = FindViewById<Button>(Resource.Id.btnAceptar);

            lblInformacion = FindViewById<TextView>(Resource.Id.lblInformacion);

            txtCampo1 = FindViewById<TextInputEditText>(Resource.Id.txtCampo1);
            txtCampo2 = FindViewById<TextInputEditText>(Resource.Id.txtCampo2);

            rd1 = FindViewById<RadioButton>(Resource.Id.radioSoltero);
            rd2 = FindViewById<RadioButton>(Resource.Id.radioCasado);

        }

        public void Eventos()
        {

            btnAceptar.Click += delegate {

                string informacion;
                string edocivil = "";

                if (rd1.Checked) {
                    edocivil = "Soltero";
                }
                else if (rd2.Checked) {
                    edocivil = "Casado";
                }

                informacion = $" Tu nombre es {txtCampo1.Text}  y tu apellido es {txtCampo2.Text} y tu estado civil es {edocivil}";


                lblInformacion.Text = informacion;

            };
            

    }
}
}