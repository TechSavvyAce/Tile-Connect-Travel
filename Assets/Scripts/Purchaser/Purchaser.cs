using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;


public class Purchaser : MonoBehaviour, IStoreListener
{

    public static Purchaser ins;

    //==================
    private static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
    // for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    public static string kProductIDSmallPack = "smallPack";                                                         // General handle for the consumable product.
    public static string kProductIDNormalPack = "mediumPack";                                                  // General handle for the non-consumable product.
    public static string kProductIDBigPack = "bigPack";                                                   // General handle for the subscription product.

    private static string kProductNameAppleSmallPack = "com.minigameshouse.pikachuclassic.smallpack";             // Apple App Store identifier for the consumable product.
    private static string kProductNameAppleNormalPack = "com.minigameshouse.pikachuclassic.normalpack";      // Apple App Store identifier for the non-consumable product.
    private static string kProductNameAppleBigPack = "com.minigameshouse.pikachuclassic.bigpack";       // Apple App Store identifier for the subscription product.

    private static string kProductNameGooglePlaySmallPack = "com.minigameshouse.pikachuclassic.small";        // Google Play Store identifier for the consumable product.
    private static string kProductNameGooglePlayNormalPack = "com.minigameshouse.pikachuclassic.medium";     // Google Play Store identifier for the non-consumable product.
    private static string kProductNameGooglePlayBigPack = "com.minigameshouse.pikachuclassic.big";  // Google Play Store identifier for the subscription product.

    void Start()
    {
        ins = this;
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        else
        {
            //update();
        }
    }
    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        Debug.Log("init Inapp purchaser");
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
        builder.AddProduct(kProductIDSmallPack, ProductType.Consumable, new IDs() { { kProductNameAppleSmallPack, AppleAppStore.Name }, { kProductNameGooglePlaySmallPack, GooglePlay.Name }, });// Continue adding the non-consumable product.
        builder.AddProduct(kProductIDNormalPack, ProductType.Consumable, new IDs() { { kProductNameAppleNormalPack, AppleAppStore.Name }, { kProductNameGooglePlayNormalPack, GooglePlay.Name }, });// And finish adding the subscription product.
        builder.AddProduct(kProductIDBigPack, ProductType.Consumable, new IDs() { { kProductNameAppleBigPack, AppleAppStore.Name }, { kProductNameGooglePlayBigPack, GooglePlay.Name }, });// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        // If the stores throw an unexpected exception, use try..catch to protect my logic here.
        try
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    errorMessage();
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                errorMessage();
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        // Complete the unexpected exception handling ...
        catch (Exception e)
        {
            errorMessage();
            // ... by reporting any unexpected exception for later diagnosis.
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    void errorMessage()
    {
        ToastManager.showToast(StringUtils.error);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;

        foreach (var product in controller.products.all)
        {
            Debug.Log(product.metadata.localizedTitle);
            Debug.Log(product.metadata.localizedDescription);
            Debug.Log(product.metadata.localizedPriceString);
            Debug.Log(product.transactionID);
        }
        //update();
        // BuyProductID (kProductIDNormalPack);
    }

    //public void update()
    //{
    //    if (m_StoreController == null)
    //        return;
    //    Debug.Log("update store cost");
    //    IStoreController controller = m_StoreController;
    //    if (controller.products.all.Length >= 1)
    //    {
    //        var product = controller.products.all[0];
    //        //GameStatic.storeController.costItem1.text = product.metadata.localizedPriceString;
    //        StoreController2.cost1 = product.metadata.localizedPriceString;
    //    }
    //    if (controller.products.all.Length >= 2)
    //    {
    //        var product = controller.products.all[1];
    //        //GameStatic.storeController.costItem2.text = product.metadata.localizedPriceString;
    //        StoreController2.cost2 = product.metadata.localizedPriceString;
    //    }
    //    if (controller.products.all.Length >= 3)
    //    {
    //        var product = controller.products.all[2];
    //        //GameStatic.storeController.costItem3.text = product.metadata.localizedPriceString;
    //        StoreController2.cost3 = product.metadata.localizedPriceString;
    //    }
    //}


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        PlayerPrefs.SetInt("purchaser_progress", 1);
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, kProductIDSmallPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));//If the consumable item has been successfully purchased, add 100 coins to the player's in-game score.
            Save.addPlayerCoin(GameConfig.small_count);
            GameStatic.playerInfo.updatePlayerCoin(Save.getPlayerCoin());
            ToastManager.showToast("Success");
            GameStatic.logBuyCoin(kProductIDSmallPack, GameConfig.small_count, Save.getPlayerCoin(), args.purchasedProduct.transactionID);
        }

        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNormalPack, StringComparison.Ordinal))
        {
            Save.addPlayerCoin(GameConfig.medium_count);
            GameStatic.playerInfo.updatePlayerCoin(Save.getPlayerCoin());

            ToastManager.showToast("Success");
            GameStatic.logBuyCoin(kProductIDNormalPack, GameConfig.medium_count, Save.getPlayerCoin(), args.purchasedProduct.transactionID);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }// Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, kProductIDBigPack, StringComparison.Ordinal))
        {
            Save.addPlayerCoin(GameConfig.big_count);
            GameStatic.playerInfo.updatePlayerCoin(Save.getPlayerCoin());
            ToastManager.showToast("Success");
            GameStatic.logBuyCoin(kProductIDBigPack, GameConfig.big_count, Save.getPlayerCoin(), args.purchasedProduct.transactionID);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }// Or ... an unknown product has been purchased by this user. Fill in additional products here.
        else
        {
            errorMessage();
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Loading.hideLoading();
        errorMessage();
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
