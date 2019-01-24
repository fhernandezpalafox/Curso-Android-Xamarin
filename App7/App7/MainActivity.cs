using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;
using Android.Content;
using Com.JeevanDeshmukh.FancyBottomSheetDialogLib;
using Android.Graphics;

namespace App7
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnalerta1, btnalerta2, btnalerta3;
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

            btnalerta1 = FindViewById<Button>(Resource.Id.btnAlerta1);
            btnalerta2 = FindViewById<Button>(Resource.Id.btnAlerta2);
            btnalerta3 = FindViewById<Button>(Resource.Id.btnAlerta3);

            lblInformacion = FindViewById<TextView>(Resource.Id.lblinformacion);

        }

        public void Eventos()
        {

            btnalerta1.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetMessage("Mensaje de prueba")
                       .SetTitle("Mensaje")
                       .SetPositiveButton("Aceptar", (senderAlert, args) =>
                       {
                           lblInformacion.Text = "Respuesta de nuestra alerta";
                       });
                builder.Create().Show();
            };

            btnalerta2.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetMessage("Mensaje de prueba")
                       .SetTitle("Mensaje")
                       .SetPositiveButton("Aceptar", (senderAlert, args) =>
                       {
                           lblInformacion.Text = "Respuesta positiva";
                       })
                       .SetNegativeButton("Cancelar", (senderAlert, args) => {
                           lblInformacion.Text = "Respuesta negativa";
                       });
                builder.Create().Show();
            };

            btnalerta3.Click += delegate
            {
                 string[] datos = { "Felipe","Oscar","Juan"};

                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Selecciona");
                builder.SetItems(datos, (sender, args) => {

                    lblInformacion.Text = string.Format("Tu nombre es {0}", datos[args.Which]);

                });
                builder.Create().Show();


                AlertDialog.Builder builder2 = new AlertDialog.Builder(this);
                builder2.SetTitle("Selecciona")
                        .SetSingleChoiceItems(datos, -1, (sender, args) => {
                            lblInformacion.Text = string.Format("Tu nombre es {0}", datos[args.Which]);
                        });
                builder2.Create().Show();


                AlertDialog.Builder builder3 = new AlertDialog.Builder(this);
                builder3.SetTitle("Selecciona")
                .SetMultiChoiceItems(datos,null, (sender, args) => {

                    if (args.IsChecked) {
                        string informacion = lblInformacion.Text;
                       lblInformacion.Text = string.Format("{0}  {1}",informacion, datos[args.Which]);
                    }
                    
                });
                builder3.Create().Show();

                MsgLibreria();



            };

        }

        public void MsgLibreria() {

            new FancyBottomSheetDialog.Builder(this)
               .SetTitle("Alert bottom sheet dialog")
               .SetMessage("This is where we show the information.This is a message.This is where we show message explain or showing the information.")
               .SetBackgroundColor(Color.Orange) //don't use R.color.somecolor
               .SetIcon(Resource.Drawable.ic_info_outline_black_24dp, true)
               .IsCancellable(false)
               .OnNegativeClicked(() => {
                   Toast.MakeText(ApplicationContext, "Nego", ToastLength.Long).Show();
               })
               .OnPositiveClicked(() => {
                   Toast.MakeText(ApplicationContext, "Acepto", ToastLength.Long).Show();
               })
                .SetNegativeBtnText("Cancel")
                .SetPositiveBtnText("Ok")
                .SetPositiveBtnBackground(Color.ParseColor("#3F51B5"))//don't use R.color.somecolor
                .SetNegativeBtnBackground(Color.White)//don't use R.color.somecolor
                .Build();
}

    }
}