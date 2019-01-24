using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace App6
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText txtNombre;
        private CheckBox chkPrimaria, chkSecundaria, chkPreparatoria;
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

        public void Inicializar()
        {

            txtNombre = FindViewById<EditText>(Resource.Id.txtNombre);

            chkPrimaria = FindViewById<CheckBox>(Resource.Id.chkPrimaria);
            chkSecundaria = FindViewById<CheckBox>(Resource.Id.chkSecundaria);
            chkPreparatoria = FindViewById<CheckBox>(Resource.Id.chkPreparatoria);

            btnAceptar = FindViewById<Button>(Resource.Id.btnAceptar);

            lblInformacion = FindViewById<TextView>(Resource.Id.lblInformacion);

        }

        public void Eventos()
        {

            btnAceptar.Click += delegate
            {

                string informacion = "";
                string escolaridades = "";

                if (chkPrimaria.Checked)
                {
                    escolaridades += "Primaria ";
                }
                if (chkSecundaria.Checked)
                {
                    escolaridades += "Secundaria ";
                }
                if (chkPreparatoria.Checked)
                {
                    escolaridades += "Preparatoria";
                }

                informacion = $"Tu nombre es {txtNombre.Text} las escolaridades selecionadas fueron las siguientes {escolaridades}";
                
                lblInformacion.Text = informacion;
            };
        }
    }
}