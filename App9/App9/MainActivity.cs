using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Support.Design.Widget;

namespace App9
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private Button btn1toast, btn2toast, btn3toast, btnsnackbar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);



            btn1toast = FindViewById<Button>(Resource.Id.btnToast1);
            btn2toast = FindViewById<Button>(Resource.Id.btnToast2);

            btn3toast = FindViewById<Button>(Resource.Id.btnToast3);

            btnsnackbar = FindViewById<Button>(Resource.Id.btnsnackbar);


            btn1toast.Click += delegate
            {
                Toast toast1 = Toast.MakeText(this,
                        "Este es un toast",ToastLength.Long);
                toast1.Show();
            };

            btn2toast.Click += delegate
            {
                Toast toast1 = Toast.MakeText(this,
                       "Este es un toast", ToastLength.Long);
                toast1.SetGravity(Android.Views.GravityFlags.Center | Android.Views.GravityFlags.Top,0,0);
                toast1.Show();
            };

            btn3toast.Click += delegate
            {

                Toast toast3 = new Toast(this);

                LayoutInflater inflater = this.LayoutInflater;

                View view = inflater.Inflate(Resource.Layout.layout_toast, null);

                TextView txtMensaje = view.FindViewById<TextView>(Resource.Id.Mensaje);

                txtMensaje.Text = "esta es un mensaje de un toast dinamico";

                toast3.Duration  =  ToastLength.Long;

                toast3.View = view;

                toast3.Show();

            };

            btnsnackbar.Click += Btnsnackbar_Click; 

        }

        private void Btnsnackbar_Click(object sender, System.EventArgs e)
        {

            //Snackbar.Make(btnsnackbar,"Este es un mensaje de prueba",Snackbar.LengthLong).show();

            Snackbar.Make(btnsnackbar, "Este es un mensaje de prueba", Snackbar.LengthLong)
                    .SetActionTextColor(Android.Graphics.Color.Orange)
                    .SetAction("Aceptar", (s) => {
                        Toast toast1 = Toast.MakeText(ApplicationContext,"Este es un toast", ToastLength.Long);
                              toast1.Show();
                    }).Show();

        }
    }
 
}