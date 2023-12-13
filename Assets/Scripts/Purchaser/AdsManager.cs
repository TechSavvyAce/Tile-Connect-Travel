using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
//using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public static AdsManager ins;

    //RewardBasedVideoAd rewardBasedVideo;
    //bool rewardBasedEventHandlersSet = false;
    //public static bool isShowUnityAdsBefore = false;
    //bool show_unity_first = true;
    //bool show_ads_by_rate = false;
    //int unity_show_rate = 50;
    public BannerView bannerView;
    //public bool isShowBanner = false;
    private float currentTime;
    private InterstitialAd interstitial;
    private bool isFirstAds = true;
    // Use this for initialization
    void Start()
    {
        ins = this;
        showBannerView();


#if UNITY_EDITOR
#else
        if (GameConfig.isShowStartAds)
        {
            loadingPanel.SetActive(true);
        }
#endif
        RequestInterstitial();
    }

    void Update()
    {
        //if (isShowBanner)
        //{
        //    currentTime += Time.deltaTime;
        //    if (currentTime > GameConfig.timeReloadRequestBanner)
        //    {
        //        RequestBanner();
        //        currentTime = 0;
        //    }
        //}
    }

    private void RequestBanner()
    {
        Debug.Log("RequestBanner");
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build();
        bannerView.LoadAd(request);
    }

    public void showBannerView()
    {
//        Debug.Log("call showBannerView");
//        if (!isShowBanner)
//        {
//#if UNITY_ANDROID
//            string adUnitId = GameConfig.admob_banner_android;
//#elif UNITY_IPHONE
//		string adUnitId = GameConfig.admob_banner_ios;
//#else
//		string adUnitId = "unexpected_platform";
//#endif
//            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
//            RequestBanner();
//            isShowBanner = true;
//        }
    }

    public void hideBannerView()
    {
        //if (isShowBanner && bannerView != null)
        //{
        //    bannerView.Destroy();
        //    isShowBanner = false;
        //}
        
    }

    //public void requestAdmob()
    //{
    //    RequestInterstitial();
    //}

    //public bool isAdmobReady()
    //{
    //    return interstitial.IsLoaded();
    //}

    //public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    Debug.Log("Fail to load. Try load again");
    //    RequestRewardBasedVideo();
    //}

    //public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    //{
    //    RequestRewardBasedVideo();
    //    Debug.Log("Done reward video. Try load again");
    //}

    //public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    //{
    //    string type = args.Type;
    //    double amount = args.Amount;
    //    print("User rewarded with: " + amount.ToString() + " " + type);
    //    Reward();
    //}


    public void ShowFullAds()
    {
        //if (GSM.CONFIG.ContainsKey ("show_unity_first")) {
        //	show_unity_first = (bool)GSM.CONFIG.GetBoolean ("show_unity_first");
        //}
        //if (GSM.CONFIG.ContainsKey ("show_ads_by_rate")) {
        //	show_ads_by_rate = (bool)GSM.CONFIG.GetBoolean ("show_ads_by_rate");
        //}
        //if (GSM.CONFIG.ContainsKey ("unity_show_rate")) {
        //	unity_show_rate = (int)GSM.CONFIG.GetInt ("unity_show_rate");
        //}


        //if (show_ads_by_rate)
        //{
        //    if (UnityEngine.Random.Range(0, 100) < unity_show_rate)
        //    {
        //        _showAdsUnityFirst();
        //    }
        //    else
        //    {
        //        _showAdsAdmobFirst();
        //    }
        //}
        //else
        //{
        //    if (show_unity_first)
        //    {
        //        _showAdsUnityFirst();
        //    }
        //    else
        //    {
        //        _showAdsAdmobFirst();
        //    }
        //}

        if (interstitial != null && interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }

    //void _showAdsUnityFirst()
    //{
    //    if (Advertisement.IsReady())
    //    {
    //        Advertisement.Show();
    //        isShowUnityAdsBefore = true;
    //    }
    //    else if (interstitial != null && interstitial.IsLoaded())
    //    {
    //        interstitial.Show();
    //        RequestInterstitial();
    //    }
    //}

    IEnumerator requestFullads()
    {
        yield return new WaitForSeconds(10f);
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.OnAdLoaded -= Interstitial_OnAdLoaded;
            interstitial.OnAdClosed -= Interstitial_OnAdClosed;
            interstitial.OnAdFailedToLoad -= Interstitial_OnAdFailedToLoad;
            interstitial = null;
        }

#if UNITY_ANDROID
        string adUnitId = GameConfig.admob_full_android;
#elif UNITY_IPHONE
		string adUnitId = GameConfig.admob_full_ios;
#else
		string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
        interstitial.OnAdClosed += Interstitial_OnAdClosed;
        interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void Interstitial_OnAdLoaded(object sender, EventArgs e)
    {
        if (GameConfig.isShowStartAds && isFirstAds)
        {
            isFirstAds = false;
            loadingPanel.SetActive(false);
            interstitial.Show();
        }
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (GameConfig.isShowStartAds && isFirstAds)
        {
            isFirstAds = false;
            loadingPanel.SetActive(false);
        }
        StartCoroutine(requestFullads());
    }

    private void Interstitial_OnAdClosed(object sender, EventArgs e)
    {
        RequestInterstitial();
    }

    //    private void RequestRewardBasedVideo()
    //    {
    //#if UNITY_EDITOR
    //        string adUnitId = "unused";
    //#elif UNITY_ANDROID
    //	string adUnitId = "ca-app-pub-4392096336290870/5284432347";
    //#elif UNITY_IPHONE
    //	string adUnitId = "INSERT_AD_UNIT_HERE";
    //#else
    //	string adUnitId = "unexpected_platform";
    //#endif

    //        rewardBasedVideo = RewardBasedVideoAd.Instance;

    //        AdRequest request = new AdRequest.Builder().Build();
    //        rewardBasedVideo.LoadAd(request, adUnitId);
    //        if (!rewardBasedEventHandlersSet)
    //        {
    //            // has failed to load.
    //            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
    //            // has rewarded the user.
    //            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
    //            // is closed.
    //            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

    //            rewardBasedEventHandlersSet = true;
    //        }
    //    }

    //public void ShowRewardedAd()
    //{
    //	if (Advertisement.IsReady ("rewardedVideo")) {
    //		var options = new ShowOptions { resultCallback = HandleShowResult };
    //		Advertisement.Show ("rewardedVideo", options);
    //		isShowUnityAdsBefore = true;
    //	} else if (rewardBasedVideo != null && rewardBasedVideo.IsLoaded ()) {
    //		rewardBasedVideo.Show ();
    //	} else {
    //		ToastManager.showToast ("No video available");
    //	}
    //}

    //private void HandleShowResult(ShowResult result)
    //{
    //	switch (result)
    //	{
    //	case ShowResult.Finished:
    //		Debug.Log ("The ad was successfully shown.");
    //	//
    //	// YOUR CODE TO REWARD THE GAMER
    //	// Give coins etc.
    //		Reward ();
    //	break;
    //	case ShowResult.Skipped:
    //	Debug.Log("The ad was skipped before reaching the end.");
    //	break;
    //	case ShowResult.Failed:
    //	Debug.LogError("The ad failed to be shown.");
    //	break;
    //	}
    //}

    //public void Reward()
    //{
    //}

    //public void RewardError()
    //{
    //}
}
