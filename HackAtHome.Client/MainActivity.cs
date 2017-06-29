using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.SAL;
using Android.Content;
using HackAtHome.CustomAdapters;
using Android.Util;

namespace HackAtHome.Client
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/HackAtHome")]
    public class MainActivity : Activity
    {
        ListView lvEvidences;
        TextView tvFullname;

        string fullname;
        string token;

        Cache cache;

        EvidencesAdapter evidencesAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            tvFullname = FindViewById<TextView>(Resource.Id.tvFullname);
            lvEvidences = FindViewById<ListView>(Resource.Id.lvEvidences);

            fullname = Intent.GetStringExtra("fullname");
            token = Intent.GetStringExtra("token");

            cache = (Cache)FragmentManager.FindFragmentByTag("Cache");
            if (cache == null)
            {
                cache = new Cache();
                var fragmentTransacion = FragmentManager.BeginTransaction();
                fragmentTransacion.Add(cache, "Cache");
                fragmentTransacion.Commit();
            }

            if (cache.Evidences == null)
            {
                SetData();
            }
            else
            {
                evidencesAdapter = new EvidencesAdapter(
                    this,
                    cache.Evidences,
                    Resource.Layout.EvidenceItem,
                    Resource.Id.tvEvidenceTitle,
                    Resource.Id.tvEvidenceStatus);

                lvEvidences.Adapter = evidencesAdapter;
            }
            tvFullname.Text = fullname;
            lvEvidences.ItemClick += LvEvidences_ItemClick;
        }

        private void LvEvidences_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var evidence = evidencesAdapter[e.Position];

            var intent = new Intent(this, typeof(EvidenceDetailActivity));
            intent.PutExtra("token", token);
            intent.PutExtra("evidenceId", evidence.EvidenceID);
            intent.PutExtra("fullname", fullname);
            intent.PutExtra("title", evidence.Title);
            intent.PutExtra("status", evidence.Status);
            
            StartActivity(intent);
        }

        private async void SetData()
        {
            Log.Debug("Hack@Home", "SetData()");
            var serviceClient = new ServiceClient();
            var result = await serviceClient.GetEvidencesAsync(token);

            cache.Evidences = result;

            evidencesAdapter = new EvidencesAdapter(
                this,
                result,
                Resource.Layout.EvidenceItem,
                Resource.Id.tvEvidenceTitle,
                Resource.Id.tvEvidenceStatus
                );

            lvEvidences.Adapter = evidencesAdapter;
        }
    }
}

