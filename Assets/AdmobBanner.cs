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
