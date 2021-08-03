using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour {

    public static AdManager instance;

    private string appID = "";

    private BannerView bannerView;
    private string bannerID = "";

    private InterstitialAd fullScreenAd;
    private string fullScreenAdID = "";

    private RewardBasedVideoAd rewardedAd;
    private string rewardedAdID = "";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
          MobileAds.Initialize(appID) ;

        RequestFullScreenAd();

        rewardedAd = RewardBasedVideoAd.Instance;

        RequestRewardedAd();
        RequestBanner() ;
         
         


        rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;

        rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;

        rewardedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

        rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;
    }


    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

       this.bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void RequestFullScreenAd()
    {
        fullScreenAd = new InterstitialAd(fullScreenAdID);

        AdRequest request = new AdRequest.Builder().Build();

        fullScreenAd.LoadAd(request);

    }

    public void ShowFullScreenAd()
    {
        if (fullScreenAd.IsLoaded())
        {
            fullScreenAd.Show();
            RequestFullScreenAd()  ;
        }else
        {
            Debug.Log("Full screen ad not loaded");
            RequestFullScreenAd()  ;
        }
    }

    public void RequestRewardedAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request, rewardedAdID);
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }else
        {
            Debug.Log("Rewarded ad not loaded");
        }
    }



    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Video ad loaded successfully");

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed to load rewarded video ad : " + args.Message);


    }



    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("You have been rewarded with  " + amount.ToString() + " " + type);

       // UIManager.instance.RewardPanel.SetActive(true);
     //   UIManager.instance.GameOverUI.SetActive(false);


    }


    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log ("Rewarded video has closed");
        RequestRewardedAd();

    }
}
