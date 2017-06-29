using System.Collections.Generic;

using Android.App;
using Android.OS;
using HackAtHome.Entities;

namespace HackAtHome.Client
{
    public class Cache : Fragment
    {
        //public string Fullname { get; set; }
        //public string Token { get; set; }
        public List<Evidence> Evidences { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RetainInstance = true;
        }
    }
}