using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class AdmobBanner : MonoBehaviour {

	public bool top;
	public bool bottom;
	public bool dontDestroy;


	#if UNITY_ANDROID
	string adUnitId = "ca-app-pub-3940256099942544/6300978111";
	#endif
	 

	private static GameObject instance = null;

	BannerView bannerView;
	// Use this for initialization
	void Start () {
		
		if (dontDestroy) {
			if (instance == null) {
				DontDestroyOnLoad (transform.gameObject);
				instance = gameObject;
			} else {
				Destroy (gameObject);
			}
		}

		if(top)
			bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
		else if(bottom)
			bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
		bannerView.OnAdLoaded += HandleOnAdLoaded;

		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
		bannerView.Show ();
	}
	/*private BannerView bannerView;
        …
        public void Start()
        {
            #if UNITY_ANDROID
                string appId = "ca-app-pub-3940256099942544~3347511713";
            #elif UNITY_IPHONE
                string appId = "ca-app-pub-3940256099942544~1458002511";
            #else
                string appId = "unexpected_platform";
            #endif
    
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);
    
            this.RequestBanner();
        }
         private void RequestBanner(){
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }*/

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
			+ args.Message);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy() {
		print("Script was destroyed");
		bannerView.Destroy ();
	}
}
