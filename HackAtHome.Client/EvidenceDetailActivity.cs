
using System;
using Android.App;
using Android.OS;
using HackAtHome.SAL;
using Android.Webkit;
using Android.Widget;

namespace HackAtHome.Client
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/HackAtHome")]
    public class EvidenceDetailActivity : Activity
    {
        TextView tvFullname_Detail;
        TextView tvEvidenceTitle_Detail;
        TextView tvEvidenceStatus_Detail;
        WebView wvContent;
        ImageView ivEvidenceImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EvidenceDetail);

            tvFullname_Detail = FindViewById<TextView>(Resource.Id.tvFullname_Detail);
            tvEvidenceStatus_Detail = FindViewById<TextView>(Resource.Id.tvEvidenceStatus_Detail);
            tvEvidenceTitle_Detail = FindViewById<TextView>(Resource.Id.tvEvidenceTitle_Detail);
            ivEvidenceImage = FindViewById<ImageView>(Resource.Id.ivEvidenceImage);
            wvContent = FindViewById<WebView>(Resource.Id.wvContent);

            tvFullname_Detail.Text = Intent.GetStringExtra("fullname");
            tvEvidenceStatus_Detail.Text = Intent.GetStringExtra("status");
            tvEvidenceTitle_Detail.Text = Intent.GetStringExtra("title");
            wvContent.SetBackgroundColor(Android.Graphics.Color.Transparent);

            SetEvidenceDetail(Intent.GetStringExtra("token"), Intent.GetIntExtra("evidenceId", 0));
        }

        private async void SetEvidenceDetail(string token, int evidenceId)
        {
            var serviceClient = new ServiceClient();

            var detail = await serviceClient.GetEvidenceByIDAsync(token, evidenceId);
            var data = $"<html><head><style type='text/css'>body{{color:#fff}}</style></head><body>{detail.Description}</body></html>";

            wvContent.LoadDataWithBaseURL(null, data, "text/html", "utf-8", null);
            Koush.UrlImageViewHelper.SetUrlDrawable(ivEvidenceImage, detail.Url);
        }
    }
}