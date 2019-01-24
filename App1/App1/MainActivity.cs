using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/AppTheme.NoActionBar",
        MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        TextView lblinformacion;
        TextInputEditText txtNombre, txtApellidos;
        Button btnAceptar;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab2);
            fab.Click += FabOnClick;

            Inicializar();
            Eventos();
        }

        
        public void Inicializar() {

            lblinformacion = FindViewById<TextView>(Resource.Id.lblInformacion);

            txtNombre = FindViewById<TextInputEditText>(Resource.Id.txtNombre);

            txtApellidos = FindViewById<TextInputEditText>(Resource.Id.txtApellido);

            btnAceptar = FindViewById<Button>(Resource.Id.btnAceptar);
        }

        
        public void Eventos(){

            btnAceptar.Click += delegate
            {
                lblinformacion.Text = $"{txtNombre.Text} {txtApellidos.Text}";
            };

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

