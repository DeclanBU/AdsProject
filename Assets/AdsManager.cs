using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

	private RewardBasedVideoAd admobRewardBasedVideo;
	private InterstitialAd interstitial;

	string videoPlacement = "rewardedVideo";
	public bool testMode = true;

	#if UNITY_ANDROID
	string appId = "ca-app-pub-3098190773030829~9506109197";
	#elif UNITY_IPHONE
	string appId = "ca-app-pub-3940256099942544~1458002511";
	#else
	string appId = "unexpected_platform";
	#endif

	#if UNITY_IOS
	public const string gameID = "1234567";
	#elif UNITY_ANDROID
	public const string gameID = "3082315";
	#elif UNITY_EDITOR
	public const string gameID = "1111111";
	#endif

	// Use this for initialization
	void Start () {

		MobileAds.Initialize(appId);
		this.admobRewardBasedVideo = RewardBasedVideoAd.Instance;
		RequestRewardBasedVideo();
		RequestInterstitial ();

		Advertisement.Initialize (gameID, testMode);
	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
	string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/4411468910";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		//https://developers.google.com/admob/unity/interstitial
		
		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);

		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
	}

	private void RequestRewardBasedVideo()
	{
		#if UNITY_ANDROID
		string admobRewardId = "ca-app-pub-3940256099942544/5224354917";
		#elif UNITY_IPHONE
		string admobRewardId = "ca-app-pub-3940256099942544/1712485313";
		#else
		string admobRewardId = "unexpected_platform";
		#endif

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded video ad with the request.
		this.admobRewardBasedVideo.LoadAd(request, admobRewardId);

		// Called when an ad request has successfully loaded.
		admobRewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		// Called when an ad request failed to load.
		admobRewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// Called when an ad is shown.
		admobRewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		// Called when the ad starts to play.
		admobRewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		// Called when the user should be rewarded for watching a video.
		admobRewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		// Called when the ad is closed.
		admobRewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		// Called when the ad click caused the user to leave the application.
		admobRewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onAdmobRewardedVideo(){
		if(admobRewardBasedVideo.IsLoaded())
			admobRewardBasedVideo.Show ();
	}

	public void onAdmobInterstitial(){
		if (this.interstitial.IsLoaded()) {
			this.interstitial.Show();
		}
	}

	public void onUnityVideo(){
		Advertisement.Show (videoPlacement);
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received OnAdLoaded");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardBasedVideoFailedToLoad event received with message: "
			+ args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
			"HandleRewardBasedVideoRewarded event received for "
			+ amount.ToString() + " " + type);
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
			+ args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

}
