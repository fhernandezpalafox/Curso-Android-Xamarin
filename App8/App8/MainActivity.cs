using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;

namespace App8
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextInputEditText txtNombre;
        private Spinner listaEdoCivil;
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

            txtNombre = FindViewById<TextInputEditText>(Resource.Id.txtNombre);

            listaEdoCivil = FindViewById<Spinner>(Resource.Id.ListaEdoCivil);

            btnAceptar = FindViewById<Button>(Resource.Id.btnAceptar);

            lblInformacion = FindViewById<TextView>(Resource.Id.lblInformacion);

            //Llenar Spinner
            string[] edocivil = { "Soltero", "Casado", "Divorciado" };
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,Resource.Layout.ejemplo_spinner, //android.R.layout.simple_spinner_item
                                                             edocivil);
            listaEdoCivil.Adapter = adapter;


        }

        public void Eventos() {

            btnAceptar.Click += delegate
            {
                string informacion = "";

                informacion = string.Format("Tu nombre es {0} y tu estado civil es {1}", txtNombre.Text,
                    listaEdoCivil.SelectedItem.ToString());

                lblInformacion.Text = informacion;
            };

            listaEdoCivil.ItemSelected += (sender, args) =>
            {

                string informacion = "";

                informacion = string.Format("Tu nombre es {0} y tu estado civil es {1}", txtNombre.Text,
                                                                                           args.Parent.GetItemAtPosition(args.Position));

                lblInformacion.Text = informacion;

            };
            

        }
    }
}