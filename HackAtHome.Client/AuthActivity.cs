using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.SAL;


namespace HackAtHome.Client
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/HackAtHome")]
    public class AuthActivity : Activity
    {

        EditText etEmail;
        EditText etPassword;
        Button bValidate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Auth);

            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            bValidate = FindViewById<Button>(Resource.Id.bValidate);
            
            bValidate.Click += async (s, e) => {
                var serviceClient = new ServiceClient();
                var email = etEmail.Text;
                var password = etPassword.Text;
                var result = await serviceClient.AutenticateAsync(email, password);
                if (result.Status == Entities.Status.Success)
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    intent.PutExtra("token", result.Token);
                    intent.PutExtra("fullname", result.FullName);

                    StartActivity(intent);
                }
            };
        }
    }
}